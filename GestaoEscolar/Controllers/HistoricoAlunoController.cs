using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using System.Data.Entity.Validation;
using System.Globalization;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class HistoricoAlunoController : Controller
    {
        readonly Contexto _banco = new Contexto();


        public ActionResult BuscaHistoricoAluno(string Nome, DateTime? DataNascimento)
        {
            if (Nome != null)
            {
                var aluno = _banco.Alunos.FirstOrDefault(x => x.Nome.Contains(Nome) || x.DataNascimento.Equals(DataNascimento));

                ViewBag.Nome = aluno.Nome;
                ViewBag.DataNascimento = aluno.DataNascimento.ToShortDateString();
                ViewBag.Sexo = aluno.Sexo;
                ViewBag.NomeMae = aluno.NomeMae;
                ViewBag.NomePai = aluno.NomePai;
                ViewBag.Naturalidade = aluno.Naturalidade;
                ViewBag.UfNaturalidade = aluno.UfNaturalidade;
                ViewBag.Nacionalidade = aluno.Nacionalidade;
                ViewBag.Rg = aluno.Rg;
                ViewBag.OrgaoExpRg = aluno.OrgaoExpRg;
                ViewBag.UfRg = aluno.UfRg;

            }
            return View();
        }

        public ActionResult ListaHistoricoAluno(int id)
        {
            List<HistoricoAluno> histAluno = _banco.HistoricoAlunos.Where(x => x.AlunoId == id).ToList();

            Aluno aluno = _banco.Alunos.FirstOrDefault(x => x.Id == id);

            if (aluno != null)
            {
                ViewBag.NomeAluno = aluno.Nome;
                ViewBag.Id = aluno.Id;
            }


            return View(histAluno);
        }

        public ActionResult Index(string termoBusca)
        {

            var historicoaluno = _banco.HistoricoAlunos.Include("Aluno").ToList();

            if (!String.IsNullOrEmpty(termoBusca))
            {
                historicoaluno = historicoaluno.Where(x => x.Aluno.Nome.ToUpper().Contains(termoBusca.ToUpper())).ToList();
            }

            return View(historicoaluno);
        }

        public ActionResult Adicionar(int? alunoId)
        {
            var aluno = _banco.Alunos.FirstOrDefault(x => x.Id == alunoId);

            ViewBag.AlunoId = new SelectList(_banco.Alunos, "Id", "Nome");

            if (aluno != null)
            {
                ViewBag.AlunoId = new SelectList(new[] { aluno }, "Id", "Nome");
            }




            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(HistoricoAluno novoHistoricoAluno)
        {
            if (ModelState.IsValid)
            {
                _banco.HistoricoAlunos.Add(novoHistoricoAluno);
                _banco.SaveChanges();
                return RedirectToAction("ListaHistoricoAluno", new { Id = novoHistoricoAluno.AlunoId });
            }
            ViewBag.AlunoId = new SelectList(_banco.Alunos, "Id", "Nome", novoHistoricoAluno.AlunoId);

            return View(novoHistoricoAluno);
        }//fim adicionar

        public ActionResult Detalhes(long id)
        {
            var historicoaluno = _banco.HistoricoAlunos.Include("Aluno").First(x => x.Id == id);


            if (historicoaluno == null)
            {
                return HttpNotFound();
            }
            return View(historicoaluno);
        }

        public ActionResult Editar(long id)
        {
            HistoricoAluno historicoaluno = _banco.HistoricoAlunos.Find(id);

            ViewBag.DataHistoricoAluno = historicoaluno.DataHistoricoAluno.ToShortDateString();

            ViewBag.AlunoId = new SelectList(_banco.Alunos, "Id", "Nome", historicoaluno.AlunoId);

            return View(historicoaluno);
        }

        [HttpPost]
        public ActionResult Editar(HistoricoAluno historicoAluno)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(historicoAluno).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("ListaHistoricoAluno", new { Id = historicoAluno.AlunoId });
            }
            ViewBag.AlunoId = new SelectList(_banco.Alunos, "Id", "Nome", historicoAluno.AlunoId);

            return View(historicoAluno);
        }

        public ActionResult Excluir(long id)
        {
            var historicoaluno = _banco.HistoricoAlunos.First(x => x.Id == id);
            _banco.HistoricoAlunos.Remove(historicoaluno);
            _banco.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GerarHistoricoAluno(long id)
        {
            var aluno = _banco.Matriculas.FirstOrDefault(x => x.Aluno.Id == id);

            HistoricoAluno historico = new HistoricoAluno();

            if (aluno != null)
            {
                historico.AlunoId = aluno.AlunoId;
                historico.DataHistoricoAluno = DateTime.Now.Date;
                historico.AnoHistoricoAluno = Convert.ToInt32(aluno.AnoLetivo.Ano);
                historico.ChCurricularBasica = Convert.ToString(aluno.AnoLetivo.ChCurrBas, CultureInfo.InvariantCulture);
                historico.ChCurricularArtes = Convert.ToString(aluno.AnoLetivo.ChCurrArt, CultureInfo.InvariantCulture);
                historico.TurmaAnoHistorico = aluno.Turma.Serie;
                historico.EscolaAnoAluno = aluno.Turma.Escola.NomeEscola;
                historico.CidadeEscolaAnoAluno = aluno.Turma.Escola.EndEscola;
                historico.UfEscolaAnoAluno = aluno.Turma.Escola.UfEscola;
            }
            historico.DiasLetivos = 200;


            var conceitos = (from con in _banco.Conceitos
                             where con.Matricula.AlunoId == id
                             select con).ToArray();

            foreach (var con in conceitos)
            {
                switch (con.Disciplina.Id)
                {
                    case 1:
                        historico.AprovMatematica = con.Conceito4Bim;
                        break;
                    case 2:
                        historico.AprovPortugues = con.Conceito4Bim;
                        break;
                    case 3:
                        historico.AprovArte = con.Conceito4Bim;
                        break;
                    case 4:
                        historico.AprovEduFisica = con.Conceito4Bim;
                        break;
                    case 5:
                        historico.AprovHistoria = con.Conceito4Bim;
                        break;
                    case 6:
                        historico.AprovGeografia = con.Conceito4Bim;
                        break;
                    case 7:
                        historico.AprovEnsReligioso = con.Conceito4Bim;
                        break;
                    case 8:
                        historico.AprovCiencia = con.Conceito4Bim;
                        break;
                    case 9:
                        historico.AprovInformatica = con.Conceito4Bim;
                        break;
                    case 10:
                        historico.AprovLitInfantilJuvenil = con.Conceito4Bim;
                        break;
                }
            }
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                if (ModelState.IsValid)
                {
                    _banco.HistoricoAlunos.Add(historico);
                    _banco.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            //if (ModelState.IsValid)
            //{
            //    _banco.HistoricoAlunos.Add(historico);
            //    _banco.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return RedirectToAction("Index");
        }

    }
}
