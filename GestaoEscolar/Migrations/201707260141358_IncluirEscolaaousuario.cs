namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncluirEscolaaousuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "EscolaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuario", "EscolaId");
            AddForeignKey("dbo.Usuario", "EscolaId", "dbo.Escola", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "EscolaId", "dbo.Escola");
            DropIndex("dbo.Usuario", new[] { "EscolaId" });
            DropColumn("dbo.Usuario", "EscolaId");
        }
    }
}
