using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ViewModel
{
    public class CommentModel
    {
        public RegisterModel User { get; set; }
        public DateTime PublishTime { get; set; }
        public List<CommentModel> Replys { get; set; }
        public string ReplyUsername { get; set; }
        public string Content { get; set; }
        public int Agree { get; set; }
        public int DisAgree { get; set; }
    }
}
