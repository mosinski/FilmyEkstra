namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratunek : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Opis", c => c.String());
            AlterColumn("dbo.Movies", "ReleaseDate", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Movies", "ReleaseDate", c => c.String());
            DropColumn("dbo.Movies", "Opis");
        }
    }
}
