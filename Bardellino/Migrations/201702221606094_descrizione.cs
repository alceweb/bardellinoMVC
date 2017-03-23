namespace Bardellino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descrizione : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promoes", "Descrizione", c => c.String());
            AddColumn("dbo.Servizis", "Descrizione", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Servizis", "Descrizione");
            DropColumn("dbo.Promoes", "Descrizione");
        }
    }
}
