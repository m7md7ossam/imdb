namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Image", c => c.String());
            AddColumn("dbo.Directors", "Image", c => c.String());
            AddColumn("dbo.Movies", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Image");
            DropColumn("dbo.Directors", "Image");
            DropColumn("dbo.Actors", "Image");
        }
    }
}
