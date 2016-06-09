using Newtonsoft.Json;

namespace CNodeUwp.Models.Topic.Version1
{
    public class TopicDetailRequest
    {
        public string TopicId { get; set; }

        /// <summary>
        /// 当为 false 时，不渲染。默认为 true，渲染出现的所有 markdown 格式文本。
        /// </summary>
        [JsonProperty("mdrender")]
        public bool NeedRendered { get; set; } = true;

        /// <summary>
        /// 用户Token，当需要知道一个主题是否被特定用户收藏时，才需要带此参数。会影响返回值中的 is_collect 值。
        /// </summary>
        [JsonProperty("accesstoken")]
        public string AccessToken { get; set; }
    }
}
