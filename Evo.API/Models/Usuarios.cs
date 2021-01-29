using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evo.API.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
