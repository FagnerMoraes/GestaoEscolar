namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExigirDadoTumar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Turma", "QtdAlunos", c => c.String(nullable: false, maxLength: 3, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Turma", "QtdAlunos", c => c.String(maxLength: 3, unicode: false));
        }
    }
}
