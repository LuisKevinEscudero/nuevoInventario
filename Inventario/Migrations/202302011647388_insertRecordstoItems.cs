namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertRecordstoItems : DbMigration
    {
        public override void Up()
        {
           Sql( "INSERT INTO " +
               "Items(Name, Description, Quantity, LastUpdated, Category, Brand, Model, SerialNumber, Location, Status, Notes, AddDate, Stock, Price) " +
               "VALUES " +
               "('Item 1', 'This is a sample item 1', 10, '2022-10-01', 'Electronics', 'Brand A', 'Model 1', 'Serial 1', 'Location 1', 'Active', 'Notes 1', '2022-01-01', 10, 100.00)," +
               "('Item 2', 'This is a sample item 2', 20, '2022-11-01', 'Furniture', 'Brand B', 'Model 2', 'Serial 2', 'Location 2', 'Inactive', 'Notes 2', '2022-02-01', 20, 200.00)," +
               "('Item 3', 'This is a sample item 3', 30, '2022-12-01', 'Clothing', 'Brand C', 'Model 3', 'Serial 3', 'Location 3', 'Active', 'Notes 3', '2022-03-01', 30, 300.00)," +
               "('Item 4', 'This is a sample item 4', 40, '2022-01-01', 'Books', 'Brand D', 'Model 4', 'Serial 4', 'Location 4', 'Inactive', 'Notes 4', '2022-04-01', 40, 400.00)," +
               "('Item 5', 'This is a sample item 5', 50, '2022-02-01', 'Electronics', 'Brand E', 'Model 5', 'Serial 5', 'Location 5', 'Active', 'Notes 5', '2022-05-01', 50, 500.00)," +
               "('Item 6', 'This is a sample item 6', 60, '2022-03-01', 'Furniture', 'Brand F', 'Model 6', 'Serial 6', 'Location 6', 'Inactive', 'Notes 6', '2022-06-01', 60, 600.00)," +
               "('Item 7', 'This is a sample item 7', 70, '2022-04-01', 'Clothing', 'Brand G', 'Model 7', 'Serial 7', 'Location 7', 'Active', 'Notes 7', '2022-07-01', 70, 700.00)," +
               "('Item 8', 'This is a sample item 8', 80, '2022-05-01', 'Books', 'Brand H', 'Model 8', 'Serial 8', 'Location 8', 'Inactive', 'Notes 8', '2022-08-01', 80, 800.00)," +
               "('Item 9', 'This is a sample item 9', 90, '2022-06-01', 'Electronics', 'Brand I', 'Model 9', 'Serial 9', 'Location 9', 'Active', 'Notes 9', '2022-09-01', 90, 900.00)," +
               "('Item 10', 'This is a sample item 10', 100, '2022-07-01', 'Furniture', 'Brand J', 'Model 10', 'Serial 10', 'Location 10', 'Inactive', 'Notes 10', '2022-10-01', 100, 1000.00);"
               );

        }

        public override void Down()
        {
        }
    }
}
