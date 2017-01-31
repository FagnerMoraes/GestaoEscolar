using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.Models
{
    public class TipoFuncionario
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tipo Funcionário")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string DescricaoFuncionario { get; set; }


        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        
    }
}