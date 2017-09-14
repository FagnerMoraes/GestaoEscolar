using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestaoEscolar.Models;
using System.Data.Entity;

namespace GestaoEscolar.Controllers
{
    public class ConceitoFormacaoController : Controller
    {

        readonly Contexto _banco = new Contexto();

        public ActionResult Editar(int? id, int MatriculaId, int periodo)
        {
            var conceitoformacao = _banco.ConceitoFormacaos.FirstOrDefault
                (x => x.MatriculaId == MatriculaId && x.Periodo == periodo);


            if (conceitoformacao != null)
            {
                return View(conceitoformacao);
            }
            else
            {
                ConceitoFormacao novoconceito = new ConceitoFormacao
                { MatriculaId = MatriculaId, Periodo = periodo };

                _banco.ConceitoFormacaos.Add(novoconceito);
                _banco.SaveChanges();

                conceitoformacao = _banco.ConceitoFormacaos.FirstOrDefault
                (x => x.MatriculaId == MatriculaId && x.Periodo == periodo);

                id = conceitoformacao.Id;

                return RedirectToAction("Editar", new { id, MatriculaId, periodo });
            }
        }

        [HttpPost]
        public ActionResult Editar(ConceitoFormacao conceitoformacao)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(conceitoformacao).State = EntityState.Modified;
                _banco.SaveChanges();

                var Aluno = (from mat in _banco.Matriculas
                               join al in _banco.Alunos on mat.AlunoId equals al.Id
                               where mat.Id == conceitoformacao.MatriculaId 
                               select al).FirstOrDefault();

                return RedirectToAction("NotaAluno", "Conceito", new { alunoId = Aluno.Id, periodo = conceitoformacao.Periodo });
                //return RedirectToAction("ListaAluno", "Conceito");
            }
            return View(conceitoformacao);
        }

    }
}
