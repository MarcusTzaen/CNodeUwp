using CNodeUwp.Models.User.Version1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CNodeUwp.Models.Topic.Version1
{
    public class TopicReplyResponse
    {
        public string Id { get; set; }

        /// <summary>
        /// 回复用户
        /// </summary>
        public UserInfo Author { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 点赞的用户
        /// </summary>
        public IEnumerable<string> UpUsers { get; set; }

        [JsonProperty("create_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 被回复的帖子
        /// </summary>
        [JsonProperty("reply_id")]
        public string ReplyId { get; set; }
        /*
         * 
         * id: "572afc7a2eb6313c2c2f8bec",
author: {
loginname: "soliury",
avatar_url: "https://avatars.githubusercontent.com/u/5032079?v=3&s=120"
},
content: "<div class="markdown-text"><p>ꉂ ೭(˵¯̴͒ꇴ¯̴͒˵)౨” From <a href="https://github.com/soliury/noder-react-native">Noder</a></p> </div>",
ups: [
"543d4f522c834593092fba06",
"573b39e3fcf698421d2035a6",
"53c39fa04830d85a1bc367eb"
],
create_at: "2016-05-05T07:55:38.007Z",
reply_id: null
         * */
    }
}
