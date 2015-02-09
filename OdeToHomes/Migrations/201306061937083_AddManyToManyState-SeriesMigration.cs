namespace OdeToHomes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyStateSeriesMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Series", "State_Id", "dbo.States");
            DropIndex("dbo.Series", new[] { "State_Id" });
            CreateTable(
                "dbo.SeriesStates",
                c => new
                    {
                        Series_Id = c.Int(nullable: false),
                        State_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Series_Id, t.State_Id })
                .ForeignKey("dbo.Series", t => t.Series_Id, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.State_Id, cascadeDelete: true)
                .Index(t => t.Series_Id)
                .Index(t => t.State_Id);
            
            DropColumn("dbo.Series", "State_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "State_Id", c => c.Int());
            DropIndex("dbo.SeriesStates", new[] { "State_Id" });
            DropIndex("dbo.SeriesStates", new[] { "Series_Id" });
            DropForeignKey("dbo.SeriesStates", "State_Id", "dbo.States");
            DropForeignKey("dbo.SeriesStates", "Series_Id", "dbo.Series");
            DropTable("dbo.SeriesStates");
            CreateIndex("dbo.Series", "State_Id");
            AddForeignKey("dbo.Series", "State_Id", "dbo.States", "Id");
        }
    }
}
