namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmendCommentModelAndAddPersonalDataNicknameUniqueConstraint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.comment", "ReplyUser_Id", c => c.Int());
            AlterColumn("dbo.personal_data", "Nickname", c => c.String(maxLength: 16));
            CreateIndex("dbo.personal_data", "Nickname", unique: true);
            CreateIndex("dbo.comment", "ReplyUser_Id");
            AddForeignKey("dbo.comment", "ReplyUser_Id", "dbo.user", "Id");
            DropColumn("dbo.comment", "ReplyUsername");
        }
        
        public override void Down()
        {
            AddColumn("dbo.comment", "ReplyUsername", c => c.String());
            DropForeignKey("dbo.comment", "ReplyUser_Id", "dbo.user");
            DropIndex("dbo.comment", new[] { "ReplyUser_Id" });
            DropIndex("dbo.personal_data", new[] { "Nickname" });
            AlterColumn("dbo.personal_data", "Nickname", c => c.String());
            DropColumn("dbo.comment", "ReplyUser_Id");
        }
    }
}
