using System;
using System.Net;

namespace OgreIsland.Login
{
    public class CookieClient : WebClient
    {
        public CookieContainer Cookies { get; private set; }
        public CookieClient() { Cookies = new CookieContainer(); }
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest) (request as HttpWebRequest).CookieContainer = Cookies;
            return request;
        }
    }
}
