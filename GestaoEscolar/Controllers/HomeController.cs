using System.Linq;
using System.Web.Mvc;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index()
        {
            ViewBag.Escola = _banco.Escolas.Any();
            ViewBag.TipoFuncionario = _banco.TipoFuncionarios.Any();

            return View();
        }
    }
}
