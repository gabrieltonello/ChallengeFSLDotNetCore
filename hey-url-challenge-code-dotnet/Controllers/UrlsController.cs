using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using HeyUrlChallengeCodeDotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly IUrlRepository _urlRepository;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IUrlRepository repository)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _urlRepository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Urls = _urlRepository.GetLast10Urls();
            model.NewUrl = new();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(HomeViewModel url)
        {
            if (!Uri.IsWellFormedUriString(url.NewUrl.OriginalUrl, UriKind.Absolute))
                return View(new HomeViewModel
                {
                    Urls = _urlRepository.GetLast10Urls(),
                    Message = "The Url is not valid! Please, try another one."
                });

            var shortUrl = _urlRepository.InsertUrl(new Url
            {
                OriginalUrl = url.NewUrl.OriginalUrl
            });

            return View(new HomeViewModel
            {
                Urls = _urlRepository.GetLast10Urls(),
                Message = ""
            });
        }

        [Route("/{url}")]
        public IActionResult Visit(string url)
        {
            var redir = _urlRepository.GetRedirectionLink(url, this.browserDetector.Browser.OS, this.browserDetector.Browser.Name);

            if (redir == null)
                return Redirect("Error");

            return Redirect(redir);
        }

        [Route("urls/{url}")]
        public IActionResult Show(string url) { return View(_urlRepository.GetShowViewModelData(new Url { ShortUrl = url })); }

        [HttpGet]
        [Route("api/GetLast10Urls")]
        public async Task<IActionResult> GetLast10URls()
        {
            var homeViewModel = new HomeViewModel
            {
                Urls = await _urlRepository.GetLast10UrlsAsync()
            };

            return Ok(homeViewModel);
        }

        [Route("error")]
        public IActionResult Error(string message = "404")
        {
            var model = new ErrorViewModel() { RequestId = "", Message = message};
            return View(model);
        }
    }
}