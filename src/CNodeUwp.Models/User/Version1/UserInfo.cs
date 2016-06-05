using Newtonsoft.Json;
using System;

namespace CNodeUwp.Models.User.Version1
{
    public class UserInfo
    {
        [JsonProperty("loginname")]
        public string LoginName { get; set; }

        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }

        [JsonProperty("githubUsername")]
        public string GitHubUserName { get; set; }

        [JsonProperty("create_at")]
        public DateTime CreatedAt { get; set; }

        public int Score { get; set; }


    }
}
