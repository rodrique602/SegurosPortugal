namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_vehicleType_vehiclePlate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "vehicleType", c => c.String());
            AddColumn("dbo.AutomobilePolicy", "vehiclePlate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutomobilePolicy", "vehiclePlate");
            DropColumn("dbo.AutomobilePolicy", "vehicleType");
        }
    }
}
