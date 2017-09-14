using System.Web.Mvc;
using GestaoEscolar.Models;
using System;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    public class AnoLetivoController : Controller
    {
        private AnoLetivoDAO dao;

        public AnoLetivoController(AnoLetivoDAO anoLetivoDAO)
        {
            this.dao = anoLetivoDAO;
        }

        public ActionResult Index()
        {
            var anosLetivos = dao.ListaAnoLetivo();

            return View(anosLetivos);
        }

        public ActionResult Adicionar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adicionar(AnoLetivo novoAnoLetivo)
        {
            if (ModelState.IsValid)
            {
                dao.Salvar(novoAnoLetivo);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Detalhes(int id)
        {
            var anoletivo = dao.BuscaAnoLetivoPorId(id);

            if (anoletivo == null)
            {
                return HttpNotFound();
            }
            return View(anoletivo);
        }

        public ActionResult Editar(int id)
        {
            AnoLetivo anoLetivo = dao.BuscaAnoLetivoPorId(id);

            return View(anoLetivo);
        }
        [HttpPost]
        public ActionResult Editar(AnoLetivo anoLetivo)
        {
            if (ModelState.IsValid)
            {
                dao.Alterar(anoLetivo);
                return RedirectToAction("Index");
            }
            return View(anoLetivo);
        }

        public ActionResult Excluir(int id)
        {
            var anoLetivo = dao.BuscaAnoLetivoPorId(id);            

            try
            {
                dao.Excluir(anoLetivo);
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
