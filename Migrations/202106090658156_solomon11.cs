namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReceiptDetails", "createddate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReceiptDetails", "createddate", c => c.DateTime(nullable: false));
        }
    }
}
