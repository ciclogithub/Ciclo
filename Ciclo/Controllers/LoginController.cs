using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Entidades;
using Biblioteca.DB;
using Biblioteca.Funcoes;

namespace Ciclo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [Route("Login/Formulario")]
        public JsonResult Formulario(Organizadores organizadoresview)
        {
            return Json(new Inclusao().Cadastro(organizadoresview));
        }

    }
}