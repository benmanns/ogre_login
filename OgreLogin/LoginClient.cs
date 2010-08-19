using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace OgreIsland.Login
{
    public class LoginClient : CookieClient
    {
        private static readonly Regex CharacterRegex = new Regex("<a id=\\\"page_content_charlist_ctl([0-9]{2,})_charname\\\" href=\\\"javascript:__doPostBack\\('page\\$content\\$charlist\\$ctl([0-9]{2,})\\$charname',''\\)\\\">([a-zA-Z][a-zA-Z0-9 ]{2,19})</a>", RegexOptions.Compiled);

        private readonly Uri _homeUri = new Uri("home.aspx", UriKind.Relative);
        private readonly Uri _gameUri = new Uri("game.aspx", UriKind.Relative);

        public Dictionary<string, string> Characters { get; private set; }

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

        public void LoadCharacters()
        {
            string game = DownloadString(_gameUri);
            MatchCollection characterMatches = CharacterRegex.Matches(game);
            Characters = new Dictionary<string, string>(characterMatches.Count);
            foreach (Match character in characterMatches) Characters.Add(character.Groups[2].Value, character.Groups[3].Value);
        }

        public void UpdateCharacter(string id)
        {
            Tokens tokens = new Tokens(DownloadString(_gameUri));
            NameValueCollection update = new NameValueCollection
                                             {
                                                 {
                                                     "__EVENTTARGET",
                                                     string.Format("page$content$charlist$ctl{0}$charname", id)
                                                     },
                                                 {"__EVENTARGUMENT", String.Empty},
                                                 {"__VIEWSTATE", tokens.ViewState},
                                                 {"__EVENTVALIDATION", tokens.EventValidation},
                                                 {"page$itemkey", String.Empty},
                                                 {"page$itemname", String.Empty},
                                                 {"page$content$tradecharlist", String.Empty}
                                             };
            UploadValues(_gameUri, "POST", update);
        }
    }
}
