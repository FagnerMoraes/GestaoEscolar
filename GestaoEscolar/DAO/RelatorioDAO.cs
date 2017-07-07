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

    }
}