namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedAutomobilePolicyModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "paymentStatus", c => c.String());
            AddColumn("dbo.AutomobilePolicy", "payerEmail", c => c.String());
            AddColumn("dbo.AutomobilePolicy", "paymentFee", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutomobilePolicy", "paymentFee");
            DropColumn("dbo.AutomobilePolicy", "payerEmail");
            DropColumn("dbo.AutomobilePolicy", "paymentStatus");
        }
    }
}
