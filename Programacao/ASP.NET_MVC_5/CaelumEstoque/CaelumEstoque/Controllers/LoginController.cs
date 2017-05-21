using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autentica(string login, string senha)
        {
            var dao = new UsuariosDAO();
            var usuario = dao.Busca(login, senha);
            if (usuario != null)
            {
                Session["UsuarioLogado"] = usuario;
                return RedirectToAction("Index", "Produto");
            }
            return RedirectToAction("Index");
        }
    }
}