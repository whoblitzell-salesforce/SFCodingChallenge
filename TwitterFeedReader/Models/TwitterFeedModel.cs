using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterFeedReader.Models
{
    public class TwitterFeedModel
    {
        public String UserName { get; set; } = "Unknown";
        public long TweetId { get; set; }
        public String TweetUrl
        {
            get
            {
                String formattedUrl = String.Format("https://twitter.com/{0}/status/{1}", Uri.EscapeDataString(UserName), TweetId);
                return Uri.EscapeUriString(formattedUrl);
            }
        }
    }
}