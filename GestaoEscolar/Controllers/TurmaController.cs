using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;
using System;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class TurmaController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var turma = _banco.Turmas.Include("Escola").OrderBy(x => x.NomeTurma).ToPagedList(numeroPagina, tamanhoPagina);
            return View(turma);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola");
            ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Where(x => x.TipoFuncionario.DescricaoFuncionario.Contains("Professor")), "Id", "NomeFuncionario");
            
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Turma novoTurma)
        {
            if (ModelState.IsValid)
            {
                _banco.Turmas.Add(novoTurma);
                _banco.SaveChanges();
                return RedirectToAction("GerenciarDisciplinaTurma", "DisciplinaDoProfessorNaTurma", new { turmaId =  novoTurma.Id});
            }
            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola", novoTurma.EscolaId);
            ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Where(x => x.TipoFuncionario.DescricaoFuncionario.Contains("Professor")), "Id", "NomeFuncionario", novoTurma.FuncionarioId);
            return View(novoTurma);
        }//fim adicionar

        public ActionResult Detalhes(long id)
        {
            var turma = _banco.Turmas.First(x => x.Id == id);

            if (turma == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id = turma.Id;
            ViewBag.Escola = turma.Escola.NomeEscola;
            ViewBag.NomeTurma = turma.NomeTurma;
            ViewBag.Serie = turma.Serie;
            ViewBag.NivelTurma = turma.NivelTurma;
            ViewBag.HorarioFuncionamento = turma.HorarioFuncionamento;
            ViewBag.ModalidadeEnsino = turma.ModalidadeEnsino;
            ViewBag.QtdAlunos = turma.QtdAlunos;
            
            var disciplinas = _banco.DisciplinaDoProfessoresNasTurmas.Where(x => x.TurmaId == turma.Id).ToList();
            

            return View(disciplinas);
        }

        public ActionResult Editar(long id)
        {
            Turma turma = _banco.Turmas.Find(id);

            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola", turma.EscolaId);
            ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Where(x => x.TipoFuncionario.DescricaoFuncionario.Contains("Professor")), "Id", "NomeFuncionario", turma.FuncionarioId);

            return View(turma);
        }

        [HttpPost]
        public ActionResult Editar(Turma turma)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(turma).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola", turma.EscolaId);
            ViewBag.FuncionarioId = new SelectList(_banco.Funcionarios.Where(x => x.TipoFuncionario.DescricaoFuncionario.Contains("Professor")), "Id", "NomeFuncionario", turma.FuncionarioId);
            return View(turma);
        }

        public ActionResult Excluir(long id)
        {
            var turma = _banco.Turmas.First(x => x.Id == id);
            _banco.Turmas.Remove(turma);

            try
            {
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.UrlReferrer != null) ViewBag.Voltar = Request.UrlReferrer.ToString();
                return View("Error", new HandleErrorInfo(ex, "Turma", "Index"));
            }
        }

    }
}
