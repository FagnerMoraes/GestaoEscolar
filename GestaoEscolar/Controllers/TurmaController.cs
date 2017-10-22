using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;
using System;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class TurmaController : Controller
    {
        private TurmaDAO dao;

        public TurmaController(TurmaDAO dao)
        {
            this.dao = dao;
        }
        
        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var turma = dao.ListarTurma();

            var lista = turma.ToPagedList(numeroPagina, tamanhoPagina);
            return View(lista);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            var listaescola = dao.listarEscola();
            var listaprofessor = dao.listarProfessor();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola");
            ViewBag.FuncionarioId = new SelectList(listaprofessor, "Id", "NomeFuncionario");

            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Turma novoTurma)
        {
            if (ModelState.IsValid)
            {
                dao.Salvar(novoTurma);

                return RedirectToAction("Detalhes", "Turma", new { Id = novoTurma.Id });
            }

            var listaescola = dao.listarEscola();
            var listaprofessor = dao.listarProfessor();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola", novoTurma.EscolaId);
            ViewBag.FuncionarioId = new SelectList(listaprofessor, "Id", "NomeFuncionario", novoTurma.FuncionarioId);
            return View(novoTurma);
        }

        public ActionResult Detalhes(int id)
        {
            ViewBag.DetalheTurma = true;

            var turma =  dao.buscarTurmaId(id);

            ViewBag.AlunosTurma = dao.listarAluno();

            if (turma == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id = turma.Id;
            ViewBag.Escola = turma.Escola.NomeEscola;
            ViewBag.NomeTurma = turma.NomeTurma;
            ViewBag.Serie = turma.Serie;
            ViewBag.NivelTurma = turma.NivelTurma;
            ViewBag.HorarioFuncionamento = turma.HorarioFuncionamento;
            ViewBag.ModalidadeEnsino = turma.ModalidadeEnsino;
            ViewBag.QtdAlunos = turma.QtdAlunos;

            var disciplinas = dao.listaDiscProfTurma(turma);
            
            return View(disciplinas);
        }

        public ActionResult Editar(int id)
        {
            Turma turma = dao.buscarTurmaId(id);

            ViewBag.Editar = true;

            var listaescola = dao.listarEscola();
            var listaprofessor = dao.listarProfessor();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola", turma.EscolaId);
            ViewBag.FuncionarioId = new SelectList(listaprofessor, "Id", "NomeFuncionario", turma.FuncionarioId);

            return View(turma);
        }

        [HttpPost]
        public ActionResult Editar(Turma turma)
        {
            if (ModelState.IsValid)
            {
                dao.Alterar(turma);
                return RedirectToAction("Detalhes", "Turma", new { Id = turma.Id });
            }

            var listaescola = dao.listarEscola();
            var listaprofessor = dao.listarProfessor();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola", turma.EscolaId);
            ViewBag.FuncionarioId = new SelectList(listaprofessor, "Id", "NomeFuncionario", turma.FuncionarioId);

            return View(turma);
        }

        public ActionResult Excluir(int id)
        {
            var turma = dao.buscarTurmaId(id);            

            try
            {
                dao.Exlcuir(turma);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.UrlReferrer != null) ViewBag.Voltar = Request.UrlReferrer.ToString();
                return View("Error", new HandleErrorInfo(ex, "Turma", "Index"));
            }
        }

    }
}
