namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeItemModelAndAddSomeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "IdCategory", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "IdModel", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Category_Id", c => c.Int());
            AddColumn("dbo.Items", "Model_Id", c => c.Int());
            CreateIndex("dbo.Items", "Category_Id");
            CreateIndex("dbo.Items", "Model_Id");
            AddForeignKey("dbo.Items", "Category_Id", "dbo.ItemCategories", "Id");
            AddForeignKey("dbo.Items", "Model_Id", "dbo.ItemModels", "Id");
            DropColumn("dbo.Items", "Category");
            DropColumn("dbo.Items", "Model");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Model", c => c.String(maxLength: 100));
            AddColumn("dbo.Items", "Category", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.Items", "Model_Id", "dbo.ItemModels");
            DropForeignKey("dbo.Items", "Category_Id", "dbo.ItemCategories");
            DropIndex("dbo.Items", new[] { "Model_Id" });
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropColumn("dbo.Items", "Model_Id");
            DropColumn("dbo.Items", "Category_Id");
            DropColumn("dbo.Items", "IdModel");
            DropColumn("dbo.Items", "IdCategory");
            DropTable("dbo.ItemModels");
            DropTable("dbo.ItemCategories");
        }
    }
}
