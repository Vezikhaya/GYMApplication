namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberRegistration", "planmaster_PlanID", c => c.Int());
            AddColumn("dbo.MemberRegistration", "schememasters_SchemeID", c => c.Int());
            CreateIndex("dbo.MemberRegistration", "planmaster_PlanID");
            CreateIndex("dbo.MemberRegistration", "schememasters_SchemeID");
            AddForeignKey("dbo.MemberRegistration", "planmaster_PlanID", "dbo.PlanMaster", "PlanID");
            AddForeignKey("dbo.MemberRegistration", "schememasters_SchemeID", "dbo.SchemeMaster", "SchemeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberRegistration", "schememasters_SchemeID", "dbo.SchemeMaster");
            DropForeignKey("dbo.MemberRegistration", "planmaster_PlanID", "dbo.PlanMaster");
            DropIndex("dbo.MemberRegistration", new[] { "schememasters_SchemeID" });
            DropIndex("dbo.MemberRegistration", new[] { "planmaster_PlanID" });
            DropColumn("dbo.MemberRegistration", "schememasters_SchemeID");
            DropColumn("dbo.MemberRegistration", "planmaster_PlanID");
        }
    }
}
