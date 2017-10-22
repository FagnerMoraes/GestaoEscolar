using GestaoEscolar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestaoEscolar.DAO
{
    public class ConceitoFormacaoDAO
    {
        private Contexto contexto;

        public ConceitoFormacaoDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public ConceitoFormacao BuscaConceitoFormacao(int MatriculaId, int periodo)
        {
            return contexto.ConceitoFormacaos.FirstOrDefault
                (x => x.MatriculaId == MatriculaId && x.Periodo == periodo);
        }

        public void Salvar(ConceitoFormacao novoconceitoformacao)
        {
            contexto.ConceitoFormacaos.Add(novoconceitoformacao);
            contexto.SaveChanges();
        }

        public void Alterar(ConceitoFormacao conceitoformacao)
        {
            contexto.Entry(conceitoformacao).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public Aluno BuscaAluno(ConceitoFormacao conceitoformacao)
        {
            return (from mat in contexto.Matriculas
                    join al in contexto.Alunos on mat.AlunoId equals al.Id
                    where mat.Id == conceitoformacao.MatriculaId
                    select al).FirstOrDefault();
        }
    }
}