namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberRegistration", "PeriodID", c => c.Long(nullable: false));
            DropColumn("dbo.MemberRegistration", "Createdby");
            DropColumn("dbo.PlanMaster", "PeriodID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanMaster", "PeriodID", c => c.Int(nullable: false));
            AddColumn("dbo.MemberRegistration", "Createdby", c => c.Long(nullable: false));
            DropColumn("dbo.MemberRegistration", "PeriodID");
        }
    }
}
