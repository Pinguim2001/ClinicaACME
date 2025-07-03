using ClinicaACME.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaACME.Controllers
{
    public class AtendimentosController : Controller
    {
        public IActionResult Index(int? pacienteId, bool status, DateTime? data)
        {
            var atendimentos = Repositorio.Repositorio.Atendimentos.AsQueryable();

            if (pacienteId != null)
                atendimentos = atendimentos.Where(a => a.PacienteId == pacienteId);

            if (data != null)
                atendimentos = atendimentos.Where(a => a.Data >= data);

            ViewBag.Pacientes = Repositorio.Repositorio.Pacientes
                .Where(p => p.Status == true)
                .ToList();

            return View(atendimentos.ToList());
        }

        [HttpPost]
        public IActionResult Create(Atendimento atendimento)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            if (atendimento.Data > DateTime.Now)
                return BadRequest("Data e hora não podem ser no futuro.");

            var paciente = Repositorio.Repositorio.Pacientes.FirstOrDefault(p => p.Id == atendimento.PacienteId && p.Status == true);
            if (paciente == null)
                return BadRequest("Paciente inválido ou inativo.");

            if (atendimento.Id == 0 || atendimento.Id == null)
            {
                atendimento.Id = Repositorio.Repositorio.Atendimentos.Any()
                    ? Repositorio.Repositorio.Atendimentos.Max(a => a.Id) + 1
                    : 1;

                Repositorio.Repositorio.Atendimentos.Add(atendimento);
            }
            else
            {
                var atendimentoOld = Repositorio.Repositorio.Atendimentos.FirstOrDefault(a => a.Id == atendimento.Id);
                if (atendimentoOld == null)
                    return NotFound();

                atendimentoOld.PacienteId = atendimento.PacienteId;
                atendimentoOld.Data = atendimento.Data;
                atendimentoOld.Descricao = atendimento.Descricao;
                atendimentoOld.Status = atendimento.Status;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var atendimento = Repositorio.Repositorio.Atendimentos.FirstOrDefault(a => a.Id == id);
            if (atendimento == null)
                return NotFound();

            return View(atendimento);
        }

        public IActionResult Inativar(int id)
        {
            var atendimento = Repositorio.Repositorio.Atendimentos.FirstOrDefault(a => a.Id == id);
            if (atendimento == null)
                return NotFound();

            atendimento.Status = false;

            return RedirectToAction("Index");
        }
    }
}
