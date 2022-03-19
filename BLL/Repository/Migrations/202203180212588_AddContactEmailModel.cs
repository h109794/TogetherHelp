namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactEmailModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.contact",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.user", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.contact", "Id", "dbo.user");
            DropIndex("dbo.contact", new[] { "Id" });
            DropTable("dbo.contact");
        }
    }
}
