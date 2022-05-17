namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Rank = c.Double(nullable: false),
                        SequelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Movies", t => t.SequelId)
                .Index(t => t.SequelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "SequelId", "dbo.Movies");
            DropIndex("dbo.Movies", new[] { "SequelId" });
            DropTable("dbo.Movies");
        }
    }
}
