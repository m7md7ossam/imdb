namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yyaa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "SequelId", "dbo.Movies");
            DropIndex("dbo.Movies", new[] { "SequelId" });
            DropColumn("dbo.Movies", "SequelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "SequelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "SequelId");
            AddForeignKey("dbo.Movies", "SequelId", "dbo.Movies", "id");
        }
    }
}
