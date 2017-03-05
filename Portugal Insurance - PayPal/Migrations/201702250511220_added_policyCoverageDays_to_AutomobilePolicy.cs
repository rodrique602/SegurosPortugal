namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_policyCoverageDays_to_AutomobilePolicy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutomobilePolicy", "policyCoverageDays", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutomobilePolicy", "policyCoverageDays");
        }
    }
}
