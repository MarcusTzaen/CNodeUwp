using CNodeUwp.Models.Common.Version1;
using CNodeUwp.Models.Topic.Version1;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace CNodeUwp.Services
{
    public class TopicService
    {
        public async static Task<ObservableCollection<TopicResponse>> GetTopicListAsync(TopicPageRequest request
            , CancellationToken cancellationToken = default(CancellationToken))
        {
            string url = $"/v1/topics?page={request.Page}&tab={request.Tab.ToString().ToLowerInvariant()}&limit={request.Limit}";
            var response = await ServiceHelper.Get<ApiResponse<ObservableCollection<TopicResponse>>>(url).ConfigureAwait(false);
            return response.GetData();
        }

        public async static Task<TopicDetailResponse> GetTopicDetailAsync(TopicDetailRequest request
            , CancellationToken cancellationToken = default(CancellationToken))
        {
            string url = $"/v1/topic/{request.TopicId}";
            var response = await ServiceHelper.Get<ApiResponse<TopicDetailResponse>>(url).ConfigureAwait(false);
            return response.GetData();
        }
    }
}
