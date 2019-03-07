namespace Todo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSampleUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ecae62a3-c461-46b2-9e6f-7166badd68b5', N'Test2@Todo.com', 0, N'ABi2S+L1/Dn2oJ+x6U+N7anmgob6mZ+7E6U38JcI8nmeaguE5zCxruE9T1epf2Bfbw==', N'a0e14f87-5a5b-4dc7-aa1b-7f724fcdcadf', NULL, 0, 0, NULL, 1, 0, N'Test2')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f16e0ad5-2326-45b8-9d25-efb524d0f1f9', N'Test@Todo.com', 0, N'APUjwvMCLjjWpQhdRfCMdr/OtIEPpSkumX5ivjG4DfJzYYidipKUyCgwkp2D8lk9VA==', N'9f23a751-8c60-4e88-8cf9-263056b0b2ab', NULL, 0, 0, NULL, 1, 0, N'Test')
            ");
        }

        public override void Down()
        {
        }
    }
}
