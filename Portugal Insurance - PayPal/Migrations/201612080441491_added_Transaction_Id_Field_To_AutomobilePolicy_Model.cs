namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Transaction_Id_Field_To_AutomobilePolicy_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "TransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutomobilePolicy", "TransactionId");
        }
    }
}
