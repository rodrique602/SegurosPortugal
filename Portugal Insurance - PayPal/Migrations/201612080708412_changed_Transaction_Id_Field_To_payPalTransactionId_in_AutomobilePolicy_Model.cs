namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_Transaction_Id_Field_To_payPalTransactionId_in_AutomobilePolicy_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "payPalTransactionId", c => c.String());
            DropColumn("dbo.AutomobilePolicy", "TransactionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutomobilePolicy", "TransactionId", c => c.String());
            DropColumn("dbo.AutomobilePolicy", "payPalTransactionId");
        }
    }
}
