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

        public SqlDbContext() : base(connctionStr) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Keyword> Keywords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.InvitationCode).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<Keyword>().Property(k => k.Text).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<Keyword>().HasIndex(k => k.Text).IsUnique();
            modelBuilder.Entity<Keyword>().HasMany(k => k.BelongTo).WithMany(a => a.Keywords);

            base.OnModelCreating(modelBuilder);
        }
    }
}
