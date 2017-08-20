namespace GestaoEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aluno",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentificacaoUnica = c.String(maxLength: 11, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        Nis = c.String(maxLength: 25, unicode: false),
                        DataNascimento = c.DateTime(nullable: false, storeType: "date"),
                        Sexo = c.String(maxLength: 1, unicode: false),
                        EstadoCivilAluno = c.String(maxLength: 50, unicode: false),
                        ProfissaoAluno = c.String(maxLength: 200, unicode: false),
                        ReligiaoAluno = c.String(maxLength: 50, unicode: false),
                        RacaCor = c.String(maxLength: 50, unicode: false),
                        Situacao = c.String(maxLength: 50, unicode: false),
                        Responsavel = c.String(maxLength: 200, unicode: false),
                        ParentescoResponsavel = c.String(maxLength: 200, unicode: false),
                        NomePai = c.String(maxLength: 200, unicode: false),
                        ProfPai = c.String(maxLength: 200, unicode: false),
                        TelPai = c.String(maxLength: 20, unicode: false),
                        NomeMae = c.String(maxLength: 200, unicode: false),
                        ProfMae = c.String(maxLength: 200, unicode: false),
                        TelMae = c.String(maxLength: 20, unicode: false),
                        Nacionalidade = c.String(maxLength: 200, unicode: false),
                        PaisOrigem = c.String(maxLength: 200, unicode: false),
                        UfNaturalidade = c.String(maxLength: 2, unicode: false),
                        Naturalidade = c.String(maxLength: 200, unicode: false),
                        PessoaAutoBuscarAluno = c.String(maxLength: 200, unicode: false),
                        HorDispReunioesPaisAluno = c.String(maxLength: 3, unicode: false),
                        ProblemaSaudeAluno = c.String(maxLength: 3, unicode: false),
                        DescProblemaSaudeAluno = c.String(unicode: false, storeType: "text"),
                        MedicamentoUsadoAluno = c.String(maxLength: 1000, unicode: false),
                        AlergiasAluno = c.String(maxLength: 1000, unicode: false),
                        AcompFonoaudiologoAluno = c.String(maxLength: 3, unicode: false),
                        AcompPscicologoAluno = c.String(maxLength: 3, unicode: false),
                        AcompApaeAluno = c.String(maxLength: 3, unicode: false),
                        DefFisicaAluno = c.String(maxLength: 3, unicode: false),
                        DescDefFisicaAluno = c.String(maxLength: 3000, unicode: false),
                        DefVisualAluno = c.String(maxLength: 3, unicode: false),
                        DescDefVisualAluno = c.String(maxLength: 3000, unicode: false),
                        DefAuditivaAluno = c.String(maxLength: 3, unicode: false),
                        DescDefAuditivaAluno = c.String(maxLength: 3000, unicode: false),
                        AltaHabilidade = c.String(maxLength: 3, unicode: false),
                        DescAltaHabilidade = c.String(maxLength: 3000, unicode: false),
                        RecursoParticipacaoProvas = c.String(maxLength: 1000, unicode: false),
                        Rg = c.String(maxLength: 20, unicode: false),
                        OrgaoExpRg = c.String(maxLength: 100, unicode: false),
                        UfRg = c.String(maxLength: 2, unicode: false),
                        DataExpRg = c.DateTime(storeType: "date"),
                        CertidaoCivil = c.String(maxLength: 20, unicode: false),
                        TipoCertidaoCivil = c.String(maxLength: 300, unicode: false),
                        NumeroNovaCertidaoCivil = c.String(maxLength: 300, unicode: false),
                        NumeroDoTermo = c.String(maxLength: 100, unicode: false),
                        Folha = c.String(maxLength: 30, unicode: false),
                        Livro = c.String(maxLength: 30, unicode: false),
                        DataEmissaoCertidao = c.DateTime(storeType: "date"),
                        NomeCartorioCertidao = c.String(maxLength: 200, unicode: false),
                        Cpf = c.String(maxLength: 30, unicode: false),
                        LocalZonaResidencia = c.String(maxLength: 200, unicode: false),
                        CepResidencia = c.String(maxLength: 20, unicode: false),
                        EnderecoResidencia = c.String(maxLength: 102, unicode: false),
                        NumeroResidencia = c.String(maxLength: 20, unicode: false),
                        ComplementoResidencia = c.String(maxLength: 200, unicode: false),
                        BairroResidencia = c.String(maxLength: 200, unicode: false),
                        UfResidencia = c.String(maxLength: 2, unicode: false),
                        CidadeResidencia = c.String(maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoricoAluno",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnoHistoricoAluno = c.Int(nullable: false),
                        TurmaAnoHistorico = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        DataHistoricoAluno = c.DateTime(storeType: "date"),
                        EscolaAnoAluno = c.String(maxLength: 200, unicode: false),
                        CidadeEscolaAnoAluno = c.String(maxLength: 200, unicode: false),
                        UfEscolaAnoAluno = c.String(maxLength: 2, unicode: false),
                        ChCurricularBasica = c.String(maxLength: 20, unicode: false),
                        ChCurricularArtes = c.String(maxLength: 20, unicode: false),
                        ChCurricularInformatica = c.String(maxLength: 20, unicode: false),
                        ChCurricularLitInfantoJuvenil = c.String(maxLength: 20, unicode: false),
                        ChCurricularProjetoTempoIntegral = c.String(maxLength: 20, unicode: false),
                        QtdFaltaHoraAnoBasica = c.String(maxLength: 20, unicode: false),
                        QtdFaltaHoraAnoParte = c.String(maxLength: 20, unicode: false),
                        QtdFaltaHoraAnoProjeto = c.String(maxLength: 20, unicode: false),
                        DiasLetivos = c.Int(),
                        AprovPortugues = c.String(maxLength: 1, unicode: false),
                        AprovMatematica = c.String(maxLength: 1, unicode: false),
                        AprovGeografia = c.String(maxLength: 1, unicode: false),
                        AprovHistoria = c.String(maxLength: 1, unicode: false),
                        AprovCiencia = c.String(maxLength: 1, unicode: false),
                        AprovEduFisica = c.String(maxLength: 1, unicode: false),
                        AprovEnsReligioso = c.String(maxLength: 1, unicode: false),
                        AprovArte = c.String(maxLength: 1, unicode: false),
                        AprovInformatica = c.String(maxLength: 1, unicode: false),
                        AprovLitInfantilJuvenil = c.String(maxLength: 1, unicode: false),
                        AprovAtividadeLingMat = c.String(maxLength: 1, unicode: false),
                        AprovAtividadesEspMot = c.String(maxLength: 1, unicode: false),
                        AprovFormacaoPessoal = c.String(maxLength: 1, unicode: false),
                        SituacaoAluno = c.String(maxLength: 10, unicode: false),
                        AlunoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aluno", t => t.AlunoId, cascadeDelete: true)
                .Index(t => t.AlunoId);
            
            CreateTable(
                "dbo.Matricula",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlunoId = c.Int(nullable: false),
                        TurmaId = c.Int(),
                        AnoLetivoId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aluno", t => t.AlunoId, cascadeDelete: true)
                .ForeignKey("dbo.AnoLetivo", t => t.AnoLetivoId, cascadeDelete: true)
                .ForeignKey("dbo.Turma", t => t.TurmaId)
                .Index(t => t.AlunoId)
                .Index(t => t.TurmaId)
                .Index(t => t.AnoLetivoId);
            
            CreateTable(
                "dbo.AnoLetivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ano = c.String(nullable: false, maxLength: 10, unicode: false),
                        MediaAno = c.String(maxLength: 3, unicode: false),
                        QtdDiasLetivosAno = c.String(nullable: false, maxLength: 3, unicode: false),
                        ChCurrBas = c.Double(nullable: false),
                        ChCurrArt = c.Double(nullable: false),
                        ChCurrInf = c.Double(nullable: false),
                        ChCurrLiter = c.Double(nullable: false),
                        ChCurrArtProj = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conceito",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisciplinaId = c.Int(nullable: false),
                        MatriculaId = c.Int(nullable: false),
                        Conceito1Bim = c.String(maxLength: 1, unicode: false),
                        Conceito2Bim = c.String(maxLength: 1, unicode: false),
                        Conceito3Bim = c.String(maxLength: 1, unicode: false),
                        Conceito4Bim = c.String(maxLength: 1, unicode: false),
                        ConceitoFinal = c.String(maxLength: 1, unicode: false),
                        Faltas1Bim = c.String(maxLength: 3, unicode: false),
                        Faltas2Bim = c.String(maxLength: 3, unicode: false),
                        Faltas3Bim = c.String(maxLength: 3, unicode: false),
                        Faltas4Bim = c.String(maxLength: 3, unicode: false),
                        FaltasTotal = c.String(maxLength: 3, unicode: false),
                        Situacao = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplina", t => t.DisciplinaId, cascadeDelete: true)
                .ForeignKey("dbo.Matricula", t => t.MatriculaId, cascadeDelete: true)
                .Index(t => t.DisciplinaId)
                .Index(t => t.MatriculaId);
            
            CreateTable(
                "dbo.Disciplina",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeDisciplina = c.String(nullable: false, maxLength: 200, unicode: false),
                        MaximoPontos = c.String(nullable: false, maxLength: 2, unicode: false),
                        MediaPontos = c.String(nullable: false, maxLength: 2, unicode: false),
                        MinimoPonto = c.String(nullable: false, maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DisciplinaDoProfessorNaTurma",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisciplinaId = c.Int(nullable: false),
                        FuncionarioId = c.Int(nullable: false),
                        TurmaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplina", t => t.DisciplinaId, cascadeDelete: true)
                .ForeignKey("dbo.Turma", t => t.TurmaId, cascadeDelete: true)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .Index(t => t.DisciplinaId)
                .Index(t => t.FuncionarioId)
                .Index(t => t.TurmaId);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EscolaId = c.Int(nullable: false),
                        TipoFuncionarioId = c.Int(nullable: false),
                        NumRegistroFuncionario = c.String(maxLength: 100, unicode: false),
                        NomeFuncionario = c.String(maxLength: 200, unicode: false),
                        SexoFuncionario = c.String(maxLength: 1, unicode: false),
                        DataNascimentoFuncionario = c.DateTime(nullable: false, storeType: "date"),
                        CidadeEndFuncionario = c.String(maxLength: 200, unicode: false),
                        UfEndFuncionario = c.String(maxLength: 2, unicode: false),
                        LogradouroEndFuncionario = c.String(maxLength: 200, unicode: false),
                        BairroEndFuncionario = c.String(maxLength: 200, unicode: false),
                        NumLogradouroEndFuncionario = c.String(maxLength: 5, unicode: false),
                        CepEndFuncionario = c.String(maxLength: 10, unicode: false),
                        DataAdimissaoFuncionario = c.DateTime(storeType: "date"),
                        CpfFuncionario = c.String(maxLength: 20, unicode: false),
                        RgFuncionario = c.String(maxLength: 20, unicode: false),
                        UfRgFuncionario = c.String(maxLength: 2, unicode: false),
                        OrgaoRgFuncionario = c.String(maxLength: 200, unicode: false),
                        DataEmissaoRgFuncionario = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Escola", t => t.EscolaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoFuncionario", t => t.TipoFuncionarioId, cascadeDelete: true)
                .Index(t => t.EscolaId)
                .Index(t => t.TipoFuncionarioId);
            
            CreateTable(
                "dbo.Escola",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeEscola = c.String(nullable: false, maxLength: 200, unicode: false),
                        Cnpj = c.String(nullable: false, maxLength: 30, unicode: false),
                        NomeGestor = c.String(maxLength: 200, unicode: false),
                        CargoGestor = c.String(maxLength: 200, unicode: false),
                        CepEscola = c.String(maxLength: 30, unicode: false),
                        UfEscola = c.String(maxLength: 2, unicode: false),
                        CidadeEscola = c.String(maxLength: 200, unicode: false),
                        DistritoEscola = c.String(maxLength: 100, unicode: false),
                        EndEscola = c.String(maxLength: 300, unicode: false),
                        NumeroEndEscola = c.String(maxLength: 10, unicode: false),
                        ComplementoEndEscola = c.String(maxLength: 100, unicode: false),
                        BairroEndEscola = c.String(maxLength: 200, unicode: false),
                        TelefoneEscola = c.String(maxLength: 20, unicode: false),
                        FaxEscola = c.String(maxLength: 20, unicode: false),
                        EmailEscola = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Turma",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EscolaId = c.Int(nullable: false),
                        FuncionarioId = c.Int(nullable: false),
                        NomeTurma = c.String(nullable: false, maxLength: 200, unicode: false),
                        Serie = c.String(nullable: false, maxLength: 40, unicode: false),
                        NivelTurma = c.String(nullable: false, maxLength: 40, unicode: false),
                        HorarioFuncionamento = c.String(maxLength: 30, unicode: false),
                        ModalidadeEnsino = c.String(maxLength: 300, unicode: false),
                        QtdAlunos = c.String(maxLength: 3, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Escola", t => t.EscolaId, cascadeDelete: true)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .Index(t => t.EscolaId)
                .Index(t => t.FuncionarioId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 200, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 200, unicode: false),
                        EscolaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Escola", t => t.EscolaId)
                .Index(t => t.EscolaId);
            
            CreateTable(
                "dbo.TipoFuncionario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DescricaoFuncionario = c.String(nullable: false, maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConceitoFormacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatriculaId = c.Int(nullable: false),
                        Periodo = c.Int(nullable: false),
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
            DropForeignKey("dbo.Conceito", "MatriculaId", "dbo.Matricula");
            DropForeignKey("dbo.DisciplinaDoProfessorNaTurma", "FuncionarioId", "dbo.Funcionario");
            DropForeignKey("dbo.Funcionario", "TipoFuncionarioId", "dbo.TipoFuncionario");
            DropForeignKey("dbo.Usuario", "EscolaId", "dbo.Escola");
            DropForeignKey("dbo.Matricula", "TurmaId", "dbo.Turma");
            DropForeignKey("dbo.Turma", "FuncionarioId", "dbo.Funcionario");
            DropForeignKey("dbo.Turma", "EscolaId", "dbo.Escola");
            DropForeignKey("dbo.DisciplinaDoProfessorNaTurma", "TurmaId", "dbo.Turma");
            DropForeignKey("dbo.Funcionario", "EscolaId", "dbo.Escola");
            DropForeignKey("dbo.DisciplinaDoProfessorNaTurma", "DisciplinaId", "dbo.Disciplina");
            DropForeignKey("dbo.Conceito", "DisciplinaId", "dbo.Disciplina");
            DropForeignKey("dbo.Matricula", "AnoLetivoId", "dbo.AnoLetivo");
            DropForeignKey("dbo.Matricula", "AlunoId", "dbo.Aluno");
            DropForeignKey("dbo.HistoricoAluno", "AlunoId", "dbo.Aluno");
            DropIndex("dbo.ConceitoFormacao", new[] { "MatriculaId" });
            DropIndex("dbo.Usuario", new[] { "EscolaId" });
            DropIndex("dbo.Turma", new[] { "FuncionarioId" });
            DropIndex("dbo.Turma", new[] { "EscolaId" });
            DropIndex("dbo.Funcionario", new[] { "TipoFuncionarioId" });
            DropIndex("dbo.Funcionario", new[] { "EscolaId" });
            DropIndex("dbo.DisciplinaDoProfessorNaTurma", new[] { "TurmaId" });
            DropIndex("dbo.DisciplinaDoProfessorNaTurma", new[] { "FuncionarioId" });
            DropIndex("dbo.DisciplinaDoProfessorNaTurma", new[] { "DisciplinaId" });
            DropIndex("dbo.Conceito", new[] { "MatriculaId" });
            DropIndex("dbo.Conceito", new[] { "DisciplinaId" });
            DropIndex("dbo.Matricula", new[] { "AnoLetivoId" });
            DropIndex("dbo.Matricula", new[] { "TurmaId" });
            DropIndex("dbo.Matricula", new[] { "AlunoId" });
            DropIndex("dbo.HistoricoAluno", new[] { "AlunoId" });
            DropTable("dbo.ConceitoFormacao");
            DropTable("dbo.TipoFuncionario");
            DropTable("dbo.Usuario");
            DropTable("dbo.Turma");
            DropTable("dbo.Escola");
            DropTable("dbo.Funcionario");
            DropTable("dbo.DisciplinaDoProfessorNaTurma");
            DropTable("dbo.Disciplina");
            DropTable("dbo.Conceito");
            DropTable("dbo.AnoLetivo");
            DropTable("dbo.Matricula");
            DropTable("dbo.HistoricoAluno");
            DropTable("dbo.Aluno");
        }
    }
}
