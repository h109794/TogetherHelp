namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactEmailUniqueConstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.contact", "EmailAddress", c => c.String(maxLength: 64));
            CreateIndex("dbo.contact", "EmailAddress", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.contact", new[] { "EmailAddress" });
            AlterColumn("dbo.contact", "EmailAddress", c => c.String());
        }
    }
}
