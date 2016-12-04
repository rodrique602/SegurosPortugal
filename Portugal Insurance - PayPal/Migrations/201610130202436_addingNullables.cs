namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingNullables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutomobilePolicy", "clientID", "dbo.Client");
            DropIndex("dbo.AutomobilePolicy", new[] { "clientID" });
            AlterColumn("dbo.AutomobilePolicy", "vehicleValue", c => c.Int());
            AlterColumn("dbo.AutomobilePolicy", "clientID", c => c.Int());
            CreateIndex("dbo.AutomobilePolicy", "clientID");
            AddForeignKey("dbo.AutomobilePolicy", "clientID", "dbo.Client", "clientID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AutomobilePolicy", "clientID", "dbo.Client");
            DropIndex("dbo.AutomobilePolicy", new[] { "clientID" });
            AlterColumn("dbo.AutomobilePolicy", "clientID", c => c.Int(nullable: false));
            AlterColumn("dbo.AutomobilePolicy", "vehicleValue", c => c.Int(nullable: false));
            CreateIndex("dbo.AutomobilePolicy", "clientID");
            AddForeignKey("dbo.AutomobilePolicy", "clientID", "dbo.Client", "clientID", cascadeDelete: true);
        }
    }
}
