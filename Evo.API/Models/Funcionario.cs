using System.ComponentModel.DataAnnotations;

namespace Evo.API.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        public string Foto { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(15, ErrorMessage = "Este campo deve conter entre 9 e 15 caracteres")]
        [MinLength(9, ErrorMessage = "Este campo deve conter entre 9 e 15 caracteres")]
        public string RG { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Departamento Inválido")]
        public int DepartamentoId { get; set; }

        public Departamento Departamento { get; set; }
    }
}
