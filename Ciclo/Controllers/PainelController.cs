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
            HttpCookie cookie = Request.Cookies["ciclo_instrutores"];
            ViewBag.contadores = new Biblioteca.DB.ContadoresDB().Listar(Convert.ToInt32(cookie.Value));
            return View();
        }

        [Autenticacao]
        public ActionResult Usuario()
        {
            return PartialView(new Logado().Buscar());
        }

        [Autenticacao]
        public ActionResult Instrutores(string instrutor = "")
        {
            List<Instrutores> list = new List<Instrutores>();
            ViewBag.instrutor = instrutor;

            if (instrutor == "")
                list = new InstrutoresDB().Listar();
            else
                list = new InstrutoresDB().Listar(instrutor);

            return View(list);
        }

        [Autenticacao]
        public JsonResult InstrutorExcluir(string ident)
        {
            var arr = ident.Split(',');
            foreach(var i in arr)
            {
                new InstrutoresDB().Excluir(Convert.ToInt32(i));
            }
            
            return Json(new Retorno());
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