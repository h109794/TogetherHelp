using SRV.ViewModel;
using System;
using System.Linq;
using System.Text;

namespace SRV.ServiceInterface
{
    public interface ICommentService
    {
        CommentModel Publish(int articleId, int userId, string commentContent);
    }
}
