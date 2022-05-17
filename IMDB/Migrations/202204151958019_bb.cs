namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DirectorGenres",
                c => new
                    {
                        DirectorId = c.Int(nullable: false),
                        GenreName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DirectorId, t.GenreName })
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreName, cascadeDelete: true)
                .Index(t => t.DirectorId)
                .Index(t => t.GenreName);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GenreName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DirectorGenres", "GenreName", "dbo.Genres");
            DropForeignKey("dbo.DirectorGenres", "DirectorId", "dbo.Directors");
            DropIndex("dbo.DirectorGenres", new[] { "GenreName" });
            DropIndex("dbo.DirectorGenres", new[] { "DirectorId" });
            DropTable("dbo.Genres");
            DropTable("dbo.DirectorGenres");
            DropTable("dbo.Directors");
            DropTable("dbo.Actors");
        }
    }
}
