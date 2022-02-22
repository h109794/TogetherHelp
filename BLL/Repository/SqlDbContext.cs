using BLL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class SqlDbContext : DbContext
    {
        private const string connctionStr = @"Data Source=(localdb)\MSSQLLocalDB;
                                              Initial Catalog=togetherhelp_debug";

        public SqlDbContext() : base(connctionStr)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().Property(t => t.InvitationCode).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.Username).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().Property(t => t.Password).IsRequired();
            modelBuilder.Entity<User>().HasIndex(t => t.Username).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
