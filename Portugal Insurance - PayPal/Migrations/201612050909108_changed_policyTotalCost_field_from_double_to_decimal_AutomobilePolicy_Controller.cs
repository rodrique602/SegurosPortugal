namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_policyTotalCost_field_from_double_to_decimal_AutomobilePolicy_Controller : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AutomobilePolicy", "policyTocalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AutomobilePolicy", "policyTocalCost", c => c.Double(nullable: false));
        }
    }
}
