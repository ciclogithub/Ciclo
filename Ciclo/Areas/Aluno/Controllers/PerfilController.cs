using Biblioteca.DB;
using Biblioteca.Entidades;
using Ciclo.Areas.Aluno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Aluno.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Aluno/Perfil
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            return View(new PerfilView(Convert.ToInt32(cookie.Value)));
        }
    }
}