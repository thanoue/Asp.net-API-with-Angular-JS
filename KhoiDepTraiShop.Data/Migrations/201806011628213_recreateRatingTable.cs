namespace KhoiDepTraiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recreateRatingTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductRatings", "RatingTime", c => c.DateTime());
            AlterColumn("dbo.ProductRatings", "RatingPeopleName", c => c.String(nullable: false));
            AlterColumn("dbo.ProductRatings", "PublicDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductRatings", "PublicDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductRatings", "RatingPeopleName", c => c.String());
            AlterColumn("dbo.ProductRatings", "RatingTime", c => c.DateTime(nullable: false));
        }
    }
}
