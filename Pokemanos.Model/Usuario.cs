using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pokemanos.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [Display(Name = "Nome", Prompt = "Digite o {0}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [EmailAddress]
        [Display(Name = "E-mail", Prompt = "Digite o {0}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [Display(Name = "Telefone", Prompt = "Digite o {0}")]
        public string Telefone { get; set; }
    }
}
