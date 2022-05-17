namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjbfdsfsdiudgertgere4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieCountries",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.CountryId })
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieCountries", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieCountries", "CountryId", "dbo.Countries");
            DropIndex("dbo.MovieCountries", new[] { "CountryId" });
            DropIndex("dbo.MovieCountries", new[] { "MovieId" });
            DropTable("dbo.MovieCountries");
            DropTable("dbo.Countries");
        }
    }
}
