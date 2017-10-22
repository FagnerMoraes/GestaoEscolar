using System.Web.Mvc;
using GestaoEscolar.Models;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    public class ConceitoFormacaoController : Controller
    {
        private ConceitoFormacaoDAO dao;

        public ConceitoFormacaoController(ConceitoFormacaoDAO dao)
        {
            this.dao = dao;
        }        

        public ActionResult Editar(int? id, int MatriculaId, int periodo)
        {
            var conceitoformacao = dao.BuscaConceitoFormacao(MatriculaId, periodo);


            if (conceitoformacao != null)
            {
                return View(conceitoformacao);
            }
            else
            {
                ConceitoFormacao novoconceito = new ConceitoFormacao
                { MatriculaId = MatriculaId, Periodo = periodo };

                dao.Salvar(novoconceito);

                conceitoformacao = dao.BuscaConceitoFormacao(MatriculaId, periodo);

                id = conceitoformacao.Id;

                return RedirectToAction("Editar", new { id, MatriculaId, periodo });
            }
        }

        [HttpPost]
        public ActionResult Editar(ConceitoFormacao conceitoformacao)
        {
            if (ModelState.IsValid)
            {
                dao.Alterar(conceitoformacao);

                var Aluno = dao.BuscaAluno(conceitoformacao);
                    
                return RedirectToAction("NotaAluno", "Conceito", new { alunoId = Aluno.Id, periodo = conceitoformacao.Periodo });               
            }
            return View(conceitoformacao);
        }

    }
}
