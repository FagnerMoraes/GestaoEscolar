using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class DisciplinaDoProfessorNaTurma
    {
        [Key]
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public int FuncionarioId { get; set; }
        public int TurmaId { get; set; }

        [ForeignKey("DisciplinaId")]
        public virtual Disciplina Disciplina { get; set; }
        [ForeignKey("FuncionarioId")]
        public virtual Funcionario Funcionario { get; set; }
        [ForeignKey("TurmaId")]
        public virtual Turma Turma { get; set; }

        
    }
}