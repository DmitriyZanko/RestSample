namespace RestSample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePizzaDbWithWeight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopPizzas", "Weight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopPizzas", "Weight");
        }
    }
}
