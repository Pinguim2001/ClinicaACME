using ClinicaACME.Models;

namespace ClinicaACME.Repositorio
{
    public static class Repositorio
    {
        public static List<Paciente> Pacientes { get; } = new List<Paciente>();
        public static List<Atendimento> Atendimentos { get; } = new List<Atendimento>();
        public static List<Endereco> Enderecos { get; } = new List<Endereco>();
    }
}
