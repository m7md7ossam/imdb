namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dffdfdf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DirectorGenres", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.DirectorGenres", "GenreName", "dbo.Genres");
            DropForeignKey("dbo.MovieGenres", "GenreName", "dbo.Genres");
            DropForeignKey("dbo.MovieGenres", "MovieId", "dbo.Movies");
            DropIndex("dbo.DirectorGenres", new[] { "DirectorId" });
            DropIndex("dbo.DirectorGenres", new[] { "GenreName" });
            DropIndex("dbo.MovieGenres", new[] { "MovieId" });
            DropIndex("dbo.MovieGenres", new[] { "GenreName" });
            DropTable("dbo.DirectorGenres");
            DropTable("dbo.Genres");
            DropTable("dbo.MovieGenres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        GenreName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MovieId, t.GenreName });
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GenreName);
            
            CreateTable(
                "dbo.DirectorGenres",
                c => new
                    {
                        DirectorId = c.Int(nullable: false),
                        GenreName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DirectorId, t.GenreName });
            
            CreateIndex("dbo.MovieGenres", "GenreName");
            CreateIndex("dbo.MovieGenres", "MovieId");
            CreateIndex("dbo.DirectorGenres", "GenreName");
            CreateIndex("dbo.DirectorGenres", "DirectorId");
            AddForeignKey("dbo.MovieGenres", "MovieId", "dbo.Movies", "id", cascadeDelete: true);
            AddForeignKey("dbo.MovieGenres", "GenreName", "dbo.Genres", "GenreName", cascadeDelete: true);
            AddForeignKey("dbo.DirectorGenres", "GenreName", "dbo.Genres", "GenreName", cascadeDelete: true);
            AddForeignKey("dbo.DirectorGenres", "DirectorId", "dbo.Directors", "Id", cascadeDelete: true);
        }
    }
}
