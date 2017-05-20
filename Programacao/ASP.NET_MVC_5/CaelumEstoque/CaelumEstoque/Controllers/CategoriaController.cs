using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        [HttpGet]
        public ActionResult Index()
        {
            return View(new CategoriasDAO().Lista());
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(CategoriaDoProduto categoria)
        {
            CategoriasDAO dao = new CategoriasDAO();
            dao.Adiciona(categoria);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Visualizar(int id)
        {
            return View(new CategoriasDAO().BuscaPorId(id));
        }
    }
}