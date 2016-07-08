namespace Portugal_Insurance___PayPal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutomobilePolicy",
                c => new
                    {
                        automobilePolicyID = c.Int(nullable: false, identity: true),
                        vehicleValue = c.Int(nullable: false),
                        vehicleVin = c.String(),
                        carYear = c.String(),
                        carMake = c.String(),
                        carModel = c.String(),
                        policyFolio = c.String(),
                        policySold = c.Boolean(),
                        policySoldDate = c.DateTime(),
                        policyStartingDate = c.DateTime(),
                        policyEndingDate = c.DateTime(),
                        clientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.automobilePolicyID)
                .ForeignKey("dbo.Client", t => t.clientID, cascadeDelete: true)
                .Index(t => t.clientID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        clientID = c.Int(nullable: false, identity: true),
                        fullName = c.String(),
                        addressLine1 = c.String(),
                        addressLine2 = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zipCode = c.String(),
                        country = c.String(),
                        phoneNumber = c.String(),
                        emailAddress = c.String(),
                        licenseNumber1 = c.String(),
                        licenseNumber2 = c.String(),
                    })
                .PrimaryKey(t => t.clientID);
            
            CreateTable(
                "dbo.HomeCondoPolicy",
                c => new
                    {
                        policyQuoteID = c.Int(nullable: false, identity: true),
                        homeOwnerInsurance = c.Boolean(),
                        condoInsurance = c.Boolean(),
                        customerName = c.String(),
                        email = c.String(),
                        customerUsaAddress = c.String(),
                        zipCode = c.Double(),
                        city = c.String(),
                        phoneUsa = c.String(),
                        cellPhone = c.String(),
                        insuredBuildingLocation = c.String(),
                        floors = c.Int(),
                        typeOfConstruction = c.String(),
                        wall = c.String(),
                        roof = c.String(),
                        buildingsValue = c.Decimal(precision: 18, scale: 2),
                        contentsValue = c.Decimal(precision: 18, scale: 2),
                        burlglaryTheft = c.String(),
                        brokenGlass = c.String(),
                        familyCivilLiability = c.String(),
                        others = c.String(),
                        annualPremium = c.Decimal(precision: 18, scale: 2),
                        earthquake = c.Boolean(),
                    })
                .PrimaryKey(t => t.policyQuoteID);
            
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AutomobilePolicy", "clientID", "dbo.Client");
            DropIndex("dbo.AutomobilePolicy", new[] { "clientID" });
            DropTable("dbo.Precios");
            DropTable("dbo.HomeCondoPolicy");
            DropTable("dbo.Client");
            DropTable("dbo.AutomobilePolicy");
        }
    }
}
