using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using System;

namespace GestaoEscolar.Controllers
{
    public class AnoLetivoController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index()
        {
            var anosLetivos = _banco.AnoLetivos.ToList();

            return View(anosLetivos);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adicionar(AnoLetivo novoAnoLetivo)
        {
            if (ModelState.IsValid)
            {
                _banco.AnoLetivos.Add(novoAnoLetivo);
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Detalhes(long id)
        {
            var anoletivo = _banco.AnoLetivos.First(x => x.Id == id);

            if (anoletivo == null)
            {
                return HttpNotFound();
            }
            return View(anoletivo);
        }

        [HttpGet]
        public ActionResult Editar(long id)
        {
            AnoLetivo anoLetivo = _banco.AnoLetivos.Find(id);

            return View(anoLetivo);
        }
        [HttpPost]
        public ActionResult Editar(AnoLetivo anoLetivo)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(anoLetivo).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anoLetivo);
        }

        public ActionResult Excluir(long id)
        {
            var anoLetivo = _banco.AnoLetivos.First(x => x.Id == id);
            _banco.AnoLetivos.Remove(anoLetivo);

            try
            {
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.UrlReferrer != null) ViewBag.Voltar = Request.UrlReferrer.ToString();
                return View("Error", new HandleErrorInfo(ex, "AnoLetivo", "Index"));
            }
        }
    }
}
