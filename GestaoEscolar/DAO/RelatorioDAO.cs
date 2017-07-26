using GestaoEscolar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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


    }
}