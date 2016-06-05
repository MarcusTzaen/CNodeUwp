using Newtonsoft.Json;

namespace CNodeUwp.Models.Topic.Version1
{
    public class TopicPageRequest
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 主题分类
        /// </summary>
        public TopicTabType Tab { get; set; }

        /// <summary>
        /// 每一页的主题数量
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// 当为 false 时，不渲染。默认为 true，渲染出现的所有 markdown 格式文本。
        /// </summary>
        [JsonProperty("mdrender")]
        public bool NeedRendered { get; set; } = true;
    }
}
