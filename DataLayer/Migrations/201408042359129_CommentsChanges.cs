namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Post_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.BlogPosts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.BlogPosts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropColumn("dbo.Comments", "Post_Id");
        }
    }
}
