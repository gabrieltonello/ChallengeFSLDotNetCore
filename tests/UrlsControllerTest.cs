using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Repositories;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shyjus.BrowserDetection;
using System;

namespace tests
{
    public class UrlsControllerTest
    {
        private readonly UrlRepository _repository;
        public UrlsControllerTest()
        {
            DbContextOptionsBuilder<ApplicationContext> DBoptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer("Server=(localdb)\\Local;Initial Catalog=ChallengeFSL;Integrated Security=true");

            ApplicationContext applicationContext = new ApplicationContext(DBoptions.Options);

            _repository = new UrlRepository(applicationContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIndex()
        {
            Random random = new Random(); 
            _repository.InsertUrl(new Url { OriginalUrl = $"https://google.com/{random.Next(100000)}" });
        }

        [Test]
        public void TestGetLast10Urls()
        {
            _repository.GetLast10Urls();
        }
        [Test]
        public void TestRedirectionLink()
        {
            _repository.GetRedirectionLink("EKOYM", "platform1", "browser1");
            _repository.GetRedirectionLink("EKOYM", "platform2", "browser2");
        }

        [Test]
        public void TestShow()
        {
            _repository.GetShowViewModelData(new Url {ShortUrl = "EKOYM" });
        }
    }
}