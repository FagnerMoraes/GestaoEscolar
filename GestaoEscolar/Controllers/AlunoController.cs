using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class AlunoController : Controller
    {
        private AlunoDAO dao;

        public AlunoController(AlunoDAO alunoDAO)
        {
            this.dao = alunoDAO;
        }

        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 4;
            int numeroPagina = pagina ?? 1;

            var aluno = dao.Lista();

            var lista = aluno.ToPagedList(numeroPagina, tamanhoPagina);


            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(string termoBusca, int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var aluno = dao.Lista();

            var lista = aluno.ToPagedList(numeroPagina, tamanhoPagina);

            if (!String.IsNullOrEmpty(termoBusca))
            {
                lista = lista.Where(x => x.Nome.ToUpper().Contains(termoBusca.ToUpper())).
                    ToPagedList(numeroPagina, tamanhoPagina);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_ListaAlunos", lista);
            return View(lista);
        }
        
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Aluno novoAluno)
        {
            if (ModelState.IsValid)
            {
                dao.Salvar(novoAluno);
                return RedirectToAction("Index");
            }
            return View(novoAluno);
        }

        public ActionResult Detalhes(int id)
        {
            ViewBag.Detalhe = true;

            var aluno = dao.BuscarAlunoId(id);

            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Editar = true;

            Aluno aluno = dao.BuscarAlunoId(id);

            ViewBag.DataNascimento = aluno.DataNascimento.ToShortDateString();
            if (aluno.DataExpRg != null) ViewBag.DataExpRg = aluno.DataExpRg.Value.ToShortDateString();
            if (aluno.DataEmissaoCertidao != null) ViewBag.DataEmissaoCertidao = aluno.DataEmissaoCertidao.Value.ToShortDateString();

            return View(aluno);
        }

        [HttpPost]
        public ActionResult Editar(Aluno aluno)
        {

            if (ModelState.IsValid)
            {
                dao.Alterar(aluno);
                return RedirectToAction("Detalhes",aluno);
            }

            return View(aluno);
        }

        public ActionResult Excluir(int id)
        {
            var aluno = dao.BuscarAlunoId(id);

            try
            {
                dao.Excluir(aluno);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.UrlReferrer != null) ViewBag.Voltar = Request.UrlReferrer.ToString();
                return View("Error", new HandleErrorInfo(ex, "Aluno", "Index"));
            }
        }
    }
}
