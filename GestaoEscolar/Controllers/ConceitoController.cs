using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using System.Web.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class ConceitoController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult ListaAluno(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var aluno = _banco.Matriculas.Where(x=> x.TurmaId != null).OrderBy(x => x.Aluno.Nome).ToPagedList(numeroPagina, tamanhoPagina);

            return View(aluno);
        }

        [HttpPost]
        public ActionResult ListaAluno(string termoBusca, int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var aluno = _banco.Matriculas.OrderBy(x => x.Aluno.Nome).ToPagedList(numeroPagina, tamanhoPagina);

            if (!String.IsNullOrEmpty(termoBusca))
            {
                aluno = _banco.Matriculas.OrderBy(x => x.Aluno.Nome).
                Where(x => x.Aluno.Nome.ToUpper().Contains(termoBusca.ToUpper())).
                ToPagedList(numeroPagina, tamanhoPagina);
            }

            return View(aluno);
        }

        public JsonResult GetAlunos(string term)
        {
            var aluno = _banco.Matriculas.Where(x => x.Aluno.Nome.ToUpper().Contains(term.ToUpper()))
                .Select(y => y.Aluno.Nome).ToList();

            return Json(aluno, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult NotaAluno(int? alunoId, int? periodo)
        {
            List<Conceito> conceitos = null;

            if (periodo != null)
            {
                conceitos = _banco.Conceitos.Where(x => x.Matricula.AlunoId == alunoId).ToList();

                foreach (var item in conceitos)
                {
                    switch (periodo)
                    {
                        case 1:
                            ViewBag.Nota = item.Conceito1Bim; ViewBag.Falta = item.Faltas1Bim;
                            break;
                        case 2:
                            ViewBag.Nota = item.Conceito2Bim; ViewBag.Falta = item.Faltas2Bim;
                            break;
                        case 3:
                            ViewBag.Nota = item.Conceito3Bim; ViewBag.Falta = item.Faltas3Bim;
                            break;
                        case 4:
                            ViewBag.Nota = item.Conceito4Bim; ViewBag.Falta = item.Faltas4Bim;
                            break;
                    }

                }


                conceitos = _banco.Conceitos.Where(x => x.Matricula.AlunoId == alunoId).ToList();

                var novoaluno = _banco.Matriculas.FirstOrDefault(x => x.AlunoId == alunoId);

                var dadosaluno = conceitos.FirstOrDefault();

                var conform = _banco.ConceitoFormacaos.FirstOrDefault(x => x.Matricula.AlunoId == alunoId && periodo == x.Periodo);

                if(conform != null)
                {
                    ViewBag.FormConId = conform.Id;
                    ViewBag.AtitVal = conform.AtitVal;
                    ViewBag.CompAssid = conform.CompAssid;
                    ViewBag.CriCriti = conform.CriCriti;
                    ViewBag.PartFamilia = conform.PartFamilia;
                }

                if (dadosaluno != null)
                {
                    ViewBag.NomeAluno = dadosaluno.Matricula.Aluno.Nome;
                    ViewBag.AlunoId = dadosaluno.Matricula.AlunoId;
                    ViewBag.MatriculaId = dadosaluno.MatriculaId;
                    if (novoaluno != null) ViewBag.NovoAlunoId = novoaluno.AlunoId;
                    ViewBag.Turma = dadosaluno.Matricula.Turma.NomeTurma;
                    ViewBag.TurmaId = dadosaluno.Matricula.TurmaId;
                }
                ViewBag.Periodo = periodo;
            }


            return View(conceitos);
        }

        public ActionResult Index(int? turmaId, int? disciplinaId)
        {
            ViewBag.turmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");

            var disciplina = (from d in _banco.Disciplinas
                              join dpt in _banco.DisciplinaDoProfessoresNasTurmas on d.Id equals dpt.DisciplinaId
                              where d.Id == dpt.DisciplinaId && dpt.TurmaId == turmaId
                              select d).ToList();

            ViewBag.DisciplinaId = new SelectList(disciplina, "Id", "NomeDisciplina");

            var conceitos = new List<Conceito>();

            //conceitos = (from con in _banco.Conceitos
            //             join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
            //             select con).ToList();


            if (turmaId != null && disciplinaId != null)
            {

                conceitos = (from con in _banco.Conceitos
                             join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
                             where con.MatriculaId == mat.Id && mat.TurmaId == turmaId && con.DisciplinaId == disciplinaId
                             select con).ToList();

            }


            return View(conceitos);
        }

        public ActionResult Detalhes(long id, int disciplinaId)
        {
            var conceito = _banco.Conceitos.FirstOrDefault(x => x.Id == id && x.DisciplinaId == disciplinaId);

            return View(conceito);
        }

        public ActionResult NotasTurma(int? turmaId, int? disciplinaId, int? periodo)
        {
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");

            var disciplinas = (from d in _banco.Disciplinas
                               join dpt in _banco.DisciplinaDoProfessoresNasTurmas on d.Id equals dpt.DisciplinaId
                               where d.Id == dpt.DisciplinaId && dpt.TurmaId == turmaId
                               select d).ToList();

            ViewBag.DisciplinaId = new SelectList(disciplinas, "Id", "NomeDisciplina");

            ViewBag.Bimestre = periodo;

            var bimestre = new List<Bimestre>();

            if (turmaId != null && disciplinaId != null)
            {
                //List<Matricula> semNota = (from mat in _banco.Matriculas
                //                           join con in _banco.Conceitos on mat.Id equals con.MatriculaId
                //                           into matricula
                //                           from con in matricula.DefaultIfEmpty()
                //                           where con.MatriculaId != mat.Id
                //                           select mat).ToList();

                List<Matricula> query = null;

                switch (periodo)
                {
                    case 1:
                        query = (from con in _banco.Conceitos
                                 join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
                                 where con.Matricula.TurmaId == turmaId &&
                                 con.DisciplinaId == disciplinaId &&
                                 con.Conceito1Bim == null
                                 select mat).ToList();

                        //query.AddRange(semNota);

                        break;

                    case 2:
                        query = (from con in _banco.Conceitos
                                 join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
                                 where con.Matricula.TurmaId == turmaId &&
                                 con.DisciplinaId == disciplinaId &&
                                 con.Conceito2Bim == null
                                 select mat).ToList();

                        //query.AddRange(semNota);
                        break;

                    case 3:
                        query = (from con in _banco.Conceitos
                                 join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
                                 where con.Matricula.TurmaId == turmaId &&
                                 con.DisciplinaId == disciplinaId &&
                                 con.Conceito3Bim == null
                                 select mat).ToList();

                        //query.AddRange(semNota);
                        break;

                    case 4:
                        query = (from con in _banco.Conceitos
                                 join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
                                 where con.Matricula.TurmaId == turmaId &&
                                 con.DisciplinaId == disciplinaId &&
                                 con.Conceito4Bim == null
                                 select mat).ToList();

                        //query.AddRange(semNota);

                        break;
                }

                if (query != null)
                {
                    query = query.OrderBy(x => x.Aluno.Nome).ToList();

                    bimestre.AddRange(query.Select(item => new Bimestre
                    {
                        MatriculaId = item.Id,
                        Aluno = item.Aluno.Nome
                    }));
                }
            }

            return View(bimestre);
        }

        [HttpPost]
        public ActionResult NotasTurma(List<int> matriculaId, List<string> nota, List<string> falta, int disciplinaId, int periodo, int turmaId)
        {
            var i = 0;
            foreach (var aluno in matriculaId)
            {
                var matricula = _banco.Conceitos.FirstOrDefault(x => x.MatriculaId == aluno && x.DisciplinaId == disciplinaId);

                if (matricula != null)
                {
                    switch (periodo)
                    {
                        case 1:
                            matricula.Conceito1Bim = nota[i]; matricula.Faltas1Bim = falta[i];
                            break;
                        case 2:
                            matricula.Conceito2Bim = nota[i]; matricula.Faltas2Bim = falta[i];
                            break;
                        case 3:
                            matricula.Conceito3Bim = nota[i]; matricula.Faltas3Bim = falta[i];
                            break;
                        case 4:
                            matricula.Conceito4Bim = nota[i]; matricula.Faltas4Bim = falta[i];
                            break;
                    }

                    i++;

                    _banco.Entry(matricula).State = EntityState.Modified;
                    _banco.SaveChanges();

                }

            }

            return RedirectToAction("ConceitoAluno", new { turmaId, disciplinaId, periodo });
        }
        
        [HttpGet]
        public ActionResult ConceitoAluno(int? turmaId, int? disciplinaId, int? alunoId, int? periodo)
        {
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");

            var disciplinas = (from d in _banco.Disciplinas
                               join dpt in _banco.DisciplinaDoProfessoresNasTurmas on d.Id equals dpt.DisciplinaId
                               where d.Id == dpt.DisciplinaId && dpt.TurmaId == turmaId
                               select d).ToList();

            ViewBag.DisciplinaId = new SelectList(disciplinas, "Id", "NomeDisciplina");

            var alunos = (from al in _banco.Alunos join mat in _banco.Matriculas on al.Id equals mat.AlunoId where mat.TurmaId == turmaId select new {mat.Id, al.Nome }).ToList();

            ViewBag.AlunoId = new SelectList(alunos, "Id", "Nome");

            if (periodo != null)
            {
                ViewBag.Bimestre = periodo;
            }
            var bimestre = new List<Bimestre>();

            if (turmaId != null && disciplinaId != null)
            {
                var query = (from con in _banco.Conceitos
                             join mat in _banco.Matriculas on con.MatriculaId equals
                             mat.Id
                             join al in _banco.Alunos on mat.AlunoId equals
                      al.Id
                             where mat.TurmaId == turmaId && con.DisciplinaId == disciplinaId
                             select new { con, mat, al });



                if (alunoId != null)
                {
                    query = query.Where(arg => arg.con.MatriculaId == alunoId);

                    foreach (var item in query)
                    {
                        switch (periodo)
                        {
                            case 1:
                                ViewBag.Nota = item.con.Conceito1Bim; ViewBag.Falta = item.con.Faltas1Bim;
                                break;
                            case 2:
                                ViewBag.Nota = item.con.Conceito2Bim; ViewBag.Falta = item.con.Faltas2Bim;
                                break;
                            case 3:
                                ViewBag.Nota = item.con.Conceito3Bim; ViewBag.Falta = item.con.Faltas3Bim;
                                break;
                            case 4:
                                ViewBag.Nota = item.con.Conceito4Bim; ViewBag.Falta = item.con.Faltas4Bim;
                                break;
                        }
                    }
                }

                switch (periodo)
                {
                    case 1:
                        foreach (var item in query)
                        {
                            bimestre.Add(new Bimestre { Aluno = item.al.Nome, Disciplina = item.con.Disciplina.NomeDisciplina, Falta = item.con.Faltas1Bim, Nota = item.con.Conceito1Bim, Periodo = "1º bimestre" });
                        }
                        break;
                    case 2:
                        foreach (var item in query)
                        {
                            bimestre.Add(new Bimestre { Aluno = item.al.Nome, Disciplina = item.con.Disciplina.NomeDisciplina, Falta = item.con.Faltas2Bim, Nota = item.con.Conceito2Bim, Periodo = "2º bimestre" });
                        }
                        break;
                    case 3:
                        foreach (var item in query)
                        {
                            bimestre.Add(new Bimestre { Aluno = item.al.Nome, Disciplina = item.con.Disciplina.NomeDisciplina, Falta = item.con.Faltas3Bim, Nota = item.con.Conceito3Bim, Periodo = "3º bimestre" });
                        }
                        break;
                    case 4:
                        foreach (var item in query)
                        {
                            bimestre.Add(new Bimestre { Aluno = item.al.Nome, Disciplina = item.con.Disciplina.NomeDisciplina, Falta = item.con.Faltas4Bim, Nota = item.con.Conceito4Bim, Periodo = "4º bimestre" });
                        }
                        break;
                }

            }
            return View(bimestre.OrderBy(x => x.Aluno));
        }

        [HttpPost]
        public ActionResult ConceitoAluno(int turmaId, int alunoId, int disciplinaId, int? periodo, string nota, string falta)
        {
            if (ModelState.IsValid)
            {
                Conceito conceito = new Conceito { MatriculaId = alunoId, DisciplinaId = disciplinaId };

                switch (periodo)
                {
                    case 1:
                        conceito.Conceito1Bim = nota; conceito.Faltas1Bim = falta;
                        break;
                    case 2:
                        conceito.Conceito2Bim = nota; conceito.Faltas2Bim = falta;
                        break;
                    case 3:
                        conceito.Conceito3Bim = nota; conceito.Faltas3Bim = falta;
                        break;
                    case 4:
                        conceito.Conceito4Bim = nota; conceito.Faltas4Bim = falta;
                        break;
                }

                var matricula = _banco.Conceitos.FirstOrDefault(x => x.MatriculaId == alunoId && x.DisciplinaId == disciplinaId);

                if (matricula == null)
                {
                    _banco.Conceitos.Add(conceito);
                    _banco.SaveChanges();
                    return RedirectToAction("ConceitoAluno", new { turmaId, alunoId, disciplinaId, periodo });

                }

                switch (periodo)
                {
                    case 1:
                        matricula.Conceito1Bim = nota; matricula.Faltas1Bim = falta;
                        break;
                    case 2:
                        matricula.Conceito2Bim = nota; matricula.Faltas2Bim = falta;
                        break;
                    case 3:
                        matricula.Conceito3Bim = nota; matricula.Faltas3Bim = falta;
                        break;
                    case 4:
                        matricula.Conceito4Bim = nota; matricula.Faltas4Bim = falta;
                        break;
                }

                _banco.Entry(matricula).State = EntityState.Modified;
                _banco.SaveChanges();

                return RedirectToAction("NotaAluno", new { turmaId, matricula.Matricula.AlunoId, disciplinaId, periodo });
            }
            return RedirectToAction("ConceitoAluno", new { turmaId, alunoId, disciplinaId });
        }   

        public ActionResult BoletimTurma(int? turmaId)
        {
            ViewBag.turmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");


            var conceitos = new List<Conceito>();
            var alunos = new List<Aluno>();


            var turma = _banco.Turmas.FirstOrDefault(x => x.Id == turmaId);

            if (turma != null)
            {
                ViewBag.NomeResponsavel = turma.Funcionario.NomeFuncionario;

                switch (turma.Serie)
                {
                    case "1ANO":
                        ViewBag.SerieTurma = "1º ANO";
                        break;
                    case "2ANO":
                        ViewBag.SerieTurma = "2º ANO";
                        break;
                    case "3ANO":
                        ViewBag.SerieTurma = "3º ANO";
                        break;
                    case "4ANO":
                        ViewBag.SerieTurma = "4º ANO";
                        break;
                    case "5ANO":
                        ViewBag.SerieTurma = "5º ANO";
                        break;
                }
            }


            if (turmaId != null)
            {
                alunos = (from al in _banco.Alunos
                          join mat in _banco.Matriculas on al.Id equals mat.AlunoId
                          join con in _banco.Conceitos on mat.Id equals con.MatriculaId
                          where al.Id == mat.AlunoId &&
                          mat.Id == con.MatriculaId &&
                          mat.TurmaId == turmaId
                          select al).Distinct().ToList();

                conceitos = (from con in _banco.Conceitos
                             join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
                             where con.MatriculaId == mat.Id && mat.TurmaId == turmaId
                             orderby con.DisciplinaId
                             select con).ToList();


            }
            ViewBag.Alunos = alunos.OrderBy(aluno => aluno.Nome);

            return View(conceitos);
        }

        [HttpGet]
        public ActionResult GerenciarNotasTurma(int? turmaId, int? disciplinaId, string etapa)
        {
            ViewBag.turmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");

            var disciplina = (from d in _banco.Disciplinas
                              join dpt in _banco.DisciplinaDoProfessoresNasTurmas on d.Id equals dpt.DisciplinaId
                              where d.Id == dpt.DisciplinaId && dpt.TurmaId == turmaId
                              select d).ToList();

            ViewBag.DisciplinaId = new SelectList(disciplina, "Id", "NomeDisciplina");

            var conceitos = new List<Conceito>();

            if (turmaId != null && disciplinaId != null && etapa != "")
            {
                conceitos = (from con in _banco.Conceitos
                             join mat in _banco.Matriculas on con.MatriculaId equals mat.Id
                             where con.MatriculaId == mat.Id && mat.TurmaId == turmaId &&
                             con.DisciplinaId == disciplinaId
                             select con).ToList();

            }
            return View(conceitos);
        }

        [HttpPost]
        public ActionResult GerenciarNotasTurma(int turmaId, int disciplinaId, List<int> id, List<int> matricula, List<string> conceito1Bim, List<string> faltas1Bim, List<string> conceito2Bim, List<string> faltas2Bim, List<string> conceito3Bim, List<string> faltas3Bim, List<string> conceito4Bim, List<string> faltas4Bim)
        {
            var j = 0;

            foreach (var i in id)
            {
                var conceito = _banco.Conceitos.FirstOrDefault(x => x.Id == i);

                if (conceito != null)
                {
                    conceito.DisciplinaId = disciplinaId;
                    conceito.MatriculaId = matricula[j];
                    conceito.Conceito1Bim = conceito1Bim[j];
                    conceito.Faltas1Bim = faltas1Bim[j];
                    conceito.Conceito2Bim = conceito2Bim[j];
                    conceito.Faltas2Bim = faltas2Bim[j];
                    conceito.Conceito3Bim = conceito3Bim[j];
                    conceito.Faltas3Bim = faltas3Bim[j];
                    conceito.Conceito4Bim = conceito4Bim[j];
                    conceito.Faltas4Bim = faltas4Bim[j];

                    _banco.Entry(conceito).State = EntityState.Modified;
                    _banco.SaveChanges();

                    j++;
                }

            }
            return RedirectToAction("GerenciarNotasTurma", new { turmaId, disciplinaId });

        }

        public ActionResult Editar(long id)
        {
            var conceito = _banco.Conceitos.Find(id);


            return View(conceito);
        }
        [HttpPost]
        public ActionResult Editar(Conceito conceito)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(conceito).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conceito);
        }

        public ActionResult Excluir(long id)
        {
            var conceito = _banco.Conceitos.First(x => x.Id == id);
            if (conceito == null)
                return HttpNotFound();

            return View(conceito);

        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(long id)
        {
            var conceito = _banco.Conceitos.First(x => x.Id == id);
            _banco.Conceitos.Remove(conceito);
            _banco.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}

