namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePersonalDataObjectRealtion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.personal_data",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Gender = c.Boolean(),
                        Nickname = c.String(),
                        Birthday = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.user", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.personal_data", "Id", "dbo.user");
            DropIndex("dbo.personal_data", new[] { "Id" });
            DropTable("dbo.personal_data");
        }
    }
}
