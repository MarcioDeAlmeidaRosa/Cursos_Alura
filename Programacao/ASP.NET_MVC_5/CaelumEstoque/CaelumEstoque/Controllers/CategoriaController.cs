using CaelumEstoque.DAO;
using CaelumEstoque.Filtros;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    [AltorizacaoFilter]
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
            return View(new CategoriaDoProduto());
        }

        [HttpPost]
        public ActionResult Adiciona(CategoriaDoProduto categoria)
        {
            if (ModelState.IsValid)
            {
                CategoriasDAO dao = new CategoriasDAO();
                dao.Adiciona(categoria);
                return RedirectToAction("Index");
            }
            return View("Form", categoria);
        }

        [HttpGet]
        public ActionResult Visualizar(int id)
        {
            return View(new CategoriasDAO().BuscaPorId(id));
        }
    }
}