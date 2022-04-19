using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Click
    {
        public Guid Id { get; set; }
        public DateTime ClickedAt{ get; set; }
        public string Browser{ get; set; }
        public string Platform { get; set; }
        public Url Url { get; set; }

    }
}
