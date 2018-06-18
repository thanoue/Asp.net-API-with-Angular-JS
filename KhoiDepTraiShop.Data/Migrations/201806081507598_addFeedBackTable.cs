namespace KhoiDepTraiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeedBackTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Subject = c.String(),
                        Email = c.String(nullable: false, maxLength: 250),
                        Message = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeedBacks");
        }
    }
}
