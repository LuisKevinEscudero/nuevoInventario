namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class putTheCorrectNamesToModelAndCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemCategories", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ItemModels", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.ItemCategories", "Category");
            DropColumn("dbo.ItemModels", "Model");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemModels", "Model", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ItemCategories", "Category", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.ItemModels", "Name");
            DropColumn("dbo.ItemCategories", "Name");
        }
    }
}
