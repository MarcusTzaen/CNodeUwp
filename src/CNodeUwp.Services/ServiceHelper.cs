using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CNodeUwp.Services
{
    public class ServiceHelper
    {
        private static string ApiHost = "http://cnodejs.org/api";

        public static string GetUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            return $"{ApiHost}{relativeUrl}";
        }

        public static async Task<T> Get<T>(string relativeUrl)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(GetUrl(relativeUrl)).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(response);
            }
        }
    }
}
