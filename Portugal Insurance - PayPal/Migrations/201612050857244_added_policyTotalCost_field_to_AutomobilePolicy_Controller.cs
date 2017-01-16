namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_policyTotalCost_field_to_AutomobilePolicy_Controller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "policyTocalCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutomobilePolicy", "policyTocalCost");
        }
    }
}
