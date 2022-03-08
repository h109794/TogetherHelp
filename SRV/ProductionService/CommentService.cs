using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;

namespace SRV.ProductionService
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentModel Publish(int articleId, int userId, string commentContent)
        {
            if (string.IsNullOrWhiteSpace(commentContent))
            {
                throw new ArgumentNullException("Comments are empty.");
            }

            UserRepository userRepository = new UserRepository(DbContext);
            ArticleRepository articleRepository = new ArticleRepository(DbContext);

            Comment comment = new Comment()
            {
                Author = userRepository.Find(userId),
                Body = commentContent,
                PublishTime = DateTime.Now,
            };
            articleRepository.Find(articleId).Comments.Add(comment);

            return Mapper.Map<CommentModel>(comment);
        }
    }
}
