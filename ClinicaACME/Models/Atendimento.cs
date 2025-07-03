using System.ComponentModel.DataAnnotations;

namespace ClinicaACME.Models
{
    public class Atendimento
    {
        public int? Id { get; set; }

        [Required]  
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        [Required]
        public DateTime Data {  get; set; }

        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public bool Status { get; set; }
    }
}
