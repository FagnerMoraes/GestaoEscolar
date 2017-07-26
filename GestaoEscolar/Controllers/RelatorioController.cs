using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using Rotativa;
using Rotativa.Options;
using GestaoEscolar.DAO;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class RelatorioController : Controller
    {
        readonly Contexto _banco = new Contexto();
        private RelatorioDAO dao;

        public RelatorioController(RelatorioDAO dao)
        {
            this.dao = dao;
        }

        public ActionResult Index(int? alunoId)
        {
            var alunos = (from al in _banco.Alunos
                          join mat in _banco.Matriculas on al.Id equals mat.AlunoId
                          join tur in _banco.Turmas on mat.TurmaId equals tur.Id
                          where al.Id == mat.AlunoId && tur != null
                          select al).ToList();


            ViewBag.AlunoId = new SelectList(alunos, "Id", "Nome");

            if (alunoId != null)
            {
                ViewBag.CodAluno = alunoId;
            }

            Dictionary<string, string> ListaRelatorios = new Dictionary<string, string>();

            ListaRelatorios.Add("Historico Aluno", "Relatorio/ModalHistorico");
            ListaRelatorios.Add("Declaracao de Matricula", "Relatorio/ModalDecMatricula");

            ViewBag.ListaRelatorios = ListaRelatorios;                     

            return View();
        }
        [HttpPost]
        public ActionResult Index(string termoBusca)
        {
            Dictionary<string, string> ListaRelatorios = new Dictionary<string, string>();

            ListaRelatorios.Add("Historico Aluno", "Relatorio/ModalHistorico");
            ListaRelatorios.Add("Declaracao de Matricula", "Relatorio/ModalDecMatricula");

            if (termoBusca != null)
                ViewBag.ListaRelatorios = ListaRelatorios.Where(arg => arg.Key.ToUpper().Contains(termoBusca.ToUpper())).ToList();

            if (Request.IsAjaxRequest())
                return PartialView("_ListaRelatorio");

            return View();
        }


        public ActionResult Boletim(int id, int periodo)
        {

            var matricula = dao.BuscaMatriculaPorID(id);

            var bimestre = new List<Bimestre>();

            var query = dao.ConceitosPorId(id);

            var conceitoFormacao = dao.ConceitosFormacaoPorIdPeriodo(id,periodo);

            //incluir a Formação no Boletim por Periodo
            foreach (var con in conceitoFormacao)
            {
                //Verificar qual periodo quer buscar a Formação
                switch (con.Periodo)
                {
                    case 1:
                        ViewBag.AtitVal = con.AtitVal;
                        ViewBag.CompAssid = con.CompAssid;
                        ViewBag.CriCriti = con.CriCriti;
                        ViewBag.PartFamilia = con.PartFamilia;
                        break;
                    case 2:
                        ViewBag.AtitVal = con.AtitVal;
                        ViewBag.CompAssid = con.CompAssid;
                        ViewBag.CriCriti = con.CriCriti;
                        ViewBag.PartFamilia = con.PartFamilia;
                        break;
                    case 3:
                        ViewBag.AtitVal = con.AtitVal;
                        ViewBag.CompAssid = con.CompAssid;
                        ViewBag.CriCriti = con.CriCriti;
                        ViewBag.PartFamilia = con.PartFamilia;
                        break;
                    case 4:
                        ViewBag.AtitVal = con.AtitVal;
                        ViewBag.CompAssid = con.CompAssid;
                        ViewBag.CriCriti = con.CriCriti;
                        ViewBag.PartFamilia = con.PartFamilia;
                        break;
                }
            }

            //Verificar qual de qual periodo quer incluir no Bimestre do boletim
            switch (periodo)
            {
                case 1:
                    bimestre.AddRange(query.Select(item => new Bimestre
                    {
                        Aluno = item.Matricula.Aluno.Nome,
                        Disciplina = item.Disciplina.NomeDisciplina,
                        Falta = item.Faltas1Bim,
                        Nota = item.Conceito1Bim,
                        Periodo = "1º bimestre"
                    }));
                    break;
                case 2:
                    bimestre.AddRange(query.Select(item => new Bimestre
                    {
                        Aluno = item.Matricula.Aluno.Nome,
                        Disciplina = item.Disciplina.NomeDisciplina,
                        Falta = item.Faltas2Bim,
                        Nota = item.Conceito2Bim,
                        Periodo = "2º bimestre"
                    }));
                    break;
                case 3:
                    bimestre.AddRange(query.Select(item => new Bimestre
                    {
                        Aluno = item.Matricula.Aluno.Nome,
                        Disciplina = item.Disciplina.NomeDisciplina,
                        Falta = item.Faltas3Bim,
                        Nota = item.Conceito3Bim,
                        Periodo = "3º bimestre"
                    }));
                    break;
                case 4:
                    bimestre.AddRange(query.Select(item => new Bimestre
                    {
                        Aluno = item.Matricula.Aluno.Nome,
                        Disciplina = item.Disciplina.NomeDisciplina,
                        Falta = item.Faltas4Bim,
                        Nota = item.Conceito4Bim,
                        Periodo = "4º bimestre"
                    }));
                    break;
            }

            foreach (var dis in bimestre)
            {
                switch (dis.Disciplina)
                {
                    case "LÍNGUA PORTUGUESA": ViewBag.ConLinPortuguesa = dis.Nota; break;
                    case "ARTE": ViewBag.ConArte = dis.Nota; break;
                    case "EDUCACAO FISICA": ViewBag.ConEdFisica = dis.Nota; break;
                    case "HISTORIA": ViewBag.ConHistoria = dis.Nota; break;
                    case "GEOGRAFIA": ViewBag.ConGeografia = dis.Nota; break;
                    case "ENSINO RELIGIOSO": ViewBag.ConEnReligioso = dis.Nota; break;
                    case "CIÊNCIAS DA NATUREZA": ViewBag.ConCiencias = dis.Nota; break;
                    case "MATEMATICA": ViewBag.ConMatematica = dis.Nota; break;
                    case "INFORMATICA": ViewBag.ConInformatica = dis.Nota; break;
                    case "LITERATURA INFANTO JUVENIL": ViewBag.ConLitInfJuvenil = dis.Nota; break;
                }
            }

            ViewBag.Falta = bimestre.Where(f => f.Falta != "").Aggregate(0, (current, f) => (current + Convert.ToInt32(f.Falta)));

            if (matricula != null)
            {
                ViewBag.Aluno = matricula.Aluno.Nome;
                ViewBag.Serie = matricula.Turma.Serie;
                ViewBag.AnoLetivo = matricula.AnoLetivo.Ano;
                ViewBag.NomeResponsavel = matricula.Turma.Funcionario.NomeFuncionario;
                ViewBag.Turma = matricula.Turma.NomeTurma;
                ViewBag.Turno = matricula.Turma.HorarioFuncionamento;
                ViewBag.Nivel = matricula.Turma.NivelTurma;
            }


            var bim = (from b in bimestre select b.Periodo);

            var firstOrDefault = bim.FirstOrDefault();
            if (firstOrDefault != null) ViewBag.Periodo = firstOrDefault.ToUpper();

            return View(bimestre);
        }

        public ActionResult Historico(int id)
        {
            var aluno = _banco.Alunos.FirstOrDefault(x => x.Id == id);

            if (aluno == null)
            {
                Response.Redirect("~/HistoricoAluno/Index");
            }
            else
            {
                ViewBag.Nome = aluno.Nome;
                ViewBag.Naturalidade = aluno.Naturalidade;
                ViewBag.UFNaturalidade = aluno.UfNaturalidade;
                ViewBag.Nacionalidade = aluno.Nacionalidade;
                ViewBag.Sexo = aluno.Sexo;
                ViewBag.DataNascimento = aluno.DataNascimento.Date.ToString("dd DE  MMMMMMMMMMMMMM DE yyyy");
                ViewBag.Pai = aluno.NomePai;
                ViewBag.Mae = aluno.NomeMae;

                ViewBag.Rg = aluno.Rg ?? "------------";

                ViewBag.Orgao = aluno.OrgaoExpRg ?? "-------";

                ViewBag.UfRg = aluno.UfRg ?? "-------";


                ViewBag.DateHistorico = "14 DE DEZEMBRO DE 2016";
                //DateTime.Now.ToString("dd DE  MMMMMMMMMMMMMM DE yyyy");

            }

            var primeiroAnoHistorico = _banco.HistoricoAlunos.Include("Aluno").FirstOrDefault(x => x.TurmaAnoHistorico.Equals("1Ano") && x.AlunoId == id);

            if (primeiroAnoHistorico == null)
            {
                ViewBag.Ano1TurmaAnoHistorico = "----";

                ViewBag.Ano1AprovPortugues = "-";
                ViewBag.Ano1AprovMatematica = "-";
                ViewBag.Ano1AprovGeografia = "-";
                ViewBag.Ano1AprovHistoria = "-";
                ViewBag.Ano1AprovCiencia = "-";
                ViewBag.Ano1AprovEduFisica = "-";
                ViewBag.Ano1AprovEnsReligioso = "-";
                ViewBag.Ano1AprovArte = "-";
                ViewBag.Ano1AprovInformatica = "-";
                ViewBag.Ano1AprovLitInfantilJuvenil = "-";
                ViewBag.Ano1AprovAtividadeLingMat = "-";
                ViewBag.Ano1AprovAtividadesEspMot = "-";
                ViewBag.Ano1AprovFormacaoPessoal = "-";
                ViewBag.Ano1ChCurricularBasica = "----";
                ViewBag.Ano1ChCurricularArtes = "----";
                ViewBag.Ano1ChCurricularInformatica = "----";
                ViewBag.Ano1ChCurricularLitInfantoJuvenil = "----";
                ViewBag.Ano1ChCurricularProjetoTempoIntegral = "----";

                ViewBag.Ano1QtdFaltaHoraAnoBasica = "----";
                ViewBag.Ano1QtdFaltaHoraAnoParte = "----";
                ViewBag.Ano1QtdFaltaHoraAnoProjeto = "----";

                ViewBag.Ano1SituacaoAluno = "----";
                ViewBag.Ano1Totalchcurricular = "----";
                ViewBag.Ano1TotalFaltasHoras = "----";


                ViewBag.Ano1AnoHistorico = "----";
                ViewBag.Ano1DiasLetivos = "----";
                ViewBag.Ano1EscolaAnoAluno = "--------------------------------------------";
                ViewBag.Ano1CidadeEscolaAnoAluno = "---------------";
            }
            else
            {
                var primeiroAnototalchcurricular = (Convert.ToDouble(primeiroAnoHistorico.ChCurricularBasica) + Convert.ToDouble(primeiroAnoHistorico.ChCurricularArtes));
                var primeiroAnoTotalFaltasHoras = (Convert.ToDouble(primeiroAnoHistorico.QtdFaltaHoraAnoBasica) +
                                                   Convert.ToDouble(primeiroAnoHistorico.QtdFaltaHoraAnoParte) +
                                                   Convert.ToDouble(primeiroAnoHistorico.QtdFaltaHoraAnoProjeto));


                //primeiro Ano
                ViewBag.Ano1TurmaAnoHistorico = primeiroAnoHistorico.TurmaAnoHistorico;

                ViewBag.Ano1AprovPortugues = primeiroAnoHistorico.AprovPortugues;
                ViewBag.Ano1AprovMatematica = primeiroAnoHistorico.AprovMatematica;
                ViewBag.Ano1AprovGeografia = primeiroAnoHistorico.AprovGeografia;
                ViewBag.Ano1AprovHistoria = primeiroAnoHistorico.AprovHistoria;
                ViewBag.Ano1AprovCiencia = primeiroAnoHistorico.AprovCiencia;
                ViewBag.Ano1AprovEduFisica = primeiroAnoHistorico.AprovEduFisica;
                ViewBag.Ano1AprovEnsReligioso = primeiroAnoHistorico.AprovEnsReligioso;

                ViewBag.Ano1AprovArte = string.IsNullOrEmpty(primeiroAnoHistorico.AprovArte) ? "-" : primeiroAnoHistorico.AprovArte;
                ViewBag.Ano1AprovInformatica = string.IsNullOrEmpty(primeiroAnoHistorico.AprovInformatica) ? "-" : primeiroAnoHistorico.AprovInformatica;
                ViewBag.Ano1AprovLitInfantilJuvenil = string.IsNullOrEmpty(primeiroAnoHistorico.AprovLitInfantilJuvenil) ? "-" : primeiroAnoHistorico.AprovLitInfantilJuvenil;
                ViewBag.Ano1AprovAtividadeLingMat = string.IsNullOrEmpty(primeiroAnoHistorico.AprovAtividadeLingMat) ? "-" : primeiroAnoHistorico.AprovAtividadeLingMat;
                ViewBag.Ano1AprovAtividadesEspMot = string.IsNullOrEmpty(primeiroAnoHistorico.AprovAtividadesEspMot) ? "-" : primeiroAnoHistorico.AprovAtividadesEspMot;
                ViewBag.Ano1AprovFormacaoPessoal = string.IsNullOrEmpty(primeiroAnoHistorico.AprovFormacaoPessoal) ? "-" : primeiroAnoHistorico.AprovFormacaoPessoal;
                ViewBag.Ano1ChCurricularBasica = string.IsNullOrEmpty(primeiroAnoHistorico.ChCurricularBasica) ? "-" : primeiroAnoHistorico.ChCurricularBasica;
                ViewBag.Ano1ChCurricularArtes = string.IsNullOrEmpty(primeiroAnoHistorico.ChCurricularArtes) ? "-" : primeiroAnoHistorico.ChCurricularArtes;
                ViewBag.Ano1ChCurricularInformatica = string.IsNullOrEmpty(primeiroAnoHistorico.ChCurricularInformatica) ? "-" : primeiroAnoHistorico.ChCurricularInformatica;
                ViewBag.Ano1ChCurricularLitInfantoJuvenil = string.IsNullOrEmpty(primeiroAnoHistorico.ChCurricularLitInfantoJuvenil) ? "-" : primeiroAnoHistorico.ChCurricularLitInfantoJuvenil;
                ViewBag.Ano1ChCurricularProjetoTempoIntegral = string.IsNullOrEmpty(primeiroAnoHistorico.ChCurricularProjetoTempoIntegral) ? "-" : primeiroAnoHistorico.ChCurricularProjetoTempoIntegral;

                ViewBag.Ano1QtdFaltaHoraAnoBasica = string.IsNullOrEmpty(primeiroAnoHistorico.QtdFaltaHoraAnoBasica) ? "-" : primeiroAnoHistorico.QtdFaltaHoraAnoBasica;
                ViewBag.Ano1QtdFaltaHoraAnoParte = string.IsNullOrEmpty(primeiroAnoHistorico.QtdFaltaHoraAnoParte) ? "-" : primeiroAnoHistorico.QtdFaltaHoraAnoParte;
                ViewBag.Ano1QtdFaltaHoraAnoProjeto = string.IsNullOrEmpty(primeiroAnoHistorico.QtdFaltaHoraAnoProjeto) ? "-" : primeiroAnoHistorico.QtdFaltaHoraAnoProjeto;


                ViewBag.Ano1SituacaoAluno = primeiroAnoHistorico.SituacaoAluno;
                ViewBag.Ano1Totalchcurricular = primeiroAnototalchcurricular;
                ViewBag.Ano1TotalFaltasHoras = primeiroAnoTotalFaltasHoras;


                ViewBag.Ano1AnoHistorico = primeiroAnoHistorico.AnoHistoricoAluno;
                ViewBag.Ano1DiasLetivos = primeiroAnoHistorico.DiasLetivos;
                ViewBag.Ano1EscolaAnoAluno = primeiroAnoHistorico.EscolaAnoAluno;
                ViewBag.Ano1CidadeEscolaAnoAluno = primeiroAnoHistorico.CidadeEscolaAnoAluno;

            }

            var segundoAnoHistorico = _banco.HistoricoAlunos.Include("Aluno").FirstOrDefault(x => x.TurmaAnoHistorico.Equals("2Ano") && x.AlunoId == id);

            if (segundoAnoHistorico == null)
            {
                ViewBag.Ano2TurmaAnoHistorico = "----";

                ViewBag.Ano2AprovPortugues = "-";
                ViewBag.Ano2AprovMatematica = "-";
                ViewBag.Ano2AprovGeografia = "-";
                ViewBag.Ano2AprovHistoria = "-";
                ViewBag.Ano2AprovCiencia = "-";
                ViewBag.Ano2AprovEduFisica = "-";
                ViewBag.Ano2AprovEnsReligioso = "-";
                ViewBag.Ano2AprovArte = "-";
                ViewBag.Ano2AprovInformatica = "-";
                ViewBag.Ano2AprovLitInfantilJuvenil = "-";
                ViewBag.Ano2AprovAtividadeLingMat = "-";
                ViewBag.Ano2AprovAtividadesEspMot = "-";
                ViewBag.Ano2AprovFormacaoPessoal = "-";
                ViewBag.Ano2ChCurricularBasica = "----";
                ViewBag.Ano2ChCurricularArtes = "----";
                ViewBag.Ano2ChCurricularInformatica = "----";
                ViewBag.Ano2ChCurricularLitInfantoJuvenil = "----";
                ViewBag.Ano2ChCurricularProjetoTempoIntegral = "----";

                ViewBag.Ano2QtdFaltaHoraAnoBasica = "----";
                ViewBag.Ano2QtdFaltaHoraAnoParte = "----";
                ViewBag.Ano2QtdFaltaHoraAnoProjeto = "----";

                ViewBag.Ano2SituacaoAluno = "----";
                ViewBag.Ano2Totalchcurricular = "----";
                ViewBag.Ano2TotalFaltasHoras = "----";


                ViewBag.Ano2AnoHistorico = "----";
                ViewBag.Ano2DiasLetivos = "----";
                ViewBag.Ano2EscolaAnoAluno = "--------------------------------------------";
                ViewBag.Ano2CidadeEscolaAnoAluno = "---------------";
            }
            else
            {
                var segundoAnototalchcurricular = (Convert.ToDouble(segundoAnoHistorico.ChCurricularBasica) +
                                                    Convert.ToDouble(segundoAnoHistorico.ChCurricularArtes));
                var segundoAnoTotalFaltasHoras = (Convert.ToDouble(segundoAnoHistorico.QtdFaltaHoraAnoBasica) +
                                                   Convert.ToDouble(segundoAnoHistorico.QtdFaltaHoraAnoParte) +
                                                   Convert.ToDouble(segundoAnoHistorico.QtdFaltaHoraAnoProjeto));


                //primeiro Ano
                ViewBag.Ano2TurmaAnoHistorico = segundoAnoHistorico.TurmaAnoHistorico;

                ViewBag.Ano2AprovPortugues = segundoAnoHistorico.AprovPortugues;
                ViewBag.Ano2AprovMatematica = segundoAnoHistorico.AprovMatematica;
                ViewBag.Ano2AprovGeografia = segundoAnoHistorico.AprovGeografia;
                ViewBag.Ano2AprovHistoria = segundoAnoHistorico.AprovHistoria;
                ViewBag.Ano2AprovCiencia = segundoAnoHistorico.AprovCiencia;
                ViewBag.Ano2AprovEduFisica = segundoAnoHistorico.AprovEduFisica;
                ViewBag.Ano2AprovEnsReligioso = segundoAnoHistorico.AprovEnsReligioso;
                ViewBag.Ano2AprovArte = string.IsNullOrEmpty(segundoAnoHistorico.AprovArte) ? "-" : segundoAnoHistorico.AprovArte;
                ViewBag.Ano2AprovInformatica = string.IsNullOrEmpty(segundoAnoHistorico.AprovInformatica) ? "-" : segundoAnoHistorico.AprovInformatica;
                ViewBag.Ano2AprovLitInfantilJuvenil = string.IsNullOrEmpty(segundoAnoHistorico.AprovLitInfantilJuvenil) ? "-" : segundoAnoHistorico.AprovLitInfantilJuvenil;
                ViewBag.Ano2AprovAtividadeLingMat = string.IsNullOrEmpty(segundoAnoHistorico.AprovAtividadeLingMat) ? "-" : segundoAnoHistorico.AprovAtividadeLingMat;
                ViewBag.Ano2AprovAtividadesEspMot = string.IsNullOrEmpty(segundoAnoHistorico.AprovAtividadesEspMot) ? "-" : segundoAnoHistorico.AprovAtividadesEspMot;
                ViewBag.Ano2AprovFormacaoPessoal = string.IsNullOrEmpty(segundoAnoHistorico.AprovFormacaoPessoal) ? "-" : segundoAnoHistorico.AprovFormacaoPessoal;
                ViewBag.Ano2ChCurricularBasica = string.IsNullOrEmpty(segundoAnoHistorico.ChCurricularBasica) ? "-" : segundoAnoHistorico.ChCurricularBasica;
                ViewBag.Ano2ChCurricularArtes = string.IsNullOrEmpty(segundoAnoHistorico.ChCurricularArtes) ? "-" : segundoAnoHistorico.ChCurricularArtes;
                ViewBag.Ano2ChCurricularInformatica = string.IsNullOrEmpty(segundoAnoHistorico.ChCurricularInformatica) ? "-" : segundoAnoHistorico.ChCurricularInformatica;
                ViewBag.Ano2ChCurricularLitInfantoJuvenil = string.IsNullOrEmpty(segundoAnoHistorico.ChCurricularLitInfantoJuvenil) ? "-" : segundoAnoHistorico.ChCurricularLitInfantoJuvenil;
                ViewBag.Ano2ChCurricularProjetoTempoIntegral = string.IsNullOrEmpty(segundoAnoHistorico.ChCurricularProjetoTempoIntegral) ? "-" : segundoAnoHistorico.ChCurricularProjetoTempoIntegral;

                ViewBag.Ano2QtdFaltaHoraAnoBasica = string.IsNullOrEmpty(segundoAnoHistorico.QtdFaltaHoraAnoBasica) ? "-" : segundoAnoHistorico.QtdFaltaHoraAnoBasica;
                ViewBag.Ano2QtdFaltaHoraAnoParte = string.IsNullOrEmpty(segundoAnoHistorico.QtdFaltaHoraAnoParte) ? "-" : segundoAnoHistorico.QtdFaltaHoraAnoParte;
                ViewBag.Ano2QtdFaltaHoraAnoProjeto = string.IsNullOrEmpty(segundoAnoHistorico.QtdFaltaHoraAnoProjeto) ? "-" : segundoAnoHistorico.QtdFaltaHoraAnoProjeto;


                ViewBag.Ano2SituacaoAluno = segundoAnoHistorico.SituacaoAluno;
                ViewBag.Ano2Totalchcurricular = segundoAnototalchcurricular;
                ViewBag.Ano2TotalFaltasHoras = segundoAnoTotalFaltasHoras;


                ViewBag.Ano2AnoHistorico = segundoAnoHistorico.AnoHistoricoAluno;
                ViewBag.Ano2DiasLetivos = segundoAnoHistorico.DiasLetivos;
                ViewBag.Ano2EscolaAnoAluno = segundoAnoHistorico.EscolaAnoAluno;
                ViewBag.Ano2CidadeEscolaAnoAluno = segundoAnoHistorico.CidadeEscolaAnoAluno;

            }

            var terceiroAnoHistorico = _banco.HistoricoAlunos.Include("Aluno").FirstOrDefault(x => x.TurmaAnoHistorico.Equals("3Ano") && x.AlunoId == id);

            if (terceiroAnoHistorico == null)
            {
                ViewBag.Ano3TurmaAnoHistorico = "----";

                ViewBag.Ano3AprovPortugues = "-";
                ViewBag.Ano3AprovMatematica = "-";
                ViewBag.Ano3AprovGeografia = "-";
                ViewBag.Ano3AprovHistoria = "-";
                ViewBag.Ano3AprovCiencia = "-";
                ViewBag.Ano3AprovEduFisica = "-";
                ViewBag.Ano3AprovEnsReligioso = "-";
                ViewBag.Ano3AprovArte = "-";
                ViewBag.Ano3AprovInformatica = "-";
                ViewBag.Ano3AprovLitInfantilJuvenil = "-";
                ViewBag.Ano3AprovAtividadeLingMat = "-";
                ViewBag.Ano3AprovAtividadesEspMot = "-";
                ViewBag.Ano3AprovFormacaoPessoal = "-";
                ViewBag.Ano3ChCurricularBasica = "----";
                ViewBag.Ano3ChCurricularArtes = "----";
                ViewBag.Ano3ChCurricularInformatica = "----";
                ViewBag.Ano3ChCurricularLitInfantoJuvenil = "----";
                ViewBag.Ano3ChCurricularProjetoTempoIntegral = "----";

                ViewBag.Ano3QtdFaltaHoraAnoBasica = "----";
                ViewBag.Ano3QtdFaltaHoraAnoParte = "----";
                ViewBag.Ano3QtdFaltaHoraAnoProjeto = "----";

                ViewBag.Ano3SituacaoAluno = "----";
                ViewBag.Ano3Totalchcurricular = "----";
                ViewBag.Ano3TotalFaltasHoras = "----";


                ViewBag.Ano3AnoHistorico = "----";
                ViewBag.Ano3DiasLetivos = "----";
                ViewBag.Ano3EscolaAnoAluno = "--------------------------------------------";
                ViewBag.Ano3CidadeEscolaAnoAluno = "---------------";
            }
            else
            {
                var segundoAnototalchcurricular = (Convert.ToDouble(terceiroAnoHistorico.ChCurricularBasica) +
                                                    Convert.ToDouble(terceiroAnoHistorico.ChCurricularArtes));
                var segundoAnoTotalFaltasHoras = (Convert.ToDouble(terceiroAnoHistorico.QtdFaltaHoraAnoBasica) +
                                                   Convert.ToDouble(terceiroAnoHistorico.QtdFaltaHoraAnoParte) +
                                                   Convert.ToDouble(terceiroAnoHistorico.QtdFaltaHoraAnoProjeto));


                //primeiro Ano
                ViewBag.Ano3TurmaAnoHistorico = terceiroAnoHistorico.TurmaAnoHistorico;

                ViewBag.Ano3AprovPortugues = terceiroAnoHistorico.AprovPortugues;
                ViewBag.Ano3AprovMatematica = terceiroAnoHistorico.AprovMatematica;
                ViewBag.Ano3AprovGeografia = terceiroAnoHistorico.AprovGeografia;
                ViewBag.Ano3AprovHistoria = terceiroAnoHistorico.AprovHistoria;
                ViewBag.Ano3AprovCiencia = terceiroAnoHistorico.AprovCiencia;
                ViewBag.Ano3AprovEduFisica = terceiroAnoHistorico.AprovEduFisica;
                ViewBag.Ano3AprovEnsReligioso = terceiroAnoHistorico.AprovEnsReligioso;
                ViewBag.Ano3AprovArte = string.IsNullOrEmpty(terceiroAnoHistorico.AprovArte) ? "-" : terceiroAnoHistorico.AprovArte;
                ViewBag.Ano3AprovInformatica = string.IsNullOrEmpty(terceiroAnoHistorico.AprovInformatica) ? "-" : terceiroAnoHistorico.AprovInformatica;
                ViewBag.Ano3AprovLitInfantilJuvenil = string.IsNullOrEmpty(terceiroAnoHistorico.AprovLitInfantilJuvenil) ? "-" : terceiroAnoHistorico.AprovLitInfantilJuvenil;
                ViewBag.Ano3AprovAtividadeLingMat = string.IsNullOrEmpty(terceiroAnoHistorico.AprovAtividadeLingMat) ? "-" : terceiroAnoHistorico.AprovAtividadeLingMat;
                ViewBag.Ano3AprovAtividadesEspMot = string.IsNullOrEmpty(terceiroAnoHistorico.AprovAtividadesEspMot) ? "-" : terceiroAnoHistorico.AprovAtividadesEspMot;
                ViewBag.Ano3AprovFormacaoPessoal = string.IsNullOrEmpty(terceiroAnoHistorico.AprovFormacaoPessoal) ? "-" : terceiroAnoHistorico.AprovFormacaoPessoal;
                ViewBag.Ano3ChCurricularBasica = string.IsNullOrEmpty(terceiroAnoHistorico.ChCurricularBasica) ? "-" : terceiroAnoHistorico.ChCurricularBasica;
                ViewBag.Ano3ChCurricularArtes = string.IsNullOrEmpty(terceiroAnoHistorico.ChCurricularArtes) ? "-" : terceiroAnoHistorico.ChCurricularArtes;
                ViewBag.Ano3ChCurricularInformatica = string.IsNullOrEmpty(terceiroAnoHistorico.ChCurricularInformatica) ? "-" : terceiroAnoHistorico.ChCurricularInformatica;
                ViewBag.Ano3ChCurricularLitInfantoJuvenil = string.IsNullOrEmpty(terceiroAnoHistorico.ChCurricularLitInfantoJuvenil) ? "-" : terceiroAnoHistorico.ChCurricularLitInfantoJuvenil;
                ViewBag.Ano3ChCurricularProjetoTempoIntegral = string.IsNullOrEmpty(terceiroAnoHistorico.ChCurricularProjetoTempoIntegral) ? "-" : terceiroAnoHistorico.ChCurricularProjetoTempoIntegral;

                ViewBag.Ano3QtdFaltaHoraAnoBasica = string.IsNullOrEmpty(terceiroAnoHistorico.QtdFaltaHoraAnoBasica) ? "-" : terceiroAnoHistorico.QtdFaltaHoraAnoBasica;
                ViewBag.Ano3QtdFaltaHoraAnoParte = string.IsNullOrEmpty(terceiroAnoHistorico.QtdFaltaHoraAnoParte) ? "-" : terceiroAnoHistorico.QtdFaltaHoraAnoParte;
                ViewBag.Ano3QtdFaltaHoraAnoProjeto = string.IsNullOrEmpty(terceiroAnoHistorico.QtdFaltaHoraAnoProjeto) ? "-" : terceiroAnoHistorico.QtdFaltaHoraAnoProjeto;


                ViewBag.Ano3SituacaoAluno = terceiroAnoHistorico.SituacaoAluno;
                ViewBag.Ano3Totalchcurricular = segundoAnototalchcurricular;
                ViewBag.Ano3TotalFaltasHoras = segundoAnoTotalFaltasHoras;


                ViewBag.Ano3AnoHistorico = terceiroAnoHistorico.AnoHistoricoAluno;
                ViewBag.Ano3DiasLetivos = terceiroAnoHistorico.DiasLetivos;
                ViewBag.Ano3EscolaAnoAluno = terceiroAnoHistorico.EscolaAnoAluno;
                ViewBag.Ano3CidadeEscolaAnoAluno = terceiroAnoHistorico.CidadeEscolaAnoAluno;

            }

            var quartoAnoHistorico = _banco.HistoricoAlunos.Include("Aluno").FirstOrDefault(x => x.TurmaAnoHistorico.Equals("4Ano") && x.AlunoId == id);

            if (quartoAnoHistorico == null)
            {
                ViewBag.Ano4TurmaAnoHistorico = "----";

                ViewBag.Ano4AprovPortugues = "-";
                ViewBag.Ano4AprovMatematica = "-";
                ViewBag.Ano4AprovGeografia = "-";
                ViewBag.Ano4AprovHistoria = "-";
                ViewBag.Ano4AprovCiencia = "-";
                ViewBag.Ano4AprovEduFisica = "-";
                ViewBag.Ano4AprovEnsReligioso = "-";
                ViewBag.Ano4AprovArte = "-";
                ViewBag.Ano4AprovInformatica = "-";
                ViewBag.Ano4AprovLitInfantilJuvenil = "-";
                ViewBag.Ano4AprovAtividadeLingMat = "-";
                ViewBag.Ano4AprovAtividadesEspMot = "-";
                ViewBag.Ano4AprovFormacaoPessoal = "-";
                ViewBag.Ano4ChCurricularBasica = "----";
                ViewBag.Ano4ChCurricularArtes = "----";
                ViewBag.Ano4ChCurricularInformatica = "----";
                ViewBag.Ano4ChCurricularLitInfantoJuvenil = "----";
                ViewBag.Ano4ChCurricularProjetoTempoIntegral = "----";

                ViewBag.Ano4QtdFaltaHoraAnoBasica = "----";
                ViewBag.Ano4QtdFaltaHoraAnoParte = "----";
                ViewBag.Ano4QtdFaltaHoraAnoProjeto = "----";

                ViewBag.Ano4SituacaoAluno = "----";
                ViewBag.Ano4Totalchcurricular = "----";
                ViewBag.Ano4TotalFaltasHoras = "----";


                ViewBag.Ano4AnoHistorico = "----";
                ViewBag.Ano4DiasLetivos = "----";
                ViewBag.Ano4EscolaAnoAluno = "--------------------------------------------";
                ViewBag.Ano4CidadeEscolaAnoAluno = "---------------";
            }
            else
            {
                var segundoAnototalchcurricular = (Convert.ToDouble(quartoAnoHistorico.ChCurricularBasica) +
                                                    Convert.ToDouble(quartoAnoHistorico.ChCurricularArtes));
                var segundoAnoTotalFaltasHoras = (Convert.ToDouble(quartoAnoHistorico.QtdFaltaHoraAnoBasica) +
                                                   Convert.ToDouble(quartoAnoHistorico.QtdFaltaHoraAnoParte) +
                                                   Convert.ToDouble(quartoAnoHistorico.QtdFaltaHoraAnoProjeto));


                //primeiro Ano
                ViewBag.Ano4TurmaAnoHistorico = quartoAnoHistorico.TurmaAnoHistorico;

                ViewBag.Ano4AprovPortugues = quartoAnoHistorico.AprovPortugues;
                ViewBag.Ano4AprovMatematica = quartoAnoHistorico.AprovMatematica;
                ViewBag.Ano4AprovGeografia = quartoAnoHistorico.AprovGeografia;
                ViewBag.Ano4AprovHistoria = quartoAnoHistorico.AprovHistoria;
                ViewBag.Ano4AprovCiencia = quartoAnoHistorico.AprovCiencia;
                ViewBag.Ano4AprovEduFisica = quartoAnoHistorico.AprovEduFisica;
                ViewBag.Ano4AprovEnsReligioso = quartoAnoHistorico.AprovEnsReligioso;
                ViewBag.Ano4AprovArte = string.IsNullOrEmpty(quartoAnoHistorico.AprovArte) ? "-" : quartoAnoHistorico.AprovArte;
                ViewBag.Ano4AprovInformatica = string.IsNullOrEmpty(quartoAnoHistorico.AprovInformatica) ? "-" : quartoAnoHistorico.AprovInformatica;
                ViewBag.Ano4AprovLitInfantilJuvenil = string.IsNullOrEmpty(quartoAnoHistorico.AprovLitInfantilJuvenil) ? "-" : quartoAnoHistorico.AprovLitInfantilJuvenil;
                ViewBag.Ano4AprovAtividadeLingMat = string.IsNullOrEmpty(quartoAnoHistorico.AprovAtividadeLingMat) ? "-" : quartoAnoHistorico.AprovAtividadeLingMat;
                ViewBag.Ano4AprovAtividadesEspMot = string.IsNullOrEmpty(quartoAnoHistorico.AprovAtividadesEspMot) ? "-" : quartoAnoHistorico.AprovAtividadesEspMot;
                ViewBag.Ano4AprovFormacaoPessoal = string.IsNullOrEmpty(quartoAnoHistorico.AprovFormacaoPessoal) ? "-" : quartoAnoHistorico.AprovFormacaoPessoal;
                ViewBag.Ano4ChCurricularBasica = string.IsNullOrEmpty(quartoAnoHistorico.ChCurricularBasica) ? "-" : quartoAnoHistorico.ChCurricularBasica;
                ViewBag.Ano4ChCurricularArtes = string.IsNullOrEmpty(quartoAnoHistorico.ChCurricularArtes) ? "-" : quartoAnoHistorico.ChCurricularArtes;
                ViewBag.Ano4ChCurricularInformatica = string.IsNullOrEmpty(quartoAnoHistorico.ChCurricularInformatica) ? "-" : quartoAnoHistorico.ChCurricularInformatica;
                ViewBag.Ano4ChCurricularLitInfantoJuvenil = string.IsNullOrEmpty(quartoAnoHistorico.ChCurricularLitInfantoJuvenil) ? "-" : quartoAnoHistorico.ChCurricularLitInfantoJuvenil;
                ViewBag.Ano4ChCurricularProjetoTempoIntegral = string.IsNullOrEmpty(quartoAnoHistorico.ChCurricularProjetoTempoIntegral) ? "-" : quartoAnoHistorico.ChCurricularProjetoTempoIntegral;

                ViewBag.Ano4QtdFaltaHoraAnoBasica = string.IsNullOrEmpty(quartoAnoHistorico.QtdFaltaHoraAnoBasica) ? "-" : quartoAnoHistorico.QtdFaltaHoraAnoBasica;
                ViewBag.Ano4QtdFaltaHoraAnoParte = string.IsNullOrEmpty(quartoAnoHistorico.QtdFaltaHoraAnoParte) ? "-" : quartoAnoHistorico.QtdFaltaHoraAnoParte;
                ViewBag.Ano4QtdFaltaHoraAnoProjeto = string.IsNullOrEmpty(quartoAnoHistorico.QtdFaltaHoraAnoProjeto) ? "-" : quartoAnoHistorico.QtdFaltaHoraAnoProjeto;


                ViewBag.Ano4SituacaoAluno = quartoAnoHistorico.SituacaoAluno;
                ViewBag.Ano4Totalchcurricular = segundoAnototalchcurricular;
                ViewBag.Ano4TotalFaltasHoras = segundoAnoTotalFaltasHoras;


                ViewBag.Ano4AnoHistorico = quartoAnoHistorico.AnoHistoricoAluno;
                ViewBag.Ano4DiasLetivos = quartoAnoHistorico.DiasLetivos;
                ViewBag.Ano4EscolaAnoAluno = quartoAnoHistorico.EscolaAnoAluno;
                ViewBag.Ano4CidadeEscolaAnoAluno = quartoAnoHistorico.CidadeEscolaAnoAluno;

            }

            var quintoAnoHistorico = _banco.HistoricoAlunos.Include("Aluno").FirstOrDefault(x => x.TurmaAnoHistorico.Equals("5Ano") && x.AlunoId == id);

            if (quintoAnoHistorico == null)
            {
                ViewBag.Ano5TurmaAnoHistorico = "----";

                ViewBag.Ano5AprovPortugues = "-";
                ViewBag.Ano5AprovMatematica = "-";
                ViewBag.Ano5AprovGeografia = "-";
                ViewBag.Ano5AprovHistoria = "-";
                ViewBag.Ano5AprovCiencia = "-";
                ViewBag.Ano5AprovEduFisica = "-";
                ViewBag.Ano5AprovEnsReligioso = "-";
                ViewBag.Ano5AprovArte = "-";
                ViewBag.Ano5AprovInformatica = "-";
                ViewBag.Ano5AprovLitInfantilJuvenil = "-";
                ViewBag.Ano5AprovAtividadeLingMat = "-";
                ViewBag.Ano5AprovAtividadesEspMot = "-";
                ViewBag.Ano5AprovFormacaoPessoal = "-";
                ViewBag.Ano5ChCurricularBasica = "----";
                ViewBag.Ano5ChCurricularArtes = "----";
                ViewBag.Ano5ChCurricularInformatica = "----";
                ViewBag.Ano5ChCurricularLitInfantoJuvenil = "----";
                ViewBag.Ano5ChCurricularProjetoTempoIntegral = "----";

                ViewBag.Ano5QtdFaltaHoraAnoBasica = "----";
                ViewBag.Ano5QtdFaltaHoraAnoParte = "----";
                ViewBag.Ano5QtdFaltaHoraAnoProjeto = "----";

                ViewBag.Ano5SituacaoAluno = "----";
                ViewBag.Ano5Totalchcurricular = "----";
                ViewBag.Ano5TotalFaltasHoras = "----";


                ViewBag.Ano5AnoHistorico = "----";
                ViewBag.Ano5DiasLetivos = "----";
                ViewBag.Ano5EscolaAnoAluno = "--------------------------------------------";
                ViewBag.Ano5CidadeEscolaAnoAluno = "---------------";
            }
            else
            {
                var segundoAnototalchcurricular = (Convert.ToDouble(quintoAnoHistorico.ChCurricularBasica) +
                                                    Convert.ToDouble(quintoAnoHistorico.ChCurricularArtes));
                var segundoAnoTotalFaltasHoras = (Convert.ToDouble(quintoAnoHistorico.QtdFaltaHoraAnoBasica) +
                                                   Convert.ToDouble(quintoAnoHistorico.QtdFaltaHoraAnoParte) +
                                                   Convert.ToDouble(quintoAnoHistorico.QtdFaltaHoraAnoProjeto));


                //primeiro Ano
                ViewBag.Ano5TurmaAnoHistorico = quintoAnoHistorico.TurmaAnoHistorico;

                ViewBag.Ano5AprovPortugues = quintoAnoHistorico.AprovPortugues;
                ViewBag.Ano5AprovMatematica = quintoAnoHistorico.AprovMatematica;
                ViewBag.Ano5AprovGeografia = quintoAnoHistorico.AprovGeografia;
                ViewBag.Ano5AprovHistoria = quintoAnoHistorico.AprovHistoria;
                ViewBag.Ano5AprovCiencia = quintoAnoHistorico.AprovCiencia;
                ViewBag.Ano5AprovEduFisica = quintoAnoHistorico.AprovEduFisica;
                ViewBag.Ano5AprovEnsReligioso = quintoAnoHistorico.AprovEnsReligioso;
                ViewBag.Ano5AprovArte = string.IsNullOrEmpty(quintoAnoHistorico.AprovArte) ? "-" : quintoAnoHistorico.AprovArte;
                ViewBag.Ano5AprovInformatica = string.IsNullOrEmpty(quintoAnoHistorico.AprovInformatica) ? "-" : quintoAnoHistorico.AprovInformatica;
                ViewBag.Ano5AprovLitInfantilJuvenil = string.IsNullOrEmpty(quintoAnoHistorico.AprovLitInfantilJuvenil) ? "-" : quintoAnoHistorico.AprovLitInfantilJuvenil;
                ViewBag.Ano5AprovAtividadeLingMat = string.IsNullOrEmpty(quintoAnoHistorico.AprovAtividadeLingMat) ? "-" : quintoAnoHistorico.AprovAtividadeLingMat;
                ViewBag.Ano5AprovAtividadesEspMot = string.IsNullOrEmpty(quintoAnoHistorico.AprovAtividadesEspMot) ? "-" : quintoAnoHistorico.AprovAtividadesEspMot;
                ViewBag.Ano5AprovFormacaoPessoal = string.IsNullOrEmpty(quintoAnoHistorico.AprovFormacaoPessoal) ? "-" : quintoAnoHistorico.AprovFormacaoPessoal;
                ViewBag.Ano5ChCurricularBasica = string.IsNullOrEmpty(quintoAnoHistorico.ChCurricularBasica) ? "-" : quintoAnoHistorico.ChCurricularBasica;
                ViewBag.Ano5ChCurricularArtes = string.IsNullOrEmpty(quintoAnoHistorico.ChCurricularArtes) ? "-" : quintoAnoHistorico.ChCurricularArtes;
                ViewBag.Ano5ChCurricularInformatica = string.IsNullOrEmpty(quintoAnoHistorico.ChCurricularInformatica) ? "-" : quintoAnoHistorico.ChCurricularInformatica;
                ViewBag.Ano5ChCurricularLitInfantoJuvenil = string.IsNullOrEmpty(quintoAnoHistorico.ChCurricularLitInfantoJuvenil) ? "-" : quintoAnoHistorico.ChCurricularLitInfantoJuvenil;
                ViewBag.Ano5ChCurricularProjetoTempoIntegral = string.IsNullOrEmpty(quintoAnoHistorico.ChCurricularProjetoTempoIntegral) ? "-" : quintoAnoHistorico.ChCurricularProjetoTempoIntegral;

                ViewBag.Ano5QtdFaltaHoraAnoBasica = string.IsNullOrEmpty(quintoAnoHistorico.QtdFaltaHoraAnoBasica) ? "-" : quintoAnoHistorico.QtdFaltaHoraAnoBasica;
                ViewBag.Ano5QtdFaltaHoraAnoParte = string.IsNullOrEmpty(quintoAnoHistorico.QtdFaltaHoraAnoParte) ? "-" : quintoAnoHistorico.QtdFaltaHoraAnoParte;
                ViewBag.Ano5QtdFaltaHoraAnoProjeto = string.IsNullOrEmpty(quintoAnoHistorico.QtdFaltaHoraAnoProjeto) ? "-" : quintoAnoHistorico.QtdFaltaHoraAnoProjeto;


                ViewBag.Ano5SituacaoAluno = quintoAnoHistorico.SituacaoAluno;
                ViewBag.Ano5Totalchcurricular = segundoAnototalchcurricular;
                ViewBag.Ano5TotalFaltasHoras = segundoAnoTotalFaltasHoras;


                ViewBag.Ano5AnoHistorico = quintoAnoHistorico.AnoHistoricoAluno;
                ViewBag.Ano5DiasLetivos = quintoAnoHistorico.DiasLetivos;
                ViewBag.Ano5EscolaAnoAluno = quintoAnoHistorico.EscolaAnoAluno;
                ViewBag.Ano5CidadeEscolaAnoAluno = quintoAnoHistorico.CidadeEscolaAnoAluno;

            }

            var pdf = new ViewAsPdf
            {
                ViewName = "Historico",
                FileName = "Historico " + aluno.Nome + ".pdf",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageMargins = new Margins { Bottom = 2, Left = 2, Right = 2, Top = 2 }
            };

            return pdf;

            //return View();

        }

        public ActionResult DeclaracaoMatricula(int id)
        {
            var aluno = _banco.Alunos.FirstOrDefault(x => x.Id == id);

            var turma = (from mat in _banco.Matriculas
                         join tur in _banco.Turmas on mat.TurmaId equals tur.Id
                         where mat.TurmaId == tur.Id && mat.AlunoId == id
                         select tur).FirstOrDefault();


            if (aluno != null && turma != null)
            {
                ViewBag.Nome = aluno.Nome;
                ViewBag.Naturalidade = aluno.Naturalidade;
                ViewBag.UFNaturalidade = aluno.UfNaturalidade;
                ViewBag.DataNascimento = aluno.DataNascimento.Date.ToString("dd De  MMMMMMMMMMMMMM, yyyy").ToUpper();
                ViewBag.Pai = aluno.NomePai;
                ViewBag.Mae = aluno.NomeMae;

                ViewBag.Serie = turma.Serie;

                if (ViewBag.Serie == "1ANO" || ViewBag.Serie == "2ANO" || ViewBag.Serie == "3ANO")
                {
                    ViewBag.Ciclo = "Ciclo Inicial de Alfabetização";
                }
                else
                {
                    ViewBag.Ciclo = "Ciclo Complementar de Alfabetização";
                }

            }
            else
            {
                Response.Redirect("~/HistoricoAluno/Index");

            }

            return View("DeclaracaoMatricula");
        }

        public ActionResult FichaIndividual(int id)
        {

            var matricula = _banco.Matriculas.FirstOrDefault(x => x.AlunoId == id);
            //(from mat in _banco.Matriculas where mat.Id == id select mat).ToList();


            if (matricula != null)
            {
                ViewBag.NivelEnsino = matricula.Turma.NivelTurma;
                ViewBag.Turno = matricula.Turma.HorarioFuncionamento;
                ViewBag.Ano = matricula.AnoLetivo.Ano;
                ViewBag.Turma = matricula.Turma.Serie;
                ViewBag.Aluno = matricula.Aluno.Nome;
                ViewBag.Matricula = matricula.Id;
                ViewBag.Nascimento = matricula.Aluno.DataNascimento.Date.ToString("dd/MM/yyyy");
                ViewBag.Sexo = matricula.Aluno.Sexo == "M" ? "Masculino" : "Feminino"; // Se Sexo igual a M....
                ViewBag.EstadoCivil = matricula.Aluno.EstadoCivilAluno;
                ViewBag.Nacionalidade = matricula.Aluno.Nacionalidade;
                ViewBag.Naturalidade = matricula.Aluno.Naturalidade;
                ViewBag.Profissao = matricula.Aluno.ProfissaoAluno;
                ViewBag.Pai = matricula.Aluno.NomePai;
                ViewBag.Mae = matricula.Aluno.NomeMae;
                ViewBag.Logradouro = matricula.Aluno.EnderecoResidencia;
                ViewBag.Numero = matricula.Aluno.NumeroResidencia;
                ViewBag.Complemento = matricula.Aluno.ComplementoResidencia;
                ViewBag.Bairro = matricula.Aluno.BairroResidencia;
                ViewBag.Cep = matricula.Aluno.CepResidencia;
                ViewBag.Municipio = matricula.Aluno.CidadeResidencia;
                ViewBag.Uf = matricula.Aluno.UfResidencia;
                ViewBag.Telefone = matricula.Aluno.TelMae ?? matricula.Aluno.TelPai; // Se telefone da mãe nulo usar o do pai
                ViewBag.CondicaoAluno = "Aprovado";
                ViewBag.AnoLetivoAnual = matricula.AnoLetivo.QtdDiasLetivosAno;

            }

            var notas = (from con in _banco.Conceitos
                         where con.MatriculaId == matricula.Id
                         select con).ToList();




            ViewBag.ChTotal = 833.20;


            var pdf = new ViewAsPdf
            {
                Model = notas,
                ViewName = "FichaIndividual",
                FileName = "Ficha.pdf",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageMargins = new Margins { Bottom = 2, Left = 2, Right = 2, Top = 2 }
            };

            return pdf;


            //return View(notas);
        }

        public ActionResult ModalHistorico(int? turmaId, int? alunoId)
        {
            ViewBag.turmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");


            var alunos = (from al in _banco.Alunos join mat in _banco.Matriculas on al.Id equals mat.AlunoId where mat.TurmaId == turmaId select new { Id = mat.Aluno.Id, Nome = al.Nome }).ToList();

            ViewBag.AlunoId = new SelectList(alunos, "Id", "Nome");

            return View();
        }

        public ActionResult ModalDecMatricula(int? turmaId, int? alunoId)
        {
            ViewBag.turmaId = new SelectList(_banco.Turmas, "Id", "NomeTurma");

            var alunos = (from al in _banco.Alunos join mat in _banco.Matriculas on al.Id equals mat.AlunoId where mat.TurmaId == turmaId select new { Id = mat.Aluno.Id, Nome = al.Nome }).ToList();

            ViewBag.AlunoId = new SelectList(alunos, "Id", "Nome");

            return View();
        }

    }
}