namespace KhoiDepTraiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Type", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
