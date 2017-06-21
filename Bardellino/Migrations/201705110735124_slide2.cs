namespace Bardellino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slide2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Slides", "Immagine", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Slides", "Immagine", c => c.String(nullable: false));
        }
    }
}
