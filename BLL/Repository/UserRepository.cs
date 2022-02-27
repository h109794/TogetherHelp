using BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(SqlDbContext sqlContext) : base(sqlContext) { }

        public User GetByName(string name)
        {
            return sqlDbContext.Users.Where(m => m.Username == name).SingleOrDefault();
        }
    }
}
