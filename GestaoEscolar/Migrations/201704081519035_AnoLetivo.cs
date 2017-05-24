namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnoLetivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnoLetivo", "ChCurrBas", c => c.Double(nullable: false));
            AddColumn("dbo.AnoLetivo", "ChCurrArt", c => c.Double(nullable: false));
            AddColumn("dbo.AnoLetivo", "ChCurrInf", c => c.Double(nullable: false));
            AddColumn("dbo.AnoLetivo", "ChCurrLiter", c => c.Double(nullable: false));
            AddColumn("dbo.AnoLetivo", "ChCurrArtProj", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnoLetivo", "ChCurrArtProj");
            DropColumn("dbo.AnoLetivo", "ChCurrLiter");
            DropColumn("dbo.AnoLetivo", "ChCurrInf");
            DropColumn("dbo.AnoLetivo", "ChCurrArt");
            DropColumn("dbo.AnoLetivo", "ChCurrBas");
        }
    }
}
