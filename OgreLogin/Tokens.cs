using System.Text.RegularExpressions;

namespace OgreIsland.Login
{
    public class Tokens
    {
        private static readonly Regex ViewStateRegex = new Regex("<input type=\\\"hidden\\\" name=\\\"__VIEWSTATE\\\" id=\\\"__VIEWSTATE\\\" value=\\\"([0-9a-zA-Z\\+/=]{20,})\\\" />", RegexOptions.Compiled);
        private static readonly Regex EventValidationRegex = new Regex("<input type=\\\"hidden\\\" name=\\\"__EVENTVALIDATION\\\" id=\\\"__EVENTVALIDATION\\\" value=\\\"([0-9a-zA-Z\\+/=]{20,})\\\" />", RegexOptions.Compiled);

        public string ViewState { get; private set; }
        public string EventValidation { get; private set; }

        public Tokens(string document)
        {
            ViewState = ViewStateRegex.Match(document).Groups[1].Value;
            EventValidation = EventValidationRegex.Match(document).Groups[1].Value;
        }
    }
}
