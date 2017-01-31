using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Conceito
    {
        [Key]
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public int MatriculaId { get; set; }

        public string Conceito1Bim { get; set; }
        public string Conceito2Bim { get; set; }
        public string Conceito3Bim { get; set; }
        public string Conceito4Bim { get; set; }
        public string ConceitoFinal { get; set; }

        public string Faltas1Bim { get; set; }
        public string Faltas2Bim { get; set; }
        public string Faltas3Bim { get; set; }
        public string Faltas4Bim { get; set; }
        public string FaltasTotal { get; set; }

        public string Situacao { get; set; }




        [ForeignKey("DisciplinaId")]
        public virtual Disciplina Disciplina { get; set; }
        [ForeignKey("MatriculaId")]
        public virtual Matricula Matricula { get; set; }
       

    }
}