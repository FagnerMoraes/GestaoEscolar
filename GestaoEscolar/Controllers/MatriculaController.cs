using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using PagedList;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class MatriculaController : Controller
    {
        readonly Contexto _banco = new Contexto();

        public ActionResult Index(int? pagina)
        {
            const int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            var matricula = _banco.Matriculas.Include(x => x.Aluno).Include(x => x.Turma).OrderByDescending(x => x.DataCadastro).ToList();

            return View(matricula.ToPagedList(numeroPagina, tamanhoPagina));
        }

        public ActionResult Adicionar()
        {
            var alunos = (from al in _banco.Alunos
                          join mat in _banco.Matriculas on al.Id equals mat.AlunoId
                          into aluno
                          from mat in aluno.DefaultIfEmpty()
                          where al.Id != mat.AlunoId || mat.TurmaId == null
                          select al).ToList();

            ViewBag.AlunoId = new SelectList(alunos, "Id", "Nome");
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");
            ViewBag.AnoLetivoId = new SelectList(_banco.AnoLetivos, "Id", "Ano");

            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Matricula novaMatriculaAluno)
        {
            if (ModelState.IsValid)
            {
                _banco.Matriculas.Add(novaMatriculaAluno);
                _banco.SaveChanges();
            }

            var aluno = _banco.Alunos.FirstOrDefault(x => x.Id == novaMatriculaAluno.AlunoId);

            var disciplinas = (from d in _banco.Disciplinas
                               join dpt in _banco.DisciplinaDoProfessoresNasTurmas on d.Id equals dpt.DisciplinaId
                               where d.Id == dpt.DisciplinaId && dpt.TurmaId == novaMatriculaAluno.TurmaId
                               select d).ToList();


            var mat = _banco.Matriculas.FirstOrDefault(x => x.AlunoId == aluno.Id);

            var con = _banco.Conceitos.FirstOrDefault(x => x.MatriculaId == mat.Id);

            if (mat != null && con == null)
            {
                var conceito = new Conceito
                {
                    MatriculaId = mat.Id
                };
                //matricular nas disciplinas da turma
                foreach (var d in disciplinas)
                {
                    conceito.DisciplinaId = d.Id;
                    _banco.Conceitos.Add(conceito);
                    _banco.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.AlunoId = new SelectList(_banco.Alunos, "Id", "Nome", novaMatriculaAluno.AlunoId);
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma", novaMatriculaAluno.TurmaId);

            return View(novaMatriculaAluno);
        }//fim adicionar

        [HttpGet]
        public ActionResult AdicionarComId(long id)
        {

            var aluno = _banco.Alunos.FirstOrDefault(x => x.Id == id);

            if (aluno != null)
            {
                ViewBag.AlunoId = aluno.Id;
                ViewBag.NomeAluno = aluno.Nome;
            }
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");
            ViewBag.AnoLetivoId = new SelectList(_banco.AnoLetivos, "Id", "Ano");

            return View();


        }

        [HttpPost]
        public ActionResult AdicionarComId(Matricula novaMatriculaAluno)
        {
            if (ModelState.IsValid)
            {
                _banco.Matriculas.Add(novaMatriculaAluno);
                _banco.SaveChanges();
            }

            var aluno = _banco.Alunos.FirstOrDefault(x => x.Id == novaMatriculaAluno.AlunoId);

            var disciplinas = (from d in _banco.Disciplinas
                               join dpt in _banco.DisciplinaDoProfessoresNasTurmas on d.Id equals dpt.DisciplinaId
                               where d.Id == dpt.DisciplinaId && dpt.TurmaId == novaMatriculaAluno.TurmaId
                               select d).ToList();


            var mat = _banco.Matriculas.FirstOrDefault(x => x.AlunoId == aluno.Id);

            var con = _banco.Conceitos.FirstOrDefault(x => x.MatriculaId == mat.Id);

            if (mat != null && con == null)
            {
                var conceito = new Conceito
                {
                    MatriculaId = mat.Id
                };

                foreach (var d in disciplinas)
                {
                    conceito.DisciplinaId = d.Id;
                    _banco.Conceitos.Add(conceito);
                    _banco.SaveChanges();
                }
                return RedirectToAction("Index");
            }


            if (aluno != null) ViewBag.NomeAluno = aluno.Nome;
            ViewBag.AlunoId = novaMatriculaAluno.AlunoId;
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma", novaMatriculaAluno.TurmaId);

            return View(novaMatriculaAluno);
        }//fim adicionar

        [HttpGet]
        public ActionResult GerenciarMatriculas(int? turmaId, int? anoLetivoId)
        {
            ViewBag.turmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");
            ViewBag.anoLetivoId = new SelectList(_banco.AnoLetivos, "Id", "Ano");

            if (turmaId != null) { ViewBag.turmaSelecionada = turmaId; }
            if (anoLetivoId != null) { ViewBag.anoLetivoselecionado = anoLetivoId; }


            ViewBag.NaoMatriculados = (from al in _banco.Alunos
                                       join mat in _banco.Matriculas on al.Id equals mat.AlunoId
                                       into aluno
                                       from mat in aluno.DefaultIfEmpty()
                                       where al.Id != mat.AlunoId || mat.TurmaId == null
                                       select al).OrderBy(x => x.Nome).ToList();

            var matriculados =
                (from al in _banco.Alunos
                 join mat in _banco.Matriculas on al.Id equals mat.AlunoId
                 where al.Id == mat.AlunoId && mat.TurmaId != null && mat.TurmaId == turmaId
                 select al).OrderBy(x => x.Nome).ToList();

            ViewBag.Matriculados = matriculados;

            var turma = _banco.Turmas.FirstOrDefault(x => x.Id == turmaId);

            if (turma != null)
            {
                ViewBag.QtdAluno = Convert.ToInt32(turma.QtdAlunos);

                ViewBag.QtdVagas = ViewBag.QtdAluno - matriculados.Count;
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Enturmar(int turmaSelecionada, int anoLetivoselecionado, List<int> alunos)
        {
            if(alunos == null)
            {
                return RedirectToAction("GerenciarMatriculas", new { turmaId = turmaSelecionada, anoLetivoId = anoLetivoselecionado });
            }

            var novaMatricula = new Matricula
            {
                TurmaId = turmaSelecionada,
                AnoLetivoId = anoLetivoselecionado
            };

            var matriculasSemTurma = _banco.Matriculas.Where(x => x.TurmaId == null).ToList();

            var matriculadosSemTurma = (from mat in _banco.Matriculas
                                        join al in alunos on mat.AlunoId equals al
                                        where mat.TurmaId == null && mat.AlunoId == al
                                        select al).ToList();

            var semMatricula = (from al in _banco.Alunos
                                join mat in _banco.Matriculas on al.Id equals mat.AlunoId
                                into aluno
                                from mat in aluno.DefaultIfEmpty()
                                join selecao in alunos on al.Id equals selecao
                                where al.Id == selecao && al.Id != mat.AlunoId
                                select selecao).ToList();

            if (matriculadosSemTurma.Count != 0)
            {
                foreach (var i in matriculadosSemTurma)
                {
                    foreach (var matriculado in matriculasSemTurma)
                    {
                        if (i == matriculado.AlunoId)
                        {
                            matriculado.TurmaId = turmaSelecionada;
                            _banco.Entry(matriculado).State = EntityState.Modified;
                            _banco.SaveChanges();
                        }
                    }

                }

            }

            if (semMatricula.Count != 0)
            {
                var disciplinas = (from d in _banco.Disciplinas
                                   join dpt in _banco.DisciplinaDoProfessoresNasTurmas on d.Id equals dpt.DisciplinaId
                                   where d.Id == dpt.DisciplinaId && dpt.TurmaId == turmaSelecionada
                                   select d).ToList();


                foreach (var i in semMatricula)
                {
                    novaMatricula.AlunoId = i;
                    _banco.Matriculas.Add(novaMatricula);
                    _banco.SaveChanges();
                }


                foreach (var i in semMatricula)
                {
                    

                    var mat = _banco.Matriculas.FirstOrDefault(x => x.AlunoId == i);
                    if (mat != null)
                    {
                        var conceito = new Conceito
                        {
                            MatriculaId = mat.Id
                        };

                        foreach (var d in disciplinas)
                        {
                            conceito.DisciplinaId = d.Id;
                            _banco.Conceitos.Add(conceito);
                            _banco.SaveChanges();
                        }
                    }
                }
            }

            return RedirectToAction("GerenciarMatriculas", new { turmaId = turmaSelecionada, anoLetivoId = anoLetivoselecionado });


        }

        [HttpPost]
        public ActionResult Desenturmar(int turmaSelecionada, int anoLetivoselecionado, List<int> alunos)
        {
            if (alunos == null)
            {
                return RedirectToAction("GerenciarMatriculas", new { turmaId = turmaSelecionada, anoLetivoId = anoLetivoselecionado });
            }

            foreach (var aluno in alunos)
            {
                var matriculas = _banco.Matriculas.FirstOrDefault(x => x.AlunoId == aluno);


                if (matriculas != null)
                {
                    matriculas.TurmaId = null;
                    _banco.Entry(matriculas).State = EntityState.Modified;
                }
                _banco.SaveChanges();


                //_banco.Matriculas.Remove(matriculas);
                //_banco.SaveChanges();

            }


            return RedirectToAction("GerenciarMatriculas", new { turmaId = turmaSelecionada, anoLetivoId = anoLetivoselecionado });
        }
        

        public ActionResult Detalhes(long id)
        {
            var matricula = _banco.Matriculas.Include(x => x.Aluno).Include(x => x.Turma).First(x => x.Id == id);


            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        public ActionResult Editar(long id)
        {
            Matricula matricula = _banco.Matriculas.Find(id);

            ViewBag.AlunoId = new SelectList(_banco.Alunos, "Id", "Nome", matricula.AlunoId);
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma", matricula.TurmaId);
            ViewBag.AnoLetivoId = new SelectList(_banco.AnoLetivos, "Id", "Ano", matricula.AnoLetivoId);

            ViewBag.DataCadastro = matricula.DataCadastro.ToShortDateString();

            return View(matricula);
        }

        [HttpPost]
        public ActionResult Editar(Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(matricula).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlunoId = new SelectList(_banco.Alunos, "Id", "Nome", matricula.AlunoId);
            ViewBag.TurmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma", matricula.TurmaId);

            return View(matricula);
        }

        public ActionResult Excluir(long id)
        {
            var matricula = _banco.Matriculas.First(x => x.Id == id);
            _banco.Matriculas.Remove(matricula);

            try
            {
                _banco.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Request.UrlReferrer != null) ViewBag.Voltar = Request.UrlReferrer.ToString();
                return View("Error", new HandleErrorInfo(ex, "Matricula", "Index"));
            }
        }

    }
}
