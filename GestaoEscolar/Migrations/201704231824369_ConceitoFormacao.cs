namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConceitoFormacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConceitoFormacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatriculaId = c.Int(nullable: false),
                        Periodo = c.String(maxLength: 11, unicode: false),
                        AtitVal = c.String(maxLength: 3, unicode: false),
                        CompAssid = c.String(maxLength: 3, unicode: false),
                        CriCriti = c.String(maxLength: 3, unicode: false),
                        PartFamilia = c.String(maxLength: 3, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matricula", t => t.MatriculaId, cascadeDelete: true)
                .Index(t => t.MatriculaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConceitoFormacao", "MatriculaId", "dbo.Matricula");
            DropIndex("dbo.ConceitoFormacao", new[] { "MatriculaId" });
            DropTable("dbo.ConceitoFormacao");
        }
    }
}
