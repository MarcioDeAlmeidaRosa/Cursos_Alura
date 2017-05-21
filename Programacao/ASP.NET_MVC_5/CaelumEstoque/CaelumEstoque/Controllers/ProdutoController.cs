using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        [HttpGet]
        [Route("produtos", Name ="ListaProdutos")]
        public ActionResult Index()
        {
            return View(new ProdutosDAO().Lista());
        }

        [HttpGet]
        public ActionResult Form()
        {
            ViewBag.Categorias = new CategoriasDAO().Lista();
            return View(new Produto());
        }

        [HttpPost]//-->Permte somente requisição Post
        //public ActionResult Adiciona(string nome, float preco, int quantidade, string descricao, int categoriaId)
        public ActionResult Adiciona(Produto produto)//<- Model Binder faz a criação do produto no cliente antes de enviar para o servidor
        {
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index", "Produto");
            }
            ViewBag.Categorias = new CategoriasDAO().Lista();
            return View("Form", produto);
        }

        [HttpGet]
        [Route("produtos/{id}", Name = "VisualizaProduto")]
        public ActionResult Visualiza(int id)
        {
            return View(new ProdutosDAO().BuscaPorId(id));
        }
        
        [HttpPost]
        public ActionResult DecrementarQuantidade(int id)
        {
            var dao = new ProdutosDAO();
            var produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            return Json(produto);
            //return RedirectToAction("Index");
        }
    }
}