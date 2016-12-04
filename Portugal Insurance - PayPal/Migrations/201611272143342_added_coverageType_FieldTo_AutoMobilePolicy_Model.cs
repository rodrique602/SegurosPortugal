namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_coverageType_FieldTo_AutoMobilePolicy_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "coverageType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutomobilePolicy", "coverageType");
        }
    }
}
