using System;
using System.Collections.Generic;

namespace SRV.ViewModel
{
    public class CommentModel
    {
        public int Id { get; private set; }
        public byte[] UserProfile { set; get; }
        public string Content { get; set; }
        public string Username { get; set; }
        public string ReplyUsername { get; set; }
        public DateTime PublishTime { get; set; }
        public List<int> AgreeUserIds { get; set; }
        public List<int> DisagreeUserIds { get; set; }
        public List<CommentModel> Replys { get; set; }
    }
}
