using GestaoEscolar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GestaoEscolar.DAO
{
    public class TurmaDAO
    {
        private Contexto dao;

        public TurmaDAO(Contexto contexto)
        {
            this.dao = contexto;
        }

        public void Alterar(Turma turma)
        {
            dao.Entry(turma).State = EntityState.Modified;
            dao.SaveChanges();
        }

        public void Exlcuir(Turma turma)
        {
            dao.Turmas.Remove(turma);
            dao.SaveChanges();
        }

        public void Salvar(Turma novaTurma)
        {
            dao.Turmas.Add(novaTurma);
            dao.SaveChanges();

            var listaDisciplinas = listarDisciplina();

            foreach (var item in listaDisciplinas)
            {
                SalvarDisciplinaNaTurma(item, novaTurma.FuncionarioId, novaTurma);
            }
        }

        public IList<Disciplina> listarDisciplina()
        {
            return dao.Disciplinas.ToList();
        }        

        public void SalvarDisciplinaNaTurma(Disciplina disciplina, int professorId, Turma turma)
        {
            var novaDisciplinaProfessorTurma = new DisciplinaDoProfessorNaTurma();

            novaDisciplinaProfessorTurma.FuncionarioId = professorId;
            novaDisciplinaProfessorTurma.DisciplinaId = disciplina.Id;
            novaDisciplinaProfessorTurma.TurmaId = turma.Id;

            dao.DisciplinaDoProfessoresNasTurmas.Add(novaDisciplinaProfessorTurma);
            dao.SaveChanges();
        }
        
        public IList<Turma> Listar()
        {
            return dao.Turmas.ToList();
        }

        public Turma buscarTurmaId(int Id)
        {
            return dao.Turmas.FirstOrDefault( arg => arg.Id == Id);
        }
        
    }
}