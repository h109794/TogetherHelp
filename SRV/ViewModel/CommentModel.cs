using System;
using System.Collections.Generic;

namespace SRV.ViewModel
{
    public class CommentModel
    {
        public int Id { get; private set; }
        public string Username { get; set; }
        public DateTime PublishTime { get; set; }
        public List<CommentModel> Replys { get; set; }
        public string ReplyUsername { get; set; }
        public string Content { get; set; }
        public int Agree { get; set; }
        public int DisAgree { get; set; }
    }
}
