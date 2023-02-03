namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingRecords : DbMigration
    {
        public override void Up()
        {
            Sql("DBCC CHECKIDENT ('ItemCategories', RESEED, 0)");
            Sql("DBCC CHECKIDENT ('ItemModels', RESEED, 0)");
            Sql("DBCC CHECKIDENT ('Items', RESEED, 0)");

            Sql("INSERT INTO ItemCategories (Name) VALUES ('Computers')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Printers')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Monitors')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Scanners')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Keyboards')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Mice')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Speakers')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Headphones')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Webcams')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Microphones')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Projectors')");
            Sql("INSERT INTO ItemCategories (Name) VALUES ('Other')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Dell')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('HP')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Lenovo')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Acer')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Asus')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Apple')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Samsung')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('LG')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Sony')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Canon')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Epson')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Brother')");
            Sql("INSERT INTO ItemModels (Name) VALUES ('Other')");

            Sql("INSERT INTO Items (Name, Description, Quantity, LastUpdated, IdCategory, Brand, IdModel, SerialNumber, Location, Status, Notes, AddDate, Stock, Price)" +
                "VALUES('Item1', 'Description1', 10, '2022-01-01', '1', '1', 1, 'Serial1', 'Location1', 'Status1', 'Notes1', '2021-01-01', 100, 10.0)," +
                "('Item2', 'Description2', 20, '2022-02-01', '2', '2', 2, 'Serial2', 'Location2', 'Status2', 'Notes2', '2021-02-01', 200, 20.0)," +
                "('Item3', 'Description3', 30, '2022-03-01', '3', '3', 3, 'Serial3', 'Location3', 'Status3', 'Notes3', '2021-03-01', 300, 30.0)," +
                "('Item4', 'Description4', 40, '2022-04-01', '4', '4', 4, 'Serial4', 'Location4', 'Status4', 'Notes4', '2021-04-01', 400, 40.0)," +
                "('Item5', 'Description5', 50, '2022-05-01', '5', '5', 5, 'Serial5', 'Location5', 'Status5', 'Notes5', '2021-05-01', 500, 50.0); "
                );
        }
        
        public override void Down()
        {
        }
    }
}
