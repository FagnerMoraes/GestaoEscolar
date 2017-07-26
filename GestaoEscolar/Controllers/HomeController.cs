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
            ViewBag.ListaAlunos = (from al in _banco.Alunos
                                   join mat in _banco.Matriculas on al.Id equals mat.AlunoId
                                   into aluno
                                   from mat in aluno.DefaultIfEmpty()
                                   where al.Id != mat.AlunoId || mat.TurmaId == null
                                   select al).Count();


            ViewBag.ListaTurmas = _banco.Turmas.Count();

            ViewBag.ListaFuncionarios = _banco.Funcionarios.Where(arg => arg.TipoFuncionario.DescricaoFuncionario.Contains("PROFESSOR")).Count();

            return View();
        }
    }
}
