using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index(string termoBusca, int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var funcionario = _banco.Funcionarios.Include("Escola").Include("TipoFuncionario").OrderBy(x => x.NomeFuncionario).ToPagedList(numeroPagina, tamanhoPagina);

            if (!String.IsNullOrEmpty(termoBusca))
            {
                funcionario = funcionario.Where(x => x.NomeFuncionario.ToUpper().Contains(termoBusca.ToUpper())).ToPagedList(numeroPagina, tamanhoPagina + 100);
            }

            return View(funcionario);
        }

        [Authorize]
        public ActionResult Adicionar()
        {
            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola");
            ViewBag.TipoFuncionarioId = new SelectList(_banco.TipoFuncionarios, "Id", "DescricaoFuncionario");

            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Funcionario novoFuncionario)
        {
            if (ModelState.IsValid)
            {
                _banco.Funcionarios.Add(novoFuncionario);
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola", novoFuncionario.EscolaId);
            ViewBag.TipoFuncionarioId = new SelectList(_banco.TipoFuncionarios, "Id", "DescricaoFuncionario", novoFuncionario.TipoFuncionarioId);

            return View(novoFuncionario);
        }//fim adicionar

        public ActionResult Detalhes(long id)
        {
            var funcionario = _banco.Funcionarios.Include("Escola").Include("TipoFuncionario").First(x => x.Id == id);

            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola");
            ViewBag.TipoFuncionarioId = new SelectList(_banco.TipoFuncionarios, "Id", "DescricaoFuncionario");

            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        public ActionResult Editar(long id)
        {
            Funcionario funcionario = _banco.Funcionarios.Find(id);

            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola", funcionario.EscolaId);
            ViewBag.TipoFuncionarioId = new SelectList(_banco.TipoFuncionarios, "Id", "DescricaoFuncionario", funcionario.TipoFuncionarioId);


            return View(funcionario);
        }

        [HttpPost]
        public ActionResult Editar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(funcionario).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola", funcionario.EscolaId);
            ViewBag.TipoFuncionarioId = new SelectList(_banco.TipoFuncionarios, "Id", "DescricaoFuncionario", funcionario.TipoFuncionarioId);

            return View(funcionario);
        }

        public ActionResult Excluir(long id)
        {
            var funcionario = _banco.Funcionarios.First(x => x.Id == id);
            _banco.Funcionarios.Remove(funcionario);

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
