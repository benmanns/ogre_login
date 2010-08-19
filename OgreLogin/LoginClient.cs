using System;
using System.Collections.Specialized;

namespace OgreIsland.Login
{
    public class LoginClient : CookieClient
    {
        private readonly Uri _homeUri = new Uri("home.aspx", UriKind.Relative);

        public LoginClient(string baseAddress) { BaseAddress = baseAddress; }

        public void Login(string email, string password)
        {
            Tokens tokens = new Tokens(DownloadString(_homeUri));
            NameValueCollection login = new NameValueCollection
                                            {
                                                {"__LASTFOCUS", String.Empty},
                                                {"__EVENTTARGET", String.Empty},
                                                {"__EVENTARGUMENT", String.Empty},
                                                {"__VIEWSTATE", tokens.ViewState},
                                                {"__EVENTVALIDATION", tokens.EventValidation},
                                                {"page$itemkey", String.Empty},
                                                {"page$itemname", String.Empty},
                                                {"page$loginemail", email},
                                                {"page$loginpassword", password},
                                                {"page$btnLogin", "Login"}
                                            };
            UploadValues(_homeUri, "POST", login);
        }
    }
}
