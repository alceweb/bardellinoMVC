namespace Bardellino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class publicaServizi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servizis", "Pubblica", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Servizis", "Pubblica");
        }
    }
}
