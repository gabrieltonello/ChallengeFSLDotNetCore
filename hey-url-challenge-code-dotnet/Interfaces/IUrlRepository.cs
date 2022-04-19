using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Interfaces
{
    public interface IUrlRepository
    {
        public Url InsertUrl(Url url);
        public ShowViewModel GetShowViewModelData(Url url);
        public ICollection<Url> GetLast10Urls();
        public Task<ICollection<Url>> GetLast10UrlsAsync();

        public string GetRedirectionLink(string url, string platform, string browser);
    }
}
