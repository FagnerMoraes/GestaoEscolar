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

        public ActionResult Editar(int? id, int alunoId, int periodo)
        {
            var conceitoformacao = _banco.ConceitoFormacaos.FirstOrDefault
                (x => x.MatriculaId == alunoId && x.Periodo == periodo);


            if (conceitoformacao != null)
            {
                return View(conceitoformacao);
            }
            else
            {
                ConceitoFormacao novoconceito = new ConceitoFormacao
                { MatriculaId = alunoId, Periodo = periodo };

                _banco.ConceitoFormacaos.Add(novoconceito);
                _banco.SaveChanges();

                conceitoformacao = _banco.ConceitoFormacaos.FirstOrDefault
                (x => x.MatriculaId == alunoId && x.Periodo == periodo);

                id = conceitoformacao.Id;

                return RedirectToAction("Editar", new { id, alunoId, periodo });
            }
        }

        [HttpPost]
        public ActionResult Editar(ConceitoFormacao conceitoformacao)
        {
            if (ModelState.IsValid)
            {
                _banco.Entry(conceitoformacao).State = EntityState.Modified;
                _banco.SaveChanges();
                return RedirectToAction("ListaAluno", "Conceito");
            }
            return View(conceitoformacao);
        }

    }
}
