using Microsoft.AspNetCore.Mvc;
using ClinicaACME.Repositorio;
using ClinicaACME.Models;


namespace ClinicaACME.Controllers
{
    public class PacientesController : Controller
    {
        public IActionResult Index(string nome, string cpf)
        {
            var pacientes = Repositorio.Repositorio.Pacientes.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                pacientes = pacientes.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(cpf))
                pacientes = pacientes.Where(p => p.CPF.Contains(cpf));

            return View(pacientes.ToList());
        }

        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {

            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos");

            var cpf = Repositorio.Repositorio.Pacientes
                .Any(p => p.CPF == paciente.CPF && p.Id != paciente.Id);
            if (cpf)
                return BadRequest("CPF já cadastrado.");



            if (paciente.Id == 0 || paciente.Id == null)
            {
                paciente.Id = Repositorio.Repositorio.Pacientes.Any()
                    ? Repositorio.Repositorio.Pacientes.Max(p => p.Id) + 1
                    : 1;

                Repositorio.Repositorio.Pacientes.Add(paciente);
            }
            else
            {
                var pacienteOld = Repositorio.Repositorio.Pacientes.FirstOrDefault(p => p.Id == paciente.Id);
                if (pacienteOld == null)
                    return NotFound();

                pacienteOld.Nome = paciente.Nome;
                pacienteOld.DataNascimento = paciente.DataNascimento;
                pacienteOld.CPF = paciente.CPF;
                pacienteOld.Genero = paciente.Genero;
                pacienteOld.Endereco.CEP = paciente.Endereco.CEP;
                pacienteOld.Endereco.Cidade = paciente.Endereco.Cidade;
                pacienteOld.Endereco.Bairro = paciente.Endereco.Bairro;
                pacienteOld.Endereco.Rua = paciente.Endereco.Rua;
                pacienteOld.Endereco.Nro = paciente.Endereco.Nro;
                pacienteOld.Status = paciente.Status;
            }

            return RedirectToAction("Index");
        }
        
        public IActionResult Update(int id)
        {
            var paciente = Repositorio.Repositorio.Pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        public IActionResult Inativar(int id)
        {
            var paciente = Repositorio.Repositorio.Pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
                return NotFound();

            paciente.Status = false;

            return RedirectToAction("Index");
        }
    }
}
