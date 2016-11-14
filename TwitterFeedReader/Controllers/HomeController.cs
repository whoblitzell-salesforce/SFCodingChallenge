﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TweetSharp;
using TwitterFeedReader.Api;
using TwitterFeedReader.Utility;

namespace TwitterFeedReader.Controllers
{
    public class HomeController : Controller
    {
        #region Private Members

        private static readonly String OAuthAPIKey = "35Sf1oX5ixIkNA0Xx8kd97aGg";

        private static readonly String OAuthAPISecret = "3KJs7ZydK5K1mFxPFPDuLaaLysaSmjo2Po3BwmRuDAyEbWwuQh";

        #endregion

        [Compress]
        public ActionResult Index()
        {
            ViewBag.Title = "Twitter Feed Reader Coding Challenge";
            return View();
        }

        public ActionResult TwitterSignIn()
        {
            // Request an OAuth Request Token
            TwitterService twitterService = new TwitterService(OAuthAPIKey, OAuthAPISecret);

            var url = Url.Action("TwitterSignInCallback", "Home", null, "http");
            OAuthRequestToken requestToken = twitterService.GetRequestToken(url);

            // Redirect to the OAuth Authorization URL
            Uri uri = twitterService.GetAuthorizationUri(requestToken);
            return new RedirectResult(uri.ToString(), false /*permanent*/);
        }

        // This URL is registered as the application's callback under Twitter's DEV API
        public ActionResult TwitterSignInCallback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            // Exchange the Request Token for an Access Token
            TwitterService twitterService = new TwitterService(OAuthAPIKey, OAuthAPISecret);
            OAuthAccessToken accessToken = twitterService.GetAccessToken(requestToken, oauth_verifier);

            // User authenticates using the Access Token
            twitterService.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = twitterService.VerifyCredentials(new VerifyCredentialsOptions() { SkipStatus = true });
            bool successfulLogin = user != null && user.IsVerified == true;

            FormsAuthentication.SetAuthCookie(user.ScreenName, false);

            return RedirectToAction("Index", "Home", new { success = successfulLogin });
        }
    }
}
