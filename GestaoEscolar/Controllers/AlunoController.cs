using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class AlunoController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public AlunoController()
        {
            _banco.Database.Log = x => Debug.Write(x);
        }


        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var aluno = _banco.Alunos.OrderBy(x => x.Nome).ToPagedList(numeroPagina,tamanhoPagina);

            
            return View(aluno);
        }

        [HttpPost]
        public ActionResult Index(string termoBusca, int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var aluno = _banco.Alunos.OrderBy(x => x.Nome).ToPagedList(numeroPagina, tamanhoPagina);

            if (!String.IsNullOrEmpty(termoBusca))
            {
                aluno = _banco.Alunos.OrderBy(x => x.Nome).
                Where(x => x.Nome.ToUpper().Contains(termoBusca.ToUpper())).
                ToPagedList(numeroPagina, tamanhoPagina);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_ListaAlunos", aluno);

            return View(aluno);
        }

        public JsonResult GetAlunos(string term)
        {
            var aluno = _banco.Alunos.Where(x => x.Nome.ToUpper().Contains(term.ToUpper()))
                .Select(y => y.Nome).ToList();

            return Json(aluno, JsonRequestBehavior.AllowGet);
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
                _banco.Alunos.Add(novoAluno);
                _banco.SaveChanges();

                if (novoAluno.Situacao == "Ativo")
                {
                    return RedirectToAction("AdicionarComId", "Matricula", new { id = novoAluno.Id });
                }
                return RedirectToAction("Index");
            }
            return View(novoAluno);
        }//fim adicionar

        public ActionResult RegistroUnico(string nome)
        {
            var nomealunos = ((from al in _banco.Alunos
                               where al.Nome == nome
                               select al.Nome).ToArray());

            return Json(nomealunos.All(x => x.ToUpper() != nome.ToUpper()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalhes(long id)
        {
            var aluno = _banco.Alunos.First(x => x.Id == id);


            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        public ActionResult Editar(long id)
        {
            Aluno aluno = _banco.Alunos.Find(id);

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
                _banco.Entry(aluno).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aluno);
        }
        
        public ActionResult Excluir(long id)
        {
            var aluno = _banco.Alunos.First(x => x.Id == id);
            _banco.Alunos.Remove(aluno);

            try
            {
                _banco.SaveChanges();
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
