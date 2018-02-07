
using Ciclo.Areas.Aluno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Aluno.Controllers
{
    public class CursosController : Controller
    {
        // GET: Aluno/Cursos
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            return View(new CursosView(Convert.ToInt32(cookie.Value)));
        }
    }
}