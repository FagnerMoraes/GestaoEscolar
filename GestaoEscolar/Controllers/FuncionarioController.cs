using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        private FuncionarioDAO dao;

        public FuncionarioController(FuncionarioDAO dao)
        {
            this.dao = dao;
        }

        public ActionResult Index(string termoBusca, int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var funcionario = dao.ListaFuncionario().ToPagedList(numeroPagina, tamanhoPagina);

            if (!String.IsNullOrEmpty(termoBusca))
            {
                funcionario = funcionario.Where(x => x.NomeFuncionario.ToUpper().Contains(termoBusca.ToUpper())).ToPagedList(numeroPagina, tamanhoPagina + 100);
            }

            return View(funcionario);
        }

        [Authorize]
        public ActionResult Adicionar()
        {
            var listaescola = dao.listarEscola();
            var listacargo = dao.listarTipoFuncionario();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola");
            ViewBag.TipoFuncionarioId = new SelectList(listacargo, "Id", "DescricaoFuncionario");
            

            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Funcionario novoFuncionario)
        {
            if (ModelState.IsValid)
            {
                dao.Salvar(novoFuncionario);
                return RedirectToAction("Index");
            }
            var listaescola = dao.listarEscola();
            var listacargo = dao.listarTipoFuncionario();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola", novoFuncionario.EscolaId);
            ViewBag.TipoFuncionarioId = new SelectList(listacargo, "Id", "DescricaoFuncionario", novoFuncionario.TipoFuncionarioId);

            return View(novoFuncionario);
        }//fim adicionar

        public ActionResult Detalhes(int id)
        {
            ViewBag.Detalhe = true;
            var funcionario = dao.BuscaPorId(id);

            var listaescola = dao.listarEscola();
            var listacargo = dao.listarTipoFuncionario();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola");
            ViewBag.TipoFuncionarioId = new SelectList(listacargo, "Id", "DescricaoFuncionario");

            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Editar = true;
            Funcionario funcionario = dao.BuscaPorId(id);

            var listaescola = dao.listarEscola();
            var listacargo = dao.listarTipoFuncionario();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola", funcionario.EscolaId);
            ViewBag.TipoFuncionarioId = new SelectList(listacargo, "Id", "DescricaoFuncionario", funcionario.TipoFuncionarioId);


            return View(funcionario);
        }

        [HttpPost]
        public ActionResult Editar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                dao.Alterar(funcionario);
                return RedirectToAction("Index");
            }
            var listaescola = dao.listarEscola();
            var listacargo = dao.listarTipoFuncionario();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola", funcionario.EscolaId);
            ViewBag.TipoFuncionarioId = new SelectList(listacargo, "Id", "DescricaoFuncionario", funcionario.TipoFuncionarioId);

            return View(funcionario);
        }

        public ActionResult Excluir(int id)
        {
            var funcionario = dao.BuscaPorId(id);            

            try
            {
                dao.Excluir(funcionario);               
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
