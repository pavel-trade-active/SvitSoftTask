namespace SvitSoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hzwhat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Orders", "ClientName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "ClientName", c => c.String());
            DropColumn("dbo.Orders", "PhoneNumber");
        }
    }
}
