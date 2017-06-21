namespace Bardellino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slide : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Slide_Id = c.Int(nullable: false, identity: true),
                        Posizione = c.Int(nullable: false),
                        Testo = c.String(),
                        Immagine = c.String(),
                    })
                .PrimaryKey(t => t.Slide_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Slides");
        }
    }
}
