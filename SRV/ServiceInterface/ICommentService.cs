using SRV.ViewModel;

namespace SRV.ServiceInterface
{
    public interface ICommentService
    {
        CommentModel Publish(int articleId, int userId, string commentContent,
                        string replyUsername, string replyMainCommentId, string replyCommentId);
    }
}
