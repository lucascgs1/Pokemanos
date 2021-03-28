using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokemanos.Api.Model
{
    public class UsuarioViewModel
    {
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
        [JsonIgnore]
        public string Senha { get; set; }

    }
}
