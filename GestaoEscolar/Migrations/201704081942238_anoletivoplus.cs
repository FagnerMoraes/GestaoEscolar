namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anoletivoplus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnoLetivo", "Ano", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.AnoLetivo", "QtdDiasLetivosAno", c => c.String(nullable: false, maxLength: 3, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnoLetivo", "QtdDiasLetivosAno", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.AnoLetivo", "Ano", c => c.String(maxLength: 10, unicode: false));
        }
    }
}
