namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingPolicyTotalCostNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AutomobilePolicy", "policyTotalCost", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AutomobilePolicy", "policyTotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
