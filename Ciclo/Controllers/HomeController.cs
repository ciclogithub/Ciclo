using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ciclo.Models;
using Biblioteca.Funcoes;

namespace Ciclo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            ViewBag.view = "";

            //HttpCookie cookie = HttpContext.Request.Cookies["ciclo_perfil"];
            //if (Convert.ToInt32(cookie.Value) == 1)
            //{
                ViewBag.view = new VerificaLogado().Buscar();
            //}

            //if (Convert.ToInt32(cookie.Value) == 2)
            //{
            //    ViewBag.view = new VerificaLogadoAluno().Buscar();
            //}
            return PartialView(ViewBag.view);
        }

        public JsonResult Esqueceu(string email)
        {
            return Json(new Inclusao().EsqueceuSenha(email));
        }
    }

}