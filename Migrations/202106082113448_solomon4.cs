namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PlanMaster", "SchemeID");
            AddForeignKey("dbo.PlanMaster", "SchemeID", "dbo.SchemeMaster", "SchemeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanMaster", "SchemeID", "dbo.SchemeMaster");
            DropIndex("dbo.PlanMaster", new[] { "SchemeID" });
        }
    }
}
