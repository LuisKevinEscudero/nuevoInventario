namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newRecors : DbMigration
    {
        public override void Up()
        {
            Sql("DBCC CHECKIDENT ('Items', RESEED, 0)");


            Sql("INSERT INTO [dbo].[Items]" +
                "([Name], [Description], [Quantity], [LastUpdated], [Brand], [SerialNumber], " +
                "[Location], [Status], [Notes], [AddDate], [Stock], [Price], [IdCategory], [IdModel], " +
                "[Category_Id], [Model_Id])" +
                "VALUES " +
                "('Item1', 'Description1', 10, '2022-01-01 00:00:00', 'Brand1', 'Serial1', 'Location1', 'Status1', 'Notes1', '2021-01-01 00:00:00', 100, 10, 1, 1, 1, 1), " +
                "('Item2', 'Description2', 20, '2022-02-01 00:00:00', 'Brand2', 'Serial2', 'Location2', 'Status2', 'Notes2', '2021-02-01 00:00:00', 200, 20, 2, 2, 2, 2), " +
                "('Item3', 'Description3', 30, '2022-03-01 00:00:00', 'Brand3', 'Serial3', 'Location3', 'Status3', 'Notes3', '2021-03-01 00:00:00', 300, 30, 3, 3, 3, 3), " +
                "('Item4', 'Description4', 40, '2022-04-01 00:00:00', 'Brand4', 'Serial4', 'Location4', 'Status4', 'Notes4', '2021-04-01 00:00:00', 400, 40, 4, 4, 4, 4), " +
                "('Item5', 'Description5', 50, '2022-05-01 00:00:00', 'Brand5', 'Serial5', 'Location5', 'Status5', 'Notes5', '2021-05-01 00:00:00', 500, 50, 5, 5, 5, 5);");
        }
        
        public override void Down()
        {
        }
    }
}
