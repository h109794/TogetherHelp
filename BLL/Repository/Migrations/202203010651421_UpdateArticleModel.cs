namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateArticleModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.article", "Abstract", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.article", "Abstract");
        }
    }
}
