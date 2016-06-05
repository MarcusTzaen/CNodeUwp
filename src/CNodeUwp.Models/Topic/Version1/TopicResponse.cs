using CNodeUwp.Models.User.Version1;
using Newtonsoft.Json;
using System;
using CNodeUwp.Common;

namespace CNodeUwp.Models.Topic.Version1
{
    public class TopicResponse
    {
        /// <summary>
        /// 主题ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 作者ID
        /// </summary>
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }

        /// <summary>
        /// 主题分类 <see cref="TopicTabType"/>
        /// </summary>
        public string Tab { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        [JsonProperty("last_reply_at")]
        public DateTime LastRepliedAt { get; set; }

        /// <summary>
        /// 是否精华
        /// </summary>
        [JsonProperty("good")]
        public bool IsGood { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [JsonProperty("top")]
        public bool IsTop { get; set; }

        /// <summary>
        /// 回复总数
        /// </summary>
        [JsonProperty("reply_count")]
        public int RepliedCount { get; set; }

        /// <summary>
        /// 访问总数
        /// </summary>
        [JsonProperty("visit_count")]
        public int VisitedCount { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonProperty("create_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 作者信息
        /// </summary>
        public UserInfo Author { get; set; }

        public string TopicDescription
        {
            get
            {
                return $"作者：{Author.LoginName} | 查看：{VisitedCount} | 回复：{RepliedCount} | 最后回复：{LastRepliedAt.GetDiffFromNow()}";
            }
        }

    }
}
