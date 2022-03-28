namespace SRV.ServiceInterface
{
    public interface IEvaluationService
    {
        string Evaluate(int contentId, int userId, bool isAgree, bool isArticle);
    }
}
