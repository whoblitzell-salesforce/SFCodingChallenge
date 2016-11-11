using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TwitterFeedReader.Models;
using TwitterFeedReader.Utility;

namespace TwitterFeedReader.Controllers
{
    [RoutePrefix("api/tweets")]
    public class TwitterFeedController : ApiController
    {
        // GET api/values
        [Compress]
        [Route("{count:int}/{userName:alpha}/{filter:alpha?}")]
        public async Task<IEnumerable<TwitterFeedModel>> Get(int count, string userName, string filter = "")
        {
            var tweets = await Api.TwitterApiService.GetTweets(userName, count, filter, string.Empty);
            return tweets;
        }

        // POST api/values
        public void Post()
        {
            throw new NotSupportedException();
        }

        // PUT api/values/5
        public void Put()
        {
            throw new NotSupportedException();
        }

        // DELETE api/values/5
        public void Delete()
        {
            throw new NotSupportedException();
        }
    }
}
