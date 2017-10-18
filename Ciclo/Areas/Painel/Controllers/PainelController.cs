using Biblioteca.Filters;
using Ciclo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class PainelController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["ciclo_instrutores"];
            ViewBag.contadores = new Biblioteca.DB.ContadoresDB().Listar(Convert.ToInt32(cookie.Value));
            return View();
        }

        [Autenticacao]
        public ActionResult Usuario()
        {
            return PartialView(new Logado().Buscar());
        }

        public ActionResult Sair()
        {
            HttpCookie cookie = Request.Cookies["ciclo_instrutores"];
            cookie.Value = "";
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Login");
        }
    }
}