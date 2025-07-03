using System.ComponentModel.DataAnnotations;

namespace ClinicaACME.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Rua{ get; set; }
        [Required]
        public int Nro{ get; set; }
        public string Complemento { get; set; }



    }
}
