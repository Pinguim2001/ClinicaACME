using ClinicaACME.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicaACME.Controllers
{
    public class AtendimentosController : Controller
    {
        public IActionResult Index(int? pacienteId, bool? status, DateTime? dataInicio, DateTime? dataFim)
        {
            var atendimentos = Repositorio.Repositorio.Atendimentos.AsQueryable();

            if (pacienteId != null)
                atendimentos = atendimentos.Where(a => a.PacienteId == pacienteId);

            if (dataInicio != null)
                atendimentos = atendimentos.Where(a => a.Data >= dataInicio);

            if (dataFim != null)
                atendimentos = atendimentos.Where(a => a.Data <= dataFim);

            if (status != null)
                atendimentos = atendimentos.Where(p => p.Status == status);

            ViewBag.Pacientes = Repositorio.Repositorio.Pacientes
            .Where(p => p.Status == true)
            .ToList();

            foreach (var atendimento in atendimentos)
            {
                atendimento.Paciente = Repositorio.Repositorio.Pacientes
                                    .FirstOrDefault(p => p.Id == atendimento.PacienteId);
            }

            ViewBag.Pacientes = Repositorio.Repositorio.Pacientes;

            return View(atendimentos.ToList());
        }

        [HttpPost]
        public IActionResult Create(Atendimento atendimento)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pacientes = Repositorio.Repositorio.Pacientes;
                return View("Index", Repositorio.Repositorio.Atendimentos);
            }

            if (atendimento.Data > DateTime.Now)
            {
                ModelState.AddModelError("Data", "Data e hora não podem ser no futuro.");
                ViewBag.Pacientes = Repositorio.Repositorio.Pacientes;
                return View("Index", Repositorio.Repositorio.Atendimentos);
            }

            var paciente = Repositorio.Repositorio.Pacientes
                .FirstOrDefault(p => p.Id == atendimento.PacienteId && p.Status == true);

            if (paciente == null)
            {
                ModelState.AddModelError("PacienteId", "Paciente inativo ou inexistente.");
                ViewBag.Pacientes = Repositorio.Repositorio.Pacientes;
                return View("Index", Repositorio.Repositorio.Atendimentos);
            }

            if (atendimento.Id == 0 || atendimento.Id == null)
            {
                atendimento.Id = Repositorio.Repositorio.Atendimentos.Any()
                    ? Repositorio.Repositorio.Atendimentos.Max(a => a.Id) + 1
                    : 1;

                atendimento.Paciente = paciente;

                Repositorio.Repositorio.Atendimentos.Add(atendimento);
            }
            else
            {
                var atendimentoOld = Repositorio.Repositorio.Atendimentos
                    .FirstOrDefault(a => a.Id == atendimento.Id);

                if (atendimentoOld == null)
                    return NotFound();

                atendimentoOld.PacienteId = atendimento.PacienteId;
                atendimentoOld.Paciente = paciente;
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
