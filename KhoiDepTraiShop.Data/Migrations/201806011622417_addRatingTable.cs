namespace KhoiDepTraiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRatingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RatingTime = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        RatingContent = c.String(nullable: false, maxLength: 1000),
                        RatingScore = c.Int(),
                        RatingPeopleName = c.String(),
                        PublicDate = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductRatings", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductRatings", new[] { "ProductId" });
            DropTable("dbo.ProductRatings");
        }
    }
}
