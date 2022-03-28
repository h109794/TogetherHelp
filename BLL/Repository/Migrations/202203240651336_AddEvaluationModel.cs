namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEvaluationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.evaluation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsAgree = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        Comment_Id = c.Int(),
                        Article_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.comment", t => t.Comment_Id)
                .ForeignKey("dbo.article", t => t.Article_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.Article_Id);
            
            DropColumn("dbo.article", "Agree");
            DropColumn("dbo.article", "DisAgree");
            DropColumn("dbo.comment", "Agree");
            DropColumn("dbo.comment", "DisAgree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.comment", "DisAgree", c => c.Int(nullable: false));
            AddColumn("dbo.comment", "Agree", c => c.Int(nullable: false));
            AddColumn("dbo.article", "DisAgree", c => c.Int(nullable: false));
            AddColumn("dbo.article", "Agree", c => c.Int(nullable: false));
            DropForeignKey("dbo.evaluation", "Article_Id", "dbo.article");
            DropForeignKey("dbo.evaluation", "Comment_Id", "dbo.comment");
            DropIndex("dbo.evaluation", new[] { "Article_Id" });
            DropIndex("dbo.evaluation", new[] { "Comment_Id" });
            DropTable("dbo.evaluation");
        }
    }
}
