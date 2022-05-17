namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsflmmvvv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteActors",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ActorId })
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.FavoriteDirectors",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DirectorId })
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.FavoriteMovies",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteMovies", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavoriteMovies", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.FavoriteDirectors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavoriteDirectors", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.FavoriteActors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavoriteActors", "ActorId", "dbo.Actors");
            DropIndex("dbo.FavoriteMovies", new[] { "MovieId" });
            DropIndex("dbo.FavoriteMovies", new[] { "UserId" });
            DropIndex("dbo.FavoriteDirectors", new[] { "DirectorId" });
            DropIndex("dbo.FavoriteDirectors", new[] { "UserId" });
            DropIndex("dbo.FavoriteActors", new[] { "ActorId" });
            DropIndex("dbo.FavoriteActors", new[] { "UserId" });
            DropTable("dbo.FavoriteMovies");
            DropTable("dbo.FavoriteDirectors");
            DropTable("dbo.FavoriteActors");
        }
    }
}
