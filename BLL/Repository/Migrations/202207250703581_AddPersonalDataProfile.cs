namespace BLL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonalDataProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.personal_data", "Profile", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.personal_data", "Profile");
        }
    }
}
