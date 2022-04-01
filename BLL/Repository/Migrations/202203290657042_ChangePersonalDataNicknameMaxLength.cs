namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePersonalDataNicknameMaxLength : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.personal_data", new[] { "Nickname" });
            AlterColumn("dbo.personal_data", "Nickname", c => c.String(maxLength: 32));
            CreateIndex("dbo.personal_data", "Nickname", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.personal_data", new[] { "Nickname" });
            AlterColumn("dbo.personal_data", "Nickname", c => c.String(maxLength: 16));
            CreateIndex("dbo.personal_data", "Nickname", unique: true);
        }
    }
}
