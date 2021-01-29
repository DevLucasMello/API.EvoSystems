using System.ComponentModel.DataAnnotations;

namespace Evo.API.Models
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]        
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(5, ErrorMessage = "Este campo deve conter entre 1 e 5 caracteres")]
        [MinLength(1, ErrorMessage = "Este campo deve conter entre 1 e 5 caracteres")]
        public string Sigla { get; set; }
    }
}
