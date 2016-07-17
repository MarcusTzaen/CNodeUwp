using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
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

        public static async Task<T> Post<T>(string relativeUrl, object request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(GetUrl(relativeUrl), httpContent, cancellationToken).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
                return default(T);
            }
        }
    }
}
