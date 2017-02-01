using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class DisciplinaDoProfessorNaTurmaController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index(int? turmaId)
        {
            var valor = _banco.DisciplinaDoProfessoresNasTurmas
                        .Include("Disciplina")
                        .Include("Funcionario")
                        .Include("Turma")
                        .ToList();

            if (turmaId != null)
            {
                valor = valor.Where(x => x.TurmaId == turmaId).ToList();
            }

            return View(valor);
        }
        
        [HttpGet]
        public ActionResult GerenciarDisciplinaTurma(int? turmaId)
        {
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");

            var turma = _banco.Turmas.FirstOrDefault(x => x.Id == turmaId);

            if (turma != null)
            {
                ViewBag.turmaSelecionada = turma.Id;
                ViewBag.Nometurma = turma.NomeTurma;
            }

            ViewBag.DisciplinaId = new SelectList(_banco.Disciplinas.OrderBy(x => x.NomeDisciplina), "Id", "NomeDisciplina");
            ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Include(x => x.TipoFuncionario).
                                                    Where(p => p.TipoFuncionario.DescricaoFuncionario.
                                                    Contains("Professor")), "Id", "NomeFuncionario");

            var valor = _banco.DisciplinaDoProfessoresNasTurmas.Include("Disciplina")
                        .Include("Funcionario").Include("Turma")
                        .Where(x => x.TurmaId == turmaId).OrderBy(x => x.Disciplina.NomeDisciplina).ToList();

            return View(valor);
        }


        [HttpPost]
        public ActionResult GerenciarDisciplinaTurma(int turmaSelecionada, int disciplinaId, int funcionarioId)
        {
            var novocadastro = new DisciplinaDoProfessorNaTurma
            {
                TurmaId = turmaSelecionada,
                DisciplinaId = disciplinaId,
                FuncionarioId = funcionarioId
            };

            var teste =
                _banco.DisciplinaDoProfessoresNasTurmas.
                Where(arg => arg.TurmaId == novocadastro.TurmaId &&
                    arg.DisciplinaId == novocadastro.DisciplinaId).ToList();

            if (teste.Count != 0)
            {
                return RedirectToAction("GerenciarDisciplinaTurma", new { turmaId = turmaSelecionada });
            }




            if (ModelState.IsValid)
            {
                _banco.DisciplinaDoProfessoresNasTurmas.Add(novocadastro);
                _banco.SaveChanges();
                return RedirectToAction("GerenciarDisciplinaTurma", new { turmaId = turmaSelecionada });
            }

            return View();
        }

        public ActionResult Editar(long id)
        {
            DisciplinaDoProfessorNaTurma valor = _banco.DisciplinaDoProfessoresNasTurmas.Find(id);

            ViewBag.DisciplinaId = new SelectList(_banco.Disciplinas, "Id", "NomeDisciplina", valor.DisciplinaId);
            ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Include(x => x.TipoFuncionario).Where(p => p.TipoFuncionario.DescricaoFuncionario.Contains("Professor")), "Id", "NomeFuncionario", valor.FuncionarioId);
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma", valor.TurmaId);

            return View(valor);
        }

        [HttpPost]
        public ActionResult Editar(DisciplinaDoProfessorNaTurma valor)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(valor).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("GerenciarDisciplinaTurma", new { turmaId = valor.TurmaId });
            }
            ViewBag.DisciplinaId = new SelectList(_banco.Disciplinas, "Id", "NomeDisciplina", valor.DisciplinaId);
            ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Where(p => p.TipoFuncionarioId == 1), "Id", "NomeFuncionario", valor.FuncionarioId);
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma", valor.TurmaId);

            return View(valor);
        }




        //public ActionResult Adicionar()
        //{
        //    ViewBag.DisciplinaId = new SelectList(_banco.Disciplinas, "Id", "NomeDisciplina");
        //    ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Include(x => x.TipoFuncionario).Where(p => p.TipoFuncionario.DescricaoFuncionario.Contains("Professor")), "Id", "NomeFuncionario");
        //    ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Adicionar(DisciplinaDoProfessorNaTurma novovalor)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _banco.DisciplinaDoProfessoresNasTurmas.Add(novovalor);
        //        _banco.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.DisciplinaId = new SelectList(_banco.Disciplinas, "Id", "NomeDisciplina", novovalor.DisciplinaId);
        //    ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Include("TipoFuncionario").Where(p => p.TipoFuncionario.DescricaoFuncionario.Contains("Professor")), "Id", "NomeFuncionario", novovalor.FuncionarioId);
        //    ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma", novovalor.TurmaId);

        //    return View(novovalor);
        //}

        //public ActionResult Detalhes(long id)
        //{
        //    var valor =
        //        _banco.DisciplinaDoProfessoresNasTurmas.Include("Disciplina")
        //            .Include("Funcionario")
        //            .Include("Turma").First(x => x.Id == id);
        //    if (valor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(valor);
        //}

        public ActionResult Excluir(long id)
        {
            var valor = _banco.DisciplinaDoProfessoresNasTurmas.First(x => x.Id == id);
            var turma = valor.TurmaId;
            _banco.DisciplinaDoProfessoresNasTurmas.Remove(valor);
            _banco.SaveChanges();
            return RedirectToAction("GerenciarDisciplinaTurma", new { turmaId = turma });
        }

        public ActionResult ExcluirDaTurma(long id)
        {
            var valor = _banco.DisciplinaDoProfessoresNasTurmas.First(x => x.Id == id);
            var turma = valor.TurmaId;
            _banco.DisciplinaDoProfessoresNasTurmas.Remove(valor);
            _banco.SaveChanges();
            return RedirectToAction("Detalhes","Turma", new { Id = turma });
        }

    }
}
