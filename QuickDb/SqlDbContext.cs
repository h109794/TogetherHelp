using BLL.Entity;
using System.Data.Entity;

namespace BLL.Repository
{
    class SqlDbContext : DbContext
    {
        private static SqlDbContext uniqueInstance;

        private SqlDbContext() : base("togetherhelp_demo") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<PersonalData> PersonalDatas { get; set; }

        internal static SqlDbContext GetInstance()
        {
            if (uniqueInstance is null)
            {
                uniqueInstance = new SqlDbContext();
            }
            return uniqueInstance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.InvitationCode).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasRequired(u => u.PersonalData).WithRequiredPrincipal(p => p.User);
            modelBuilder.Entity<User>().HasOptional(u => u.Contact).WithRequired(c => c.User);

            modelBuilder.Entity<Keyword>().Property(k => k.Text).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<Keyword>().HasIndex(k => k.Text).IsUnique();
            modelBuilder.Entity<Keyword>().HasMany(k => k.BelongTo).WithMany(a => a.Keywords);

            modelBuilder.Entity<Contact>().Property(c => c.EmailAddress).HasMaxLength(64);
            modelBuilder.Entity<Contact>().HasIndex(c => c.EmailAddress).IsUnique();

            modelBuilder.Entity<PersonalData>().Property(p => p.Nickname).HasMaxLength(32);
            modelBuilder.Entity<PersonalData>().HasIndex(p => p.Nickname).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
