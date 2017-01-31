using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }
        public int EscolaId { get; set; }
        
        [DisplayName("Professor Responsavel")]
        public int FuncionarioId { get; set; }
        public string NomeTurma { get; set; }
        public string Serie { get; set; }
        public string NivelTurma { get; set; }
        public string HorarioFuncionamento { get; set; } //matutino , verpertino
        public string ModalidadeEnsino { get; set; } // regular
        public string QtdAlunos { get; set; }
        
        [ForeignKey("EscolaId")]
        public virtual Escola Escola { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual Funcionario Funcionario { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; }
        public virtual ICollection<DisciplinaDoProfessorNaTurma> DisciplinaDoProfessorNaTurmas { get; set; }
       

    }
}