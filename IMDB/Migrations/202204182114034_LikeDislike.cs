namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeDislike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeDislikeMovies",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MovieId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeDislikeMovies", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeDislikeMovies", "MovieId", "dbo.Movies");
            DropIndex("dbo.LikeDislikeMovies", new[] { "MovieId" });
            DropIndex("dbo.LikeDislikeMovies", new[] { "UserId" });
            DropTable("dbo.LikeDislikeMovies");
        }
    }
}
