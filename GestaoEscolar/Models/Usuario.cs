using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Login { get; set; }

        [DisplayName("Senha")]
        public string Senha { get; set; }

        [DisplayName("Escola")]
        public int? EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public virtual Escola Escola { get; set; }


    }
}