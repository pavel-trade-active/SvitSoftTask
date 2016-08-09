namespace SvitSoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneNumberDelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "PhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PhoneNumber", c => c.String());
        }
    }
}
