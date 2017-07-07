using GestaoEscolar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GestaoEscolar.DAO
{
    public class AlunoDAO
    {
        private Contexto contexto;

        public AlunoDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Salvar(Aluno novoAluno)
        {
            contexto.Alunos.Add(novoAluno);
            contexto.SaveChanges();
        }

        public IList<Aluno> Lista()
        {
            return contexto.Alunos.OrderBy(a => a.Nome).ToList();
        }

        public Aluno BuscarAlunoId (int Id)
        {
            Aluno aluno = contexto.Alunos.FirstOrDefault(a => a.Id == Id);
            return aluno;
        }

        public IList<Aluno> BuscaAlunoTermo (string termo)
        {
            return contexto.Alunos
                .Where(a => a.Nome.ToUpper()
                .Contains(termo.ToUpper())).ToList();
        }

        public void Alterar (Aluno aluno)
        {
            contexto.Entry(aluno).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Excluir (Aluno aluno)
        {
            contexto.Alunos.Remove(aluno);
            contexto.SaveChanges();
        }

    }
}