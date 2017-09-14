using GestaoEscolar.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestaoEscolar.DAO
{
    public class RelatorioDAO
    {
        private Contexto contexto;

        public RelatorioDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public Matricula BuscaMatriculaPorID(int Id)
        {
            return contexto.Matriculas.FirstOrDefault(arg => arg.AlunoId == Id);
        }

        public IList<Conceito> ConceitosPorId(int Id)
        {
            return contexto.Conceitos.Where(arg => arg.Matricula.AlunoId == Id).ToList();
        }

        public IList<ConceitoFormacao> ConceitosFormacaoPorIdPeriodo(int Id,int periodo)
        {
            return contexto.ConceitoFormacaos.Where(arg => arg.Matricula.AlunoId == Id && arg.Periodo == periodo).ToList();
        }

        public Aluno BuscaAlunoPorId(int Id)
        {
            return contexto.Alunos.FirstOrDefault(arg => arg.Id == Id);
        }

        public Turma BuscaTurmaDoAluno(int Id)
        {
            return (from mat in contexto.Matriculas
                    join tur in contexto.Turmas on mat.TurmaId equals tur.Id
                    where mat.TurmaId == tur.Id && mat.AlunoId == Id
                    select tur).FirstOrDefault();
        }

        public IList<Aluno> ListaAlunoMatriculado()
        {
            return (from al in contexto.Alunos
                    join mat in contexto.Matriculas on al.Id equals mat.AlunoId
                    join tur in contexto.Turmas on mat.TurmaId equals tur.Id
                    where al.Id == mat.AlunoId && tur != null
                    select al).ToList();
        }

        public HistoricoAluno HstoricoPorPeriodo (string periodo, int Id)
        {
            return contexto.HistoricoAlunos.Include("Aluno").FirstOrDefault(x => x.TurmaAnoHistorico.Equals(periodo) && x.AlunoId == Id);
        }

        public IList<Conceito> BuscaNotasAlunoPorMatricula(Matricula matricula)
        {
            return (from con in contexto.Conceitos where con.MatriculaId == matricula.Id select con).ToList();
        }

        public IList<Turma> ListaTurmas()
        {
            return contexto.Turmas.ToList();
        }

        public IList<Matricula> BuscaMatricuaPorTurma(int? turmaId)
        {
            return contexto.Matriculas.Where(arg => arg.TurmaId == turmaId).ToList();
        }

    }
}