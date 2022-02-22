namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvitationCode = c.String(nullable: false),
                        Username = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false),
                        Inviter_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Inviter_Id)
                .Index(t => t.Username, unique: true)
                .Index(t => t.Inviter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Inviter_Id", "dbo.User");
            DropIndex("dbo.User", new[] { "Inviter_Id" });
            DropIndex("dbo.User", new[] { "Username" });
            DropTable("dbo.User");
        }
    }
}
