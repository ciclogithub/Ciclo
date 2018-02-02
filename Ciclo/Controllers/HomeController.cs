using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ciclo.Models;
using Biblioteca.Funcoes;
using Biblioteca.Entidades;
using Biblioteca.DB;

namespace Ciclo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Cursos_Site> list = new List<Cursos_Site>();
            list = new Cursos_SiteDB().Listar(10);

            return View(list);
        }

        public ActionResult Menu()
        {
            ViewBag.view = "";
            int perfil = 0;

            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_perfil"];
            if (cookie != null) { perfil = Convert.ToInt32(cookie.Value);  }

            if (perfil == 1)
            {
                ViewBag.view = new VerificaLogado().Buscar();
            }

            if (perfil == 2)
            {
                ViewBag.view = new VerificaLogadoAluno().Buscar();
            }
            return PartialView(ViewBag.view);
        }

        public JsonResult Esqueceu(string email, int perfil)
        {
            return Json(new Inclusao().EsqueceuSenha(email, perfil));
        }
    }

}