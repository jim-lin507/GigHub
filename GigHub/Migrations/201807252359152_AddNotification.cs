namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserNotifications", "UserId");
            RenameColumn(table: "dbo.UserNotifications", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.UserNotifications", name: "IX_User_Id", newName: "IX_UserId");
            DropPrimaryKey("dbo.UserNotifications");
            AddPrimaryKey("dbo.UserNotifications", new[] { "UserId", "NotificationId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserNotifications");
            AddPrimaryKey("dbo.UserNotifications", "UserId");
            RenameIndex(table: "dbo.UserNotifications", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserNotifications", name: "UserId", newName: "User_Id");
            AddColumn("dbo.UserNotifications", "UserId", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
