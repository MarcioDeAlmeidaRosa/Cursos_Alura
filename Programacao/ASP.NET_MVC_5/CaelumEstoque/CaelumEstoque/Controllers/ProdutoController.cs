﻿using CaelumEstoque.DAO;
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
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            ViewBag.Produtos = dao.Lista();
            return View();
        }

        public ActionResult Form()
        {
            ViewBag.Categorias = new CategoriasDAO().Lista();
            return View();
        }

        [HttpPost]//-->Permte somente requisição Post
        //public ActionResult Adiciona(string nome, float preco, int quantidade, string descricao, int categoriaId)
        public ActionResult Adiciona(Produto produto)//<- Model Binder faz a criação do produto no cliente antes de enviar para o servidor
        {
            //Produto produto = new Produto();
            //produto.Nome = nome;
            //produto.Preco = preco;
            //produto.Quantidade = quantidade;
            //produto.Descricao = descricao;
            //produto.CategoriaId = categoriaId;
            ProdutosDAO dao = new ProdutosDAO();
            dao.Adiciona(produto);
            //return View();
            return RedirectToAction("Index", "Produto");
        }
            
    }
}