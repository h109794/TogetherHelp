namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmendContentPropertyName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.comment", name: "User_Id", newName: "Author_Id");
            RenameColumn(table: "dbo.article", name: "User_Id", newName: "Author_Id");
            RenameIndex(table: "dbo.article", name: "IX_User_Id", newName: "IX_Author_Id");
            RenameIndex(table: "dbo.comment", name: "IX_User_Id", newName: "IX_Author_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.comment", name: "IX_Author_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.article", name: "IX_Author_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.article", name: "Author_Id", newName: "User_Id");
            RenameColumn(table: "dbo.comment", name: "Author_Id", newName: "User_Id");
        }
    }
}
