using Newtonsoft.Json;

namespace CNodeUwp.Models.Common.Version1
{
    public class ApiResponse<T>
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public T GetData()
        {
            if (IsSuccess)
            {
                return Data;
            }
            return default(T);
        }
    }
}
