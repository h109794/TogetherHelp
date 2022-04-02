using BLL.Repository;

namespace QuickDb
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlDbContext sqlDbContext = SqlDbContext.GetInstance();
            sqlDbContext.Database.Delete();
            sqlDbContext.Database.Create();

            UserFactory.GenerateUsers();
            ContactFactory.GenerateContacts();
            KeywordFactory.GenerateKeywords();
            ArticleFactory.GenerateArticles();
            CommentFactory.GenerateComments();
            EvaluationFactory.GenerateEvaluations();

            sqlDbContext.SaveChanges();
        }
    }
}
