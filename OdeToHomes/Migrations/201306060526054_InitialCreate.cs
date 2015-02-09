namespace OdeToHomes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FloorPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModelNum = c.String(),
                        Sqft = c.Int(nullable: false),
                        Beds = c.Int(nullable: false),
                        Baths = c.Int(nullable: false),
                        Size = c.String(),
                        Sections = c.Int(nullable: false),
                        OurPrice = c.Int(nullable: false),
                        TheirPrice = c.Int(nullable: false),
                        Series = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        State_Id = c.Int(), //why?
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.State_Id)
                .Index(t => t.State_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Series", new[] { "State_Id" });
            DropForeignKey("dbo.Series", "State_Id", "dbo.States");
            DropTable("dbo.Series");
            DropTable("dbo.States");
            DropTable("dbo.FloorPlans");
        }
    }
}
