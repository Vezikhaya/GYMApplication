namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SchemeMaster", "Createdby");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchemeMaster", "Createdby", c => c.Int());
        }
    }
}
