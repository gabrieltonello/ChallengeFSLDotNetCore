using System;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public int Count { get => Clicks.Count; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Click> Clicks{ get; set; }
    }
}
