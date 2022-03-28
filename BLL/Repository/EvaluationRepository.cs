using BLL.Entity;

namespace BLL.Repository
{
    public class EvaluationRepository : BaseRepository<Evaluation>
    {
        public EvaluationRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }
    }
}
