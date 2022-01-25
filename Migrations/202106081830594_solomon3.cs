namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlanMaster", "ModifyUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanMaster", "ModifyUserID", c => c.Int(nullable: false));
        }
    }
}
