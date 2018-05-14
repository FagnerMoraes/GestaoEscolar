using System;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class EscolaController : Controller
    {
        private EscolaDAO dao;

        public EscolaController(EscolaDAO EscolaDao)
        {
            this.dao = EscolaDao;
        }

        public ActionResult Index(string termoBusca)
        {
            var escola = dao.ListaEscolas();

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
                dao.salva(novaEscola);
                return RedirectToAction("Index");                
            }
            return View();
        }

        public ActionResult Detalhes(long Id)
        {
            ViewBag.Detalhe = true;
            var escola = dao.BuscaPorId(Id);

            if (escola == null)
            {
                return HttpNotFound();
            }
            return View(escola);
        }

        public ActionResult Editar(long Id)
        {
            ViewBag.Editar = true;
            Escola escola = dao.BuscaPorId(Id);
            return View(escola);
        }

        [HttpPost]
        public ActionResult Editar(Escola escola)
        {
            if (ModelState.IsValid)
            {
                dao.Alterar(escola);
                return RedirectToAction("Index");
            }
            return View(escola);
        }

        public ActionResult Excluir(long Id)
        {            
            try
            {
                dao.Excluir(Id);
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