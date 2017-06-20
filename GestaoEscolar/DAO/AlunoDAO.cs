using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoEscolar.DAO
{
    public class AlunoDAO
    {
        public AlunoDAO(AlunoContext context)
        {
            this.context = context;
        }
    }
}