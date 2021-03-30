using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemanos.Model
{
    public class CodigoSeguranca
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [Display(Name = "Codigo do usuario", Prompt = "Digite o {0}")]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [Display(Name = "Codigo do usuario", Prompt = "Digite o {0}")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [Display(Name = "Data de criação", Prompt = "Digite o {0}")]
        public DateTime DataCriacao { get; set; }
    }
}
