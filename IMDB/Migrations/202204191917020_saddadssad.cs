namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saddadssad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "Age");
        }
    }
}
