using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.Models
{
    public class Disciplina
    {
        [Key]
        public int Id { get; set; }
        public string NomeDisciplina { get; set; }
        public string MaximoPontos { get; set; }
        public string MediaPontos { get; set; }
        public string MinimoPonto { get; set; }
        //public double ChDisciplina { get; set; }

        public virtual ICollection<DisciplinaDoProfessorNaTurma> DisciplinaDoProfessorNaTurmas { get; set; }
        public virtual ICollection<Conceito> Conceitos { get; set; } 

    }
}