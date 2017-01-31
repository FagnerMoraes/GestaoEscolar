using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class TipoFuncionarioController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var tipofuncionario = _banco.TipoFuncionarios.OrderBy(x => x.DescricaoFuncionario).ToPagedList(numeroPagina,tamanhoPagina);
            return View(tipofuncionario);
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(TipoFuncionario novoTipoFuncionario)
        {
            if (ModelState.IsValid)
            {
                _banco.TipoFuncionarios.Add(novoTipoFuncionario);
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novoTipoFuncionario);
        }//fim adicionar

        public ActionResult Detalhes(long id)
        {
            var tipofuncionario = _banco.TipoFuncionarios.First(x => x.Id == id);


            if (tipofuncionario == null)
            {
                return HttpNotFound();
            }
            return View(tipofuncionario);
        }

        public ActionResult Editar(long id)
        {
            TipoFuncionario tipofuncionario = _banco.TipoFuncionarios.Find(id);

            return View(tipofuncionario);
        }

        [HttpPost]
        public ActionResult Editar(TipoFuncionario tipoFuncionario)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(tipoFuncionario).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoFuncionario);
        }

        public ActionResult Excluir(long id)
        {
            var tipofuncionario = _banco.TipoFuncionarios.First(x => x.Id == id);
            _banco.TipoFuncionarios.Remove(tipofuncionario);
            _banco.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
