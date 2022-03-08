namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmendCommentEntityStruct : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.comment", name: "Belong_Id", newName: "Comment_Id");
            RenameIndex(table: "dbo.comment", name: "IX_Belong_Id", newName: "IX_Comment_Id");
            AddColumn("dbo.comment", "ReplyUsername", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.comment", "ReplyUsername");
            RenameIndex(table: "dbo.comment", name: "IX_Comment_Id", newName: "IX_Belong_Id");
            RenameColumn(table: "dbo.comment", name: "Comment_Id", newName: "Belong_Id");
        }
    }
}
