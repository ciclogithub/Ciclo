using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Entidades;
using Biblioteca.DB;
using Biblioteca.Filters;
using Ciclo.Models;

namespace Ciclo.Controllers
{
    [Autenticacao]
    public class PainelController : Controller
    {
        // GET: Painel
        [Autenticacao]
        public ActionResult Index()
        {
            ViewBag.contadores = new Biblioteca.DB.ContadoresDB().Listar();
            return View();
        }

        [Autenticacao]
        public ActionResult Usuario()
        {
            return PartialView(new Logado().Buscar());
        }

        [Autenticacao]
        public ActionResult Instrutores()
        {

            List<Instrutores> list = new List<Instrutores>();
            list = new InstrutoresDB().Listar();  

            return View(list);
        }

        [Autenticacao]
        public ActionResult Cursos()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Locais()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Alunos()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Configuracoes()
        {
            return View();
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