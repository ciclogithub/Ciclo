using Biblioteca.DB;
using Biblioteca.Entidades;
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
            HttpCookie cookie = Request.Cookies["ciclo_usuario"];
            ViewBag.contadores = new Biblioteca.DB.ContadoresDB().Listar(Convert.ToInt32(cookie.Value));
            ViewBag.cursos = new Biblioteca.DB.Cursos_ListaDB().Listar(Convert.ToInt32(cookie.Value));
            return View();
        }

        [Autenticacao]
        public ActionResult Usuario()
        {
            return PartialView(new VerificaLogado().Buscar());
        }

        [Autenticacao]
        public ActionResult Menu()
        {
            return PartialView();
        }

        public ActionResult Sair()
        {
            HttpCookie cookie = Request.Cookies["ciclo_usuario"];
            cookie.Value = "";
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);

            HttpCookie cookie2 = Request.Cookies["ciclo_perfil"];
            cookie2.Value = "";
            cookie2.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "../");
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ModelosEmail(int tipo = 0)
        {
            HttpCookie cookie = Request.Cookies["ciclo_usuario"];

            List<Modelos_Email> modelos = new Modelos_EmailDB().Listar(Convert.ToInt32(cookie.Value), tipo);
            return Json(modelos);
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ModelosEmailCarregar(int id = 0)
        {
            Modelos_Email modelos = new Modelos_EmailDB().Buscar(id);
            return Json(modelos);
        }
    }
}