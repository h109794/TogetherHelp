using BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class KeywordRepository : Repository<Keyword>
    {
        public KeywordRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public Keyword GetByText(string text)
        {
            return sqlDbContext.Keywords.Where(k => k.Text == text).SingleOrDefault();
        }
    }
}
