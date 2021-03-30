using System;
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
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [EmailAddress]
        [Display(Name = "E-mail", Prompt = "Digite o {0}")]
        public string Email { get; set; }
      
        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [Display(Name = "Telefone", Prompt = "Digite o {0}")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [Phone]
        [Display(Name = "Telefone", Prompt = "Digite o {0}")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data", Prompt = "Digite o {0}")]
        public DateTime DataCadastro { get; set; }

        public CodigoSeguranca CodigoSeguranca { get; set; }
    }
}
