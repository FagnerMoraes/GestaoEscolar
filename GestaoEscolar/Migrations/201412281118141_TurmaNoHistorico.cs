namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurmaNoHistorico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoricoAluno", "TurmaAnoHistorico", c => c.String(nullable: false, maxLength: 10, storeType: "nvarchar"));
            AlterColumn("dbo.Turma", "NomeTurma", c => c.String(nullable: false, maxLength: 40, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Turma", "NomeTurma", c => c.String(nullable: false, maxLength: 20, unicode: false));
            DropColumn("dbo.HistoricoAluno", "TurmaAnoHistorico");
        }
    }
}
