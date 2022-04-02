using BLL.Entity;

namespace QuickDb
{
    static class EvaluationFactory
    {
        internal static void GenerateEvaluations()
        {
            ArticleFactory.Article1.Evaluations.Add(CreateEvaluation(1, true));
            ArticleFactory.Article1.Evaluations.Add(CreateEvaluation(3, false));

            ArticleFactory.Article2.Evaluations.Add(CreateEvaluation(2, true));
            ArticleFactory.Article2.Evaluations.Add(CreateEvaluation(4, true));

            ArticleFactory.Article3.Evaluations.Add(CreateEvaluation(1, true));
            ArticleFactory.Article3.Evaluations.Add(CreateEvaluation(3, true));
            ArticleFactory.Article3.Evaluations.Add(CreateEvaluation(4, true));

            CommentFactory.Comments[0].Evaluations.Add(CreateEvaluation(2, true));
            CommentFactory.Comments[0].Evaluations.Add(CreateEvaluation(3, true));
            CommentFactory.Comments[2].Evaluations.Add(CreateEvaluation(4, false));
        }

        private static Evaluation CreateEvaluation(int userId, bool isAgree)
        {
            return new Evaluation
            {
                UserId = userId,
                IsAgree = isAgree,
            };
        }
    }
}
