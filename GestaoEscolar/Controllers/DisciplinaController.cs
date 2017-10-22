using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;
using System;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class DisciplinaController : Controller
    {        
        private DisciplinaDAO dao;

        public DisciplinaController(DisciplinaDAO dao)
        {
            this.dao = dao;
        }

        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var disciplina = dao.ListarDisciplina();

            var lista = disciplina.OrderBy(x => x.NomeDisciplina).ToPagedList(numeroPagina, tamanhoPagina);
            return View(lista);
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Disciplina novaDisciplina)
        {
            if (ModelState.IsValid)
            {
                dao.Salvar(novaDisciplina);
                return RedirectToAction("Index");
            }
            return View(novaDisciplina);
        }//fim adicionar

        public ActionResult Detalhes(int id)
        {
            var disciplina = dao.BuscaPorId(id);


            if (disciplina == null)
            {
                return HttpNotFound();
            }
            return View(disciplina);
        }
        
        public ActionResult Editar(int id)
        {
            var disciplina = dao.BuscaPorId(id);

            return View(disciplina);
        }

        [HttpPost]
        public ActionResult Editar(Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                dao.Alterar(disciplina);
                return RedirectToAction("Index");
            }
            return View(disciplina);
        }
        
        public ActionResult Excluir(int id)
        {
            var disciplina = dao.BuscaPorId(id);            

            try
            {
                dao.Excluir(disciplina);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.UrlReferrer != null) ViewBag.Voltar = Request.UrlReferrer.ToString();
                return View("Error", new HandleErrorInfo(ex, "Funcionario", "Index"));
            }
        }

    }
}
