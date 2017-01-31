using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Matricula
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public int AlunoId { get; set; }
        public int? TurmaId { get; set; }
        public int AnoLetivoId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        [ForeignKey("AlunoId")]
        public virtual Aluno Aluno { get; set; }
        [ForeignKey("TurmaId")]
        public virtual Turma Turma { get; set; }
        [ForeignKey("AnoLetivoId")]
        public virtual AnoLetivo AnoLetivo { get; set; }


        public virtual ICollection<Conceito> Conceitos { get; set; } 

    }
}