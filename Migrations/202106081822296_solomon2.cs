namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlanMaster", "CreateUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanMaster", "CreateUserID", c => c.Int(nullable: false));
        }
    }
}
