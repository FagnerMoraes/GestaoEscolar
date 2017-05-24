using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestaoEscolar.Models
{
    public class ConceitoFormacao
    {
        public int Id { get; set; }
        public int MatriculaId { get; set; }

        public int Periodo { get; set; }

        public string AtitVal { get; set; }
        public string CompAssid { get; set; }
        public string CriCriti { get; set; }
        public string PartFamilia { get; set; }

        [ForeignKey("MatriculaId")]
        public virtual Matricula Matricula { get; set; }
    }
}