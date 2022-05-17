namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gkjnbvocibf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Directors", "FirstName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Directors", "LastName", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Directors", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Directors", "FirstName", c => c.String(nullable: false));
        }
    }
}
