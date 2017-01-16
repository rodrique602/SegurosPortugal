namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_policyTocalCost_field_to_correct_name_policyTotalCost_in_AutomobilePolicy_Controller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "policyTotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AutomobilePolicy", "policyTocalCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutomobilePolicy", "policyTocalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AutomobilePolicy", "policyTotalCost");
        }
    }
}
