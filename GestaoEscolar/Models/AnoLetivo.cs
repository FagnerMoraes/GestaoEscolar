using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.Models
{
    public class AnoLetivo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ano { get; set; }
        [DisplayName("Média do Ano")]
        public string MediaAno { get; set; }
        [Required]
        [DisplayName("Dias Letivos")]
        public string QtdDiasLetivosAno { get; set; }
        [Required]
        [DisplayName("Carga Horária Básica")]
        public double ChCurrBas { get; set; }
        [Required]
        [DisplayName("Carga Horária Artes")]
        public double ChCurrArt { get; set; }
        public double ChCurrInf { get; set; }
        public double ChCurrLiter { get; set; }
        public double ChCurrArtProj { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}