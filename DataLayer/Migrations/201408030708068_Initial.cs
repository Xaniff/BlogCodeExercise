namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.LoginKeyStores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthKey = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Passphrase = c.String(nullable: false),
                        Salt = c.String(nullable: false),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passwords", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.LoginKeyStores", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.BlogPosts", "Author_Id", "dbo.UserProfiles");
            DropIndex("dbo.Passwords", new[] { "UserProfile_Id" });
            DropIndex("dbo.LoginKeyStores", new[] { "UserProfile_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.BlogPosts", new[] { "Author_Id" });
            DropTable("dbo.Passwords");
            DropTable("dbo.LoginKeyStores");
            DropTable("dbo.Comments");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.BlogPosts");
        }
    }
}
