namespace GYMApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solomon : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberRegistration",
                c => new
                    {
                        MemID = c.Long(nullable: false, identity: true),
                        MemberNo = c.String(maxLength: 20),
                        MemberFName = c.String(nullable: false, maxLength: 100),
                        MemberLName = c.String(nullable: false, maxLength: 100),
                        MemberMName = c.String(nullable: false, maxLength: 100),
                        DOB = c.DateTime(nullable: false),
                        Age = c.String(maxLength: 10),
                        Contactno = c.String(maxLength: 10),
                        EmailID = c.String(maxLength: 30),
                        Gender = c.String(maxLength: 30),
                        PlantypeID = c.String(),
                        WorkouttypeID = c.Int(nullable: false),
                        Createdby = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Address = c.String(nullable: false),
                        MainMemberID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemID);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        PaymentID = c.Long(nullable: false, identity: true),
                        PlanID = c.Int(),
                        WorkouttypeID = c.Int(),
                        Paymenttype = c.String(maxLength: 50),
                        PaymentFromdt = c.DateTime(),
                        PaymentTodt = c.DateTime(),
                        PaymentAmount = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        NextRenwalDate = c.DateTime(),
                        CreateDate = c.DateTime(),
                        CreateUserID = c.Int(),
                        ModifyDate = c.DateTime(),
                        ModifyUserID = c.Int(),
                        RecStatus = c.String(maxLength: 1),
                        MemberID = c.Long(),
                        MemberNo = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PaymentID);
            
            CreateTable(
                "dbo.PlanMaster",
                c => new
                    {
                        PlanID = c.Int(nullable: false, identity: true),
                        PlanName = c.String(maxLength: 50),
                        PlanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServicetaxAmout = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceTax = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        CreateUserID = c.Int(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        ModifyUserID = c.Int(nullable: false),
                        RecStatus = c.String(maxLength: 1),
                        SchemeID = c.Int(nullable: false),
                        PeriodID = c.Int(nullable: false),
                        TotalAmout = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServicetaxNo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PlanID);
            
            CreateTable(
                "dbo.ReceiptDetails",
                c => new
                    {
                        ReceiptID = c.Long(nullable: false, identity: true),
                        Memberid = c.Long(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        createddate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReceiptID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SchemeMaster",
                c => new
                    {
                        SchemeID = c.Int(nullable: false, identity: true),
                        SchemeName = c.String(maxLength: 50),
                        Createdby = c.Int(),
                        Createddate = c.DateTime(),
                        schemebit = c.Boolean(),
                    })
                .PrimaryKey(t => t.SchemeID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Phone = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.SchemeMaster");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ReceiptDetails");
            DropTable("dbo.PlanMaster");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.MemberRegistration");
        }
    }
}
