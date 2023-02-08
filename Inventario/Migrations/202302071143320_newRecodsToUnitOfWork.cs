namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newRecodsToUnitOfWork : DbMigration
    {
        public override void Up()
        {
            Sql("DBCC CHECKIDENT ('Items', RESEED, 0)");


            Sql("INSERT INTO [dbo].[Items] ([Name], [Description], [Quantity], [LastUpdated], [Brand], [SerialNumber], [Location], [Status], [Notes], [AddDate], [Stock], [Price], [IdCategory], [IdModel], [Category_Id], [Model_Id]) " +
                "VALUES " +
                "('Item1', 'Description1', 10, '2022-01-01 00:00:00', 'Brand1', 'Serial1', 'Location1', 'Status1', 'Notes1', '2021-01-01 00:00:00', 100, 10, 1, 1, 1, 1)," +
                "('Item2', 'Description2', 20, '2022-02-01 00:00:00', 'Brand2', 'Serial2', 'Location2', 'Status2', 'Notes2', '2021-02-01 00:00:00', 200, 20, 2, 2, 2, 2)," +
                "('Item3', 'Description3', 30, '2022-03-01 00:00:00', 'Brand3', 'Serial3', 'Location3', 'Status3', 'Notes3', '2021-03-01 00:00:00', 300, 30, 3, 3, 3, 3)," +
                "('Item4', 'Description4', 40, '2022-04-01 00:00:00', 'Brand4', 'Serial4', 'Location4', 'Status4', 'Notes4', '2021-04-01 00:00:00', 400, 40, 4, 4, 4, 4)," +
                "('Item5', 'Description5', 50, '2022-05-01 00:00:00', 'Brand5', 'Serial5', 'Location5', 'Status5', 'Notes5', '2021-05-01 00:00:00', 500, 50, 5, 5, 5, 5)," +
                "('Item6', 'Description6', 60, '2022-06-01 00:00:00', 'Brand6', 'Serial6', 'Location6', 'Status6', 'Notes6', '2021-06-01 00:00:00', 600, 60, 6, 6, 6, 6)," +
                "('Item7', 'Description7', 70, '2022-07-01 00:00:00', 'Brand7', 'Serial7', 'Location7', 'Status7', 'Notes7', '2021-07-01 00:00:00', 700, 70, 7, 7, 7, 7)," +
                "('Item8', 'Description8', 80, '2022-08-01 00:00:00', 'Brand8', 'Serial8', 'Location8', 'Status8', 'Notes8', '2021-08-01 00:00:00', 800, 80, 8, 8, 8, 8)," +
                "('Item9', 'Description9', 90, '2022-09-01 00:00:00', 'Brand9', 'Serial9', 'Location9', 'Status9', 'Notes9', '2021-09-01 00:00:00', 900, 90, 9, 9, 9, 9)," +
                "('Item10', 'Description10', 100, '2022-10-01 00:00:00', 'Brand10', 'Serial10', 'Location10', 'Status10', 'Notes10', '2021-10-01 00:00:00', 1000, 100, 10, 10, 10, 10);" +
                "");
        }
        
        public override void Down()
        {
        }
    }
}
