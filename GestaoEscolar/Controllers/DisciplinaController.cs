using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;
using System;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class DisciplinaController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var disciplina = _banco.Disciplinas.OrderBy(x => x.NomeDisciplina).ToPagedList(numeroPagina, tamanhoPagina);
            return View(disciplina);
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
                _banco.Disciplinas.Add(novaDisciplina);
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novaDisciplina);
        }//fim adicionar

        public ActionResult Detalhes(long id)
        {
            var disciplina = _banco.Disciplinas.First(x => x.Id == id);


            if (disciplina == null)
            {
                return HttpNotFound();
            }
            return View(disciplina);
        }
        
        public ActionResult Editar(long id)
        {
            Disciplina disciplina = _banco.Disciplinas.Find(id);

            return View(disciplina);
        }

        [HttpPost]
        public ActionResult Editar(Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(disciplina).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disciplina);
        }
        
        public ActionResult Excluir(long id)
        {
            var disciplina = _banco.Disciplinas.First(x => x.Id == id);
            _banco.Disciplinas.Remove(disciplina);

            try
            {
                _banco.SaveChanges();
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
