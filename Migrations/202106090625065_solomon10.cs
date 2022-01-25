namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberRegistration", "MainMemberID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberRegistration", "MainMemberID", c => c.Int(nullable: false));
        }
    }
}
