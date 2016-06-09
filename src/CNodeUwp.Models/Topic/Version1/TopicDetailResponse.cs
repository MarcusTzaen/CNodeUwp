using Newtonsoft.Json;
using System.Collections.Generic;

namespace CNodeUwp.Models.Topic.Version1
{
    public class TopicDetailResponse : TopicResponse
    {
        public IEnumerable<TopicReplyResponse> Replies { get; set; }

        /// <summary>
        /// 是否被用户收藏
        /// </summary>
        [JsonProperty("is_collect")]
        public bool IsCollected { get; set; }
    }
}
