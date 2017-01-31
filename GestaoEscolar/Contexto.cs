using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GestaoEscolar.Models;

namespace GestaoEscolar
{
    public class Contexto : DbContext
    {
        public Contexto()
            : base("BDGestao")
        { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AnoLetivo> AnoLetivos { get; set; }
        public DbSet<Conceito> Conceitos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<DisciplinaDoProfessorNaTurma> DisciplinaDoProfessoresNasTurmas { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<HistoricoAluno> HistoricoAlunos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<TipoFuncionario> TipoFuncionarios { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();



            // Dados Alunos
            modelBuilder.Entity<Aluno>().Property(x => x.IdentificacaoUnica).HasColumnType("varchar").HasMaxLength(11);
            modelBuilder.Entity<Aluno>().Property(x => x.Nome).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.Nis).HasColumnType("varchar").HasMaxLength(25);
            modelBuilder.Entity<Aluno>().Property(x => x.DataNascimento).HasColumnType("date");
            modelBuilder.Entity<Aluno>().Property(x => x.Sexo).HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<Aluno>().Property(x => x.EstadoCivilAluno).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Aluno>().Property(x => x.ProfissaoAluno).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.ReligiaoAluno).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Aluno>().Property(x => x.RacaCor).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Aluno>().Property(x => x.Situacao).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Aluno>().Property(x => x.Responsavel).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.ParentescoResponsavel).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.NomePai).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.ProfPai).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.TelPai).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Aluno>().Property(x => x.NomeMae).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.ProfMae).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.TelMae).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Aluno>().Property(x => x.Nacionalidade).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.PaisOrigem).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.UfNaturalidade).HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Aluno>().Property(x => x.Naturalidade).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.PessoaAutoBuscarAluno).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.HorDispReunioesPaisAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.ProblemaSaudeAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.DescProblemaSaudeAluno).HasColumnType("text").HasMaxLength(3000);
            modelBuilder.Entity<Aluno>().Property(x => x.MedicamentoUsadoAluno).HasColumnType("varchar").HasMaxLength(1000);
            modelBuilder.Entity<Aluno>().Property(x => x.AlergiasAluno).HasColumnType("varchar").HasMaxLength(1000);
            modelBuilder.Entity<Aluno>().Property(x => x.AcompFonoaudiologoAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.AcompPscicologoAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.AcompApaeAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.DefFisicaAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.DescDefFisicaAluno).HasColumnType("varchar").HasMaxLength(3000);
            modelBuilder.Entity<Aluno>().Property(x => x.DefVisualAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.DescDefVisualAluno).HasColumnType("varchar").HasMaxLength(3000);
            modelBuilder.Entity<Aluno>().Property(x => x.DefAuditivaAluno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.DescDefAuditivaAluno).HasColumnType("varchar").HasMaxLength(3000);
            modelBuilder.Entity<Aluno>().Property(x => x.AltaHabilidade).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Aluno>().Property(x => x.DescAltaHabilidade).HasColumnType("varchar").HasMaxLength(3000);
            modelBuilder.Entity<Aluno>().Property(x => x.RecursoParticipacaoProvas).HasColumnType("varchar").HasMaxLength(1000);
            modelBuilder.Entity<Aluno>().Property(x => x.Rg).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Aluno>().Property(x => x.OrgaoExpRg).HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Aluno>().Property(x => x.UfRg).HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Aluno>().Property(x => x.DataExpRg).IsOptional().HasColumnType("date");
            modelBuilder.Entity<Aluno>().Property(x => x.CertidaoCivil).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Aluno>().Property(x => x.TipoCertidaoCivil).HasColumnType("varchar").HasMaxLength(300);
            modelBuilder.Entity<Aluno>().Property(x => x.NumeroNovaCertidaoCivil).HasColumnType("varchar").HasMaxLength(300);
            modelBuilder.Entity<Aluno>().Property(x => x.NumeroDoTermo).HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Aluno>().Property(x => x.Folha).HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Aluno>().Property(x => x.Livro).HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Aluno>().Property(x => x.DataEmissaoCertidao).IsOptional().HasColumnType("date");
            modelBuilder.Entity<Aluno>().Property(x => x.NomeCartorioCertidao).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.Cpf).HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Aluno>().Property(x => x.LocalZonaResidencia).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.CepResidencia).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Aluno>().Property(x => x.EnderecoResidencia).HasColumnType("varchar").HasMaxLength(102);
            modelBuilder.Entity<Aluno>().Property(x => x.NumeroResidencia).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Aluno>().Property(x => x.ComplementoResidencia).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.BairroResidencia).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Aluno>().Property(x => x.UfResidencia).HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Aluno>().Property(x => x.CidadeResidencia).HasColumnType("varchar").HasMaxLength(300);
            

            //Dados AnoLetivo
            modelBuilder.Entity<AnoLetivo>().Property(x => x.Ano).HasColumnType("varchar").HasMaxLength(10);
            modelBuilder.Entity<AnoLetivo>().Property(x => x.MediaAno).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<AnoLetivo>().Property(x => x.QtdDiasLetivosAno).HasColumnType("varchar").HasMaxLength(3);


            //Dados Conceito
            modelBuilder.Entity<Conceito>().Property(x => x.Conceito1Bim).HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<Conceito>().Property(x => x.Conceito2Bim).HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<Conceito>().Property(x => x.Conceito3Bim).HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<Conceito>().Property(x => x.Conceito4Bim).HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<Conceito>().Property(x => x.ConceitoFinal).HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<Conceito>().Property(x => x.Faltas1Bim).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Conceito>().Property(x => x.Faltas2Bim).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Conceito>().Property(x => x.Faltas3Bim).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Conceito>().Property(x => x.Faltas4Bim).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Conceito>().Property(x => x.FaltasTotal).HasColumnType("varchar").HasMaxLength(3);
            modelBuilder.Entity<Conceito>().Property(x => x.Situacao).HasColumnType("varchar").HasMaxLength(20);
            
            

            //Dados Disciplina

            modelBuilder.Entity<Disciplina>().Property(x => x.NomeDisciplina).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Disciplina>().Property(x => x.MaximoPontos).IsRequired().HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Disciplina>().Property(x => x.MediaPontos).IsRequired().HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Disciplina>().Property(x => x.MinimoPonto).IsRequired().HasColumnType("varchar").HasMaxLength(2);

            //Dados Escola
            modelBuilder.Entity<Escola>().Property(x => x.NomeEscola).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Escola>().Property(x => x.Cnpj).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Escola>().Property(x => x.NomeGestor).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Escola>().Property(x => x.CargoGestor).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Escola>().Property(x => x.CepEscola).HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Escola>().Property(x => x.UfEscola).HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Escola>().Property(x => x.CidadeEscola).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Escola>().Property(x => x.DistritoEscola).HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Escola>().Property(x => x.EndEscola).HasColumnType("varchar").HasMaxLength(300);
            modelBuilder.Entity<Escola>().Property(x => x.NumeroEndEscola).HasColumnType("varchar").HasMaxLength(10);
            modelBuilder.Entity<Escola>().Property(x => x.ComplementoEndEscola).HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Escola>().Property(x => x.BairroEndEscola).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Escola>().Property(x => x.TelefoneEscola).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Escola>().Property(x => x.FaxEscola).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Escola>().Property(x => x.EmailEscola).HasColumnType("varchar").HasMaxLength(200);

            //Dados Funcionario
            modelBuilder.Entity<Funcionario>().Property(x => x.NumRegistroFuncionario).HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Funcionario>().Property(x => x.NomeFuncionario).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Funcionario>().Property(x => x.SexoFuncionario).HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<Funcionario>().Property(x => x.DataNascimentoFuncionario).HasColumnType("date");
            modelBuilder.Entity<Funcionario>().Property(x => x.CidadeEndFuncionario).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Funcionario>().Property(x => x.UfEndFuncionario).HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Funcionario>().Property(x => x.LogradouroEndFuncionario).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Funcionario>().Property(x => x.BairroEndFuncionario).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Funcionario>().Property(x => x.NumLogradouroEndFuncionario).HasColumnType("varchar").HasMaxLength(5);
            modelBuilder.Entity<Funcionario>().Property(x => x.CepEndFuncionario).HasColumnType("varchar").HasMaxLength(10);
            modelBuilder.Entity<Funcionario>().Property(x => x.DataAdimissaoFuncionario).HasColumnType("date");
            modelBuilder.Entity<Funcionario>().Property(x => x.CpfFuncionario).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Funcionario>().Property(x => x.RgFuncionario).HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<Funcionario>().Property(x => x.UfRgFuncionario).HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<Funcionario>().Property(x => x.OrgaoRgFuncionario).HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Funcionario>().Property(x => x.DataEmissaoRgFuncionario).HasColumnType("date");

            //dados HistoricoAluno
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AnoHistoricoAluno).IsRequired();
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.TurmaAnoHistorico).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.DataHistoricoAluno).IsOptional().HasColumnType("date");
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.EscolaAnoAluno).IsOptional().HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.CidadeEscolaAnoAluno).IsOptional().HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.UfEscolaAnoAluno).IsOptional().HasColumnType("varchar").HasMaxLength(2);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.ChCurricularBasica).IsOptional().HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.ChCurricularArtes).IsOptional().HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.ChCurricularInformatica).IsOptional().HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.ChCurricularLitInfantoJuvenil).IsOptional().HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.ChCurricularProjetoTempoIntegral).IsOptional().HasColumnType("varchar").HasMaxLength(20);



            modelBuilder.Entity<HistoricoAluno>().Property(x => x.QtdFaltaHoraAnoBasica).IsOptional().HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.QtdFaltaHoraAnoParte).IsOptional().HasColumnType("varchar").HasMaxLength(20);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.QtdFaltaHoraAnoProjeto).IsOptional().HasColumnType("varchar").HasMaxLength(20);

            modelBuilder.Entity<HistoricoAluno>().Property(x => x.DiasLetivos).IsOptional();
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovPortugues).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovMatematica).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovGeografia).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovHistoria).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovCiencia).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovEduFisica).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovEnsReligioso).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovArte).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovInformatica).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovLitInfantilJuvenil).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovAtividadeLingMat).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovAtividadesEspMot).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.AprovFormacaoPessoal).IsOptional().HasColumnType("varchar").HasMaxLength(1);
            modelBuilder.Entity<HistoricoAluno>().Property(x => x.SituacaoAluno).IsOptional().HasColumnType("varchar").HasMaxLength(10);

            //Matricula
           modelBuilder.Entity<Matricula>().Property(x => x.DataCadastro).HasColumnType("date");
           //modelBuilder.Entity<Matricula>().HasOptional(x => x.Turma);


            //TipoFuncionario
            modelBuilder.Entity<TipoFuncionario>().Property(x => x.DescricaoFuncionario).IsRequired().HasColumnType("varchar").HasMaxLength(200);

            //Dados Turma
            modelBuilder.Entity<Turma>().Property(x => x.NomeTurma).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Turma>().Property(x => x.NivelTurma).IsRequired().HasColumnType("varchar").HasMaxLength(40);
            modelBuilder.Entity<Turma>().Property(x => x.Serie).IsRequired().HasColumnType("varchar").HasMaxLength(40);
            modelBuilder.Entity<Turma>().Property(x => x.HorarioFuncionamento).IsOptional().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Turma>().Property(x => x.ModalidadeEnsino).IsOptional().HasColumnType("varchar").HasMaxLength(300);
            modelBuilder.Entity<Turma>().Property(x => x.QtdAlunos).HasColumnType("varchar").HasMaxLength(3);
            
            //Dados Usuario
            modelBuilder.Entity<Usuario>().Property(x => x.Login).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).IsRequired().HasColumnType("varchar").HasMaxLength(200);
        }


    }
}