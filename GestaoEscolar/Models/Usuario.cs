using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int? EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public virtual Escola Escola { get; set; }


    }
}