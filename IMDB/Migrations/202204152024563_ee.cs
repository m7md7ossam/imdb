namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieDirectors",
                c => new
                    {
                        DirectorId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DirectorId, t.MovieId })
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.DirectorId)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieDirectors", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieDirectors", "DirectorId", "dbo.Directors");
            DropIndex("dbo.MovieDirectors", new[] { "MovieId" });
            DropIndex("dbo.MovieDirectors", new[] { "DirectorId" });
            DropTable("dbo.MovieDirectors");
        }
    }
}
