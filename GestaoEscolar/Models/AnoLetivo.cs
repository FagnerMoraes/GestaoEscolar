using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.Models
{
    public class AnoLetivo
    {
        [Key]
        public int Id { get; set; }

        public string Ano { get; set; }
        public string MediaAno { get; set; }
        public string QtdDiasLetivosAno { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}