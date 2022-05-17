namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sfkdfuiriewo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "FirstName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Actors", "LastName", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actors", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Actors", "FirstName", c => c.String(nullable: false));
        }
    }
}
