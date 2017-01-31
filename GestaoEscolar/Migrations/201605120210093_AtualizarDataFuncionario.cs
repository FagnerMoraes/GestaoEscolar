namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizarDataFuncionario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Funcionario", "DataAdimissaoFuncionario", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Funcionario", "DataEmissaoRgFuncionario", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Funcionario", "DataEmissaoRgFuncionario", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Funcionario", "DataAdimissaoFuncionario", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
