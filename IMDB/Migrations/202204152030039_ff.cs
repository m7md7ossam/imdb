namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        GenreName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MovieId, t.GenreName })
                .ForeignKey("dbo.Genres", t => t.GenreName, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.GenreName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieGenres", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieGenres", "GenreName", "dbo.Genres");
            DropIndex("dbo.MovieGenres", new[] { "GenreName" });
            DropIndex("dbo.MovieGenres", new[] { "MovieId" });
            DropTable("dbo.MovieGenres");
        }
    }
}
