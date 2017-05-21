using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        [Route("Usuarios", Name = "ListarUsuarios")]
        public ActionResult Index()
        {
            return View(new UsuariosDAO().Lista());
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Adicionar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                new UsuariosDAO().Adiciona(usuario);
                return RedirectToAction("Index");
            }
            return View("Form", usuario);
        }

        [HttpGet]
        [Route("Usuarios/{id}", Name = "VisualizarUsuario")]
        public ActionResult Visualizar(int id)
        {
            return View(new UsuariosDAO().BuscaPorId(id));
        }
    }
}