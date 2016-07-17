using CNodeUwp.Models.Common.Version1;
using System.Threading;
using System.Threading.Tasks;

namespace CNodeUwp.Services.User.Version1
{
    public class UserService
    {
        public async static Task<bool> ValidateTokenAsync(string token
            , CancellationToken cancellationToken = default(CancellationToken))
        {
            string url = $"v1/accesstoken";
            var response = await ServiceHelper.Post<ApiResponse<string>>(url, new
            {
                accesstoken = token,
            }, cancellationToken).ConfigureAwait(false);
            return response?.IsSuccess ?? false;
        }
    }
}
