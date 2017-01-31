using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }
        public int EscolaId { get; set; }
        public int TipoFuncionarioId { get; set; }
        [DisplayName("Registro")]
        public string NumRegistroFuncionario { get; set; }
        [DisplayName("Nome Completo")]
        public string NomeFuncionario { get; set; }
        [DisplayName("Sexo")]
        public string SexoFuncionario { get; set; }
        [DisplayName("Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimentoFuncionario { get; set; }
        [DisplayName("Cidade")]
        public string CidadeEndFuncionario { get; set; }
        [DisplayName("UF")]
        public string UfEndFuncionario { get; set; }
        [DisplayName("Logradouro")]
        public string LogradouroEndFuncionario { get; set; }
        [DisplayName("Bairro")]
        public string BairroEndFuncionario { get; set; }
        [DisplayName("Num.")]
        public string NumLogradouroEndFuncionario { get; set; }
        [DisplayName("CEP")]
        public string CepEndFuncionario { get; set; }
        [DisplayName("Data Admissão")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataAdimissaoFuncionario { get; set; }
        [DisplayName("CPF")]
        public string CpfFuncionario { get; set; }
        [DisplayName("RG")]
        public string RgFuncionario { get; set; }
        [DisplayName("UF")]
        public string UfRgFuncionario { get; set; }
        [DisplayName("Orgao Emissor")]
        public string OrgaoRgFuncionario { get; set; }
        [DisplayName("Data Emissão")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataEmissaoRgFuncionario { get; set; }

        [ForeignKey("EscolaId")]
        public virtual Escola Escola { get; set; }
        [ForeignKey("TipoFuncionarioId")]
        public virtual TipoFuncionario TipoFuncionario { get; set; }


        public virtual ICollection<Turma> Turmas { get; set; } 
    }
}