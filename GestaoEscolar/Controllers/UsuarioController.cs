using System.Web.Mvc;
using GestaoEscolar.Models;
using System.Web.Security;
using System;
using GestaoEscolar.DAO;
using GestaoEscolar.Infra;

namespace GestaoEscolar.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioDAO dao;
        private UsuarioInfra infra;

        readonly Contexto _banco = new Contexto();

        public UsuarioController(UsuarioDAO dao, UsuarioInfra infra)
        {
            this.dao = dao;
            this.infra = infra;
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (infra.ValidarUsuario(usuario.Login, usuario.Senha))
                    {
                        FormsAuthentication.SetAuthCookie(usuario.Login, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception error)
                {
                    ViewBag.Mensagem = error;
                }
                ModelState.AddModelError("", "Dados do Login estão incorretos");
            }
            return View(usuario);
        }

        [Authorize]
        public ActionResult Adicionar()
        {
            return View();
        }

        public ActionResult Detalhes(int id)
        {
            ViewBag.Detalhe = true;

            ViewBag.EscolaId = new SelectList(_banco.Escolas, "Id", "NomeEscola");

            var usuario = dao.UsuarioPorId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Adicionar(Usuario novousuario)
        {
            if (ModelState.IsValid)
            {
                dao.Salva(novousuario);
                return RedirectToAction("Index", "Home");
            }
            return View(novousuario);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
