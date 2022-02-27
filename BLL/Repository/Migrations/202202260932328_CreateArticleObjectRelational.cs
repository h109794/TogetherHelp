namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateArticleObjectRelational : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        Agree = c.Int(nullable: false),
                        DisAgree = c.Int(nullable: false),
                        PublishTime = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.user", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Agree = c.Int(nullable: false),
                        DisAgree = c.Int(nullable: false),
                        PublishTime = c.DateTime(nullable: false),
                        Belong_Id = c.Int(),
                        User_Id = c.Int(),
                        Article_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.comment", t => t.Belong_Id)
                .ForeignKey("dbo.user", t => t.User_Id)
                .ForeignKey("dbo.article", t => t.Article_Id)
                .Index(t => t.Belong_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.keyword",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 32),
                        UseCount = c.Int(nullable: false),
                        UpperKeyword_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.keyword", t => t.UpperKeyword_Id)
                .Index(t => t.Text, unique: true)
                .Index(t => t.UpperKeyword_Id);
            
            CreateTable(
                "dbo.KeywordArticles",
                c => new
                    {
                        Keyword_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Keyword_Id, t.Article_Id })
                .ForeignKey("dbo.keyword", t => t.Keyword_Id, cascadeDelete: true)
                .ForeignKey("dbo.article", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Keyword_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.article", "User_Id", "dbo.user");
            DropForeignKey("dbo.keyword", "UpperKeyword_Id", "dbo.keyword");
            DropForeignKey("dbo.KeywordArticles", "Article_Id", "dbo.article");
            DropForeignKey("dbo.KeywordArticles", "Keyword_Id", "dbo.keyword");
            DropForeignKey("dbo.comment", "Article_Id", "dbo.article");
            DropForeignKey("dbo.comment", "User_Id", "dbo.user");
            DropForeignKey("dbo.comment", "Belong_Id", "dbo.comment");
            DropIndex("dbo.KeywordArticles", new[] { "Article_Id" });
            DropIndex("dbo.KeywordArticles", new[] { "Keyword_Id" });
            DropIndex("dbo.keyword", new[] { "UpperKeyword_Id" });
            DropIndex("dbo.keyword", new[] { "Text" });
            DropIndex("dbo.comment", new[] { "Article_Id" });
            DropIndex("dbo.comment", new[] { "User_Id" });
            DropIndex("dbo.comment", new[] { "Belong_Id" });
            DropIndex("dbo.article", new[] { "User_Id" });
            DropTable("dbo.KeywordArticles");
            DropTable("dbo.keyword");
            DropTable("dbo.comment");
            DropTable("dbo.article");
        }
    }
}
