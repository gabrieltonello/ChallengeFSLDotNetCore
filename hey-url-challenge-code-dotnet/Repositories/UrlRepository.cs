using hey_url_challenge_code_dotnet.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Repositories
{
    public class UrlRepository: IUrlRepository
    {
        private readonly ApplicationContext _context;
        public UrlRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Url InsertUrl(Url url)
        {
            Url tryFind = _context.Urls.Where(s => s.OriginalUrl == url.OriginalUrl).FirstOrDefault();
            if (tryFind != null)
                return tryFind;

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var shortUrl = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            url.ShortUrl = shortUrl;
            url.Id = new Guid();
            url.CreatedAt = DateTime.Now;
            _context.Urls.Add(url);
            _context.SaveChanges();

            return url;
        }
        public ShowViewModel GetShowViewModelData(Url url)
        {
            var show = new ShowViewModel();
            var currentDate = DateTime.Now;

            show.Url = url;
            var clicks = _context.Clicks
                .Where(click => click.Url.ShortUrl == url.ShortUrl
                && click.ClickedAt.Month == currentDate.Month
                && click.ClickedAt.Year == currentDate.Year);

            show.DailyClicks = clicks.GroupBy(g => g.ClickedAt.Day)
                .Select(s => new { Key = s.Key, Value = s.Count() })
                .ToDictionary(d => d.Key.ToString(), d => d.Value);

            show.PlatformClicks = clicks.GroupBy(g => g.Platform)
                .Select(s => new { Key = s.Key, Value = s.Count() })
                .ToDictionary(d => d.Key.ToString(), d => d.Value);

            show.BrowseClicks = clicks.GroupBy(g => g.Browser)
                .Select(s => new { Key = s.Key, Value = s.Count() })
                .ToDictionary(d => d.Key.ToString(), d => d.Value);

            return show;

        }
        public ICollection<Url> GetLast10Urls()
        {
            return _context.Urls.OrderByDescending(o => o.CreatedAt).Include(i => i.Clicks).Take(10).ToList();
        }

        public async Task<ICollection<Url>> GetLast10UrlsAsync()
        {
            return await _context.Urls.OrderByDescending(o => o.CreatedAt).Include(i => i.Clicks).Take(10).ToListAsync();
        }

        public string GetRedirectionLink(string url, string platform, string browser)
        {
            Url urlFounded = _context.Urls.Where(s => s.ShortUrl == url).FirstOrDefault();

            if (urlFounded != null)
                InsertClick(new Click { Url = urlFounded, Platform = platform, Browser = browser });

            return urlFounded?.OriginalUrl;
        }
        private void InsertClick(Click click)
        {
            click.Id = new Guid();
            click.ClickedAt = DateTime.Now;
            _context.Clicks.Add(click);
            _context.SaveChanges();
        }
    }
}
