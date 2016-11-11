using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using TwitterFeedReader.Models;

namespace TwitterFeedReader.Api
{
    public static class TwitterApiService
    {

        #region Private Properties 

        private static readonly String OAuthBaseUrl = "https://api.twitter.com/oauth2/token";

        private static readonly String OAuthAPIKey = "35Sf1oX5ixIkNA0Xx8kd97aGg";

        private static readonly String OAuthAPISecret = "3KJs7ZydK5K1mFxPFPDuLaaLysaSmjo2Po3BwmRuDAyEbWwuQh";

        private static String UserTimelineBaseUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user={2}&exclude_replies={3}";

        #endregion

        #region Public Static Methods


        public static async Task<string> GetAccessToken()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, OAuthBaseUrl);
                var customerInfo = Convert.ToBase64String(new UTF8Encoding()
                                          .GetBytes(OAuthAPIKey + ":" + OAuthAPISecret));
                request.Headers.Add("Authorization", "Basic " + customerInfo);
                request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                                                                          "application/x-www-form-urlencoded");

                HttpResponseMessage response = await httpClient.SendAsync(request);
                string jsonResponseMessage = await response.Content.ReadAsStringAsync();
                dynamic parsedResponse = new JavaScriptSerializer().Deserialize<object>(jsonResponseMessage);
                return parsedResponse["access_token"];
            }
        }

        public static async Task<IEnumerable<TwitterFeedModel>> GetTweets(string userName, int count, string filter = null, string accessToken = null)
        {
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = await GetAccessToken();
            }

            using (HttpClient httpClient = new HttpClient())
            {

                var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get,
                string.Format(UserTimelineBaseUrl,
                              count, userName, 0, 1));

                requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);

                HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);

                dynamic jsonResponse = new JavaScriptSerializer().Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());

                var tweetCollection = (jsonResponse as IEnumerable<dynamic>);

                if (tweetCollection == null)
                {
                    return null;
                }

                // apply filter, if passed
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    tweetCollection = tweetCollection.Where(t => t["text"] != null && t["text"].ToString().Contains(filter));
                }

                return tweetCollection.Select(t => new TwitterFeedModel()
                {
                    TweetId = t["id"],
                    UserName = t["user"]["name"]
                });
            }

        }
        #endregion

    }
}