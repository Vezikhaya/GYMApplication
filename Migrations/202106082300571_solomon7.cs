namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberRegistration", "schememasters_SchemeID", "dbo.SchemeMaster");
            DropForeignKey("dbo.MemberRegistration", "planmaster_PlanID", "dbo.PlanMaster");
            DropIndex("dbo.MemberRegistration", new[] { "planmaster_PlanID" });
            DropIndex("dbo.MemberRegistration", new[] { "schememasters_SchemeID" });
            RenameColumn(table: "dbo.MemberRegistration", name: "planmaster_PlanID", newName: "PlanID");
            AlterColumn("dbo.MemberRegistration", "PlanID", c => c.Int(nullable: false));
            CreateIndex("dbo.MemberRegistration", "PlanID");
            AddForeignKey("dbo.MemberRegistration", "PlanID", "dbo.PlanMaster", "PlanID", cascadeDelete: true);
            DropColumn("dbo.MemberRegistration", "PlantypeID");
            DropColumn("dbo.MemberRegistration", "WorkouttypeID");
            DropColumn("dbo.MemberRegistration", "schememasters_SchemeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberRegistration", "schememasters_SchemeID", c => c.Int());
            AddColumn("dbo.MemberRegistration", "WorkouttypeID", c => c.Int(nullable: false));
            AddColumn("dbo.MemberRegistration", "PlantypeID", c => c.String());
            DropForeignKey("dbo.MemberRegistration", "PlanID", "dbo.PlanMaster");
            DropIndex("dbo.MemberRegistration", new[] { "PlanID" });
            AlterColumn("dbo.MemberRegistration", "PlanID", c => c.Int());
            RenameColumn(table: "dbo.MemberRegistration", name: "PlanID", newName: "planmaster_PlanID");
            CreateIndex("dbo.MemberRegistration", "schememasters_SchemeID");
            CreateIndex("dbo.MemberRegistration", "planmaster_PlanID");
            AddForeignKey("dbo.MemberRegistration", "planmaster_PlanID", "dbo.PlanMaster", "PlanID");
            AddForeignKey("dbo.MemberRegistration", "schememasters_SchemeID", "dbo.SchemeMaster", "SchemeID");
        }
    }
}
