using BLL.Entity;
using System.Linq;

namespace BLL.Repository
{
    public class KeywordRepository : BaseRepository<Keyword>
    {
        public KeywordRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public Keyword GetByText(string text)
        {
            return sqlDbContext.Keywords.Where(k => k.Text == text).SingleOrDefault();
        }
    }
}
