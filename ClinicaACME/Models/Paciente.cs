using System.ComponentModel.DataAnnotations;

namespace ClinicaACME.Models
{
    public class Paciente : IValidatableObject
    {
        public enum GeneroEnum
        {
            Masculino = 0,
            Feminino = 1
        }

        public int? Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento {  get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O Gênero é obrigatório")]
        public GeneroEnum Genero { get; set; }

        [Required(ErrorMessage = "O Endereço é obrigatório")]
        public Endereco Endereco { get; set; }

        [Required]
        public bool Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataNascimento > DateTime.Now)
                yield return new ValidationResult("Data de nascimento não pode ser no futuro", new[] { nameof(DataNascimento) });

            if (Endereco == null)
                yield return new ValidationResult("Endereço é obrigatório", new[] { nameof(Endereco) });

        }
    }
}
