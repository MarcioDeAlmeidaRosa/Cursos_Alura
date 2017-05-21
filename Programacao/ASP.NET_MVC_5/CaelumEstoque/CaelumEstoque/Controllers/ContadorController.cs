using CaelumEstoque.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    [AltorizacaoFilter]
    public class ContadorController : Controller
    {
        // GET: Contador
        public ActionResult Index()
        {
            var variavelSession = Session["contador"];
            var contador = Convert.ToInt32(variavelSession);
            contador++;
            Session["contador"] = contador;
            return View(contador);
        }
    }
}