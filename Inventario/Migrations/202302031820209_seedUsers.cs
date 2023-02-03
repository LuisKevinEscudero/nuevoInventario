namespace Inventario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2c3e78d3-1944-4299-b1a1-0ed2d24f220d', N'guest@vidly.com', 0, N'AOD3jOdsBrstJX0ql0ReaP5jIs4U6D7V6gcDiUsl1H+Y1UNxMUA4HffmAr4OexFUBQ==', N'6ebf628a-fba0-4712-b8ad-0573b7f11ee1', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6529f491-0e9d-4a5c-a527-3911d1c3c0dd', N'admin@vidly.com', 0, N'AFDkWuq+uLT51OHD50f4w9ABjwMbepymLJEigcM8q5JMoJq6KXVx4XOKaR2X0I/gBw==', N'aca3dd8a-35af-4f93-9e78-937d78fb856f', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3c9febc6-ae20-4db3-9961-636a19808ca5', N'Admin')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6529f491-0e9d-4a5c-a527-3911d1c3c0dd', N'3c9febc6-ae20-4db3-9961-636a19808ca5')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
