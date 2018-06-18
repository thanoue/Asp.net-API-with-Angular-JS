namespace KhoiDepTraiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Type", c => c.String(nullable: false));
        }
    }
}
