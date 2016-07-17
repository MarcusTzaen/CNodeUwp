using Windows.Storage;

namespace CNodeUwp.Common
{
    public class TokenHelper
    {
        private const string TOKEN_KEY = "cnode_uwp_token";

        public static void SaveToken(string token)
        {
            ApplicationData.Current.LocalSettings.Values[TOKEN_KEY] = token;
        }

        public static string GetToken()
        {
            return ApplicationData.Current.LocalSettings.Values[TOKEN_KEY]?.ToString();
        }
    }
}
