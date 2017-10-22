using GestaoEscolar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GestaoEscolar.DAO
{
    public class TurmaDAO
    {
        private Contexto contexto;

        public TurmaDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(Turma turma)
        {
            contexto.Entry(turma).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Exlcuir(Turma turma)
        {
            contexto.Turmas.Remove(turma);
            contexto.SaveChanges();
        }

        public void Salvar(Turma novaTurma)
        {
            contexto.Turmas.Add(novaTurma);
            contexto.SaveChanges();

            var listaDisciplinas = listarDisciplina();

            foreach (var item in listaDisciplinas)
            {
                SalvarDisciplinaNaTurma(item, novaTurma.FuncionarioId, novaTurma);
            }
        }

        public IList<Disciplina> listarDisciplina()
        {
            return contexto.Disciplinas.ToList();
        }

        public IList<DisciplinaDoProfessorNaTurma> listaDiscProfTurma( Turma turma)
        {
            return contexto.DisciplinaDoProfessoresNasTurmas.Where(x => x.TurmaId == turma.Id).ToList();
        }

        public IList<Aluno> listarAluno()
        {
            return contexto.Alunos.ToList();
        }

        public IList<Escola> listarEscola()
        {
            return contexto.Escolas.ToList();
        }

        public IList<Funcionario> listarProfessor()
        {
            return contexto.Funcionarios.Where(x => x.TipoFuncionario.DescricaoFuncionario.Contains("Professor")).ToList();
        }

        

        public void SalvarDisciplinaNaTurma(Disciplina disciplina, int professorId, Turma turma)
        {
            var novaDisciplinaProfessorTurma = new DisciplinaDoProfessorNaTurma();

            novaDisciplinaProfessorTurma.FuncionarioId = professorId;
            novaDisciplinaProfessorTurma.DisciplinaId = disciplina.Id;
            novaDisciplinaProfessorTurma.TurmaId = turma.Id;

            contexto.DisciplinaDoProfessoresNasTurmas.Add(novaDisciplinaProfessorTurma);
            contexto.SaveChanges();
        }
        
        public IList<Turma> ListarTurma()
        {
            return contexto.Turmas.Include("Escola").OrderBy(x => x.NomeTurma).ToList();
        }

        public Turma buscarTurmaId(int Id)
        {
            return contexto.Turmas.FirstOrDefault( arg => arg.Id == Id);
        }
        
    }
}