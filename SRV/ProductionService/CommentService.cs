using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;

namespace SRV.ProductionService
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentModel Publish(int articleId, int userId, string commentContent, string replyUsername, string replyMainCommentId, string replyCommentId)
        {
            if (string.IsNullOrWhiteSpace(commentContent))
            {
                throw new ArgumentNullException("Comments are empty.");
            }

            UserRepository userRepository = new UserRepository(DbContext);
            ArticleRepository articleRepository = new ArticleRepository(DbContext);
            CommentRepository commentRepository = new CommentRepository(DbContext);

            User replyUser = userRepository.GetByNickname(replyUsername);
            Comment comment = new Comment
            {
                Author = userRepository.Find(userId),
                Body = commentContent,
                PublishTime = DateTime.Now,
                ReplyUser = (replyUser is null) ? null : replyUser,
            };
            articleRepository.Find(articleId).Comments.Add(comment);

            if (int.TryParse(replyMainCommentId, out int id))
            {
                Comment replyToComment = commentRepository.Find(id);

                // 判断被回复评论和其用户Id是否匹配，避免数据库污染
                if (replyMainCommentId == replyCommentId && replyToComment.Author.PersonalData.Nickname != replyUsername)
                { throw new ArgumentException(); }
                else if (commentRepository.Find(int.Parse(replyCommentId)).Author.PersonalData.Nickname != replyUsername)
                { throw new ArgumentException(); }
                else { replyToComment.Replys.Add(comment); }
            }// else nothing
            // 获取新发布主评论Id
            DbContext.SaveChanges();

            return Mapper.Map<CommentModel>(comment);
        }
    }
}
