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
        public ActionResult Index(int pagina = 1)
        {
            List<Cursos_Site> list = new List<Cursos_Site>();
            list = new Cursos_SiteDB().Listar(pagina, 12);

            if (list.Count > 0)
            {
                ViewBag.total = list.First().total;
                ViewBag.pagina = pagina;
            }
            else
            {
                ViewBag.total = 0;
                ViewBag.pagina = 0;
            }

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

        public JsonResult Esqueceu(string email)
        {
            return Json(new Inclusao().EsqueceuSenha(email));
        }
    }

}