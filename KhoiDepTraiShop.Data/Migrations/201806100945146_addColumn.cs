namespace KhoiDepTraiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "CridentialCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "CridentialCode");
        }
    }
}
