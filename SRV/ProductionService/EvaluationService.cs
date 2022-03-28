using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;

namespace SRV.ProductionService
{
    public class EvaluationService : BaseService, IEvaluationService
    {
        public string Evaluate(int contentId, int userId, bool isAgree, bool isArticle)
        {
            Content content;
            if (isArticle)
            {
                content = new ArticleRepository(DbContext).GetArticleIncludeEvaluation(contentId);
            }
            else { content = new CommentRepository(DbContext).Find(contentId); }

            Evaluation evaluation = content.Evaluations.Find(e => e.UserId == userId);
            if (evaluation is null)
            {
                Evaluation newEvaluation = new Evaluation() { UserId = userId, IsAgree = isAgree };
                content.Evaluations.Add(newEvaluation);
                return "insert";
            }
            else if (evaluation.IsAgree == isAgree)
            {
                new EvaluationRepository(DbContext).Delete(evaluation);
                return "delete";
            }
            else
            {
                evaluation.IsAgree = isAgree;
                return "update";
            }
        }
    }
}
