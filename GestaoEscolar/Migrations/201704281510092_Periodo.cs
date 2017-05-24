namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Periodo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConceitoFormacao", "Periodo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConceitoFormacao", "Periodo", c => c.String(maxLength: 11, unicode: false));
        }
    }
}
