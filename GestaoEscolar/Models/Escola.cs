using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace GestaoEscolar.Models
{
    public class Escola
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nome da Escola")]
        [Required(ErrorMessage = "Nome da escola é obrigatório")]
        public string NomeEscola { get; set; }

        [DisplayName("CNPJ")]
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string Cnpj { get; set; }

        [DisplayName("Gestor da Escola")]
        public string NomeGestor { get; set; }

        [DisplayName("Cargo do Gestor")]
        public string CargoGestor { get; set; } // Diretor ou Outro Cargo

        [DisplayName("CEP")]
        public string CepEscola { get; set; }

        [DisplayName("UF")]
        public string UfEscola { get; set; }

        [DisplayName("Cidade")]
        public string CidadeEscola { get; set; }

        [DisplayName("Distrito")]
        public string DistritoEscola { get; set; } // verificar distritos

        [DisplayName("Logradouro")]
        public string EndEscola { get; set; } //Exemplos: Avenida das Palmeiras, Rua João Bosco

        [DisplayName("Numero")]
        public string NumeroEndEscola { get; set; }

        [DisplayName("Complemento")]
        public string ComplementoEndEscola { get; set; } //bloco, casa, fundos, sobrado, condomínio,quadra, lote, conjunto.

        [DisplayName("Bairro")]
        public string BairroEndEscola { get; set; }

        [DisplayName("Telefone")]
        public string TelefoneEscola { get; set; } // incluindo DDD

        [DisplayName("Fax")]
        public string FaxEscola { get; set; }

        [DisplayName("Email")]
        public string EmailEscola { get; set; }

        public virtual ICollection<Turma> Turmas { get; set; } 
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}