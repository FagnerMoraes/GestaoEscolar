using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class EscolaController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index(string termoBusca)
        {
            var escola = _banco.Escolas.ToList();

            if (!String.IsNullOrEmpty(termoBusca))
            {
                escola = escola.Where(x => x.NomeEscola.ToUpper().Contains(termoBusca.ToUpper())).ToList();
            }

            return View(escola);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Escola novaEscola)
        {
            if (ModelState.IsValid)
            {
                _banco.Escolas.Add(novaEscola);
                _banco.SaveChanges();

                return RedirectToAction("Index");



                //if (Request.UrlReferrer != Request.Url)
                //{
                //    return Redirect(Request.UrlReferrer.ToString());    
                //}


            }

            return View();
        }

        public ActionResult Detalhes(long id)
        {
            var escola = _banco.Escolas.First(x => x.Id == id);


            if (escola == null)
            {
                return HttpNotFound();
            }
            return View(escola);
        }

        public ActionResult Editar(long id)
        {
            Escola escola = _banco.Escolas.Find(id);

            return View(escola);
        }

        [HttpPost]
        public ActionResult Editar(Escola escola)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(escola).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(escola);
        }

        public ActionResult Excluir(long id)
        {
            var escola = _banco.Escolas.First(x => x.Id == id);
            _banco.Escolas.Remove(escola);
            try
            {
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.UrlReferrer != null) ViewBag.Voltar = Request.UrlReferrer.ToString();
                return View("Error", new HandleErrorInfo(ex, "Escola", "Index"));
            }
        }

    }
}
