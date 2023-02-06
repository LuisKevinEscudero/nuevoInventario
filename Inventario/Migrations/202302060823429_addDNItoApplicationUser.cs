namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDNItoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DNI", c => c.String(nullable: false, maxLength: 9));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DNI");
        }
    }
}
