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
            return PartialView(new VerificaLogado().Buscar());
        }

        public JsonResult Esqueceu(string email)
        {
            return Json(new Inclusao().EsqueceuSenha(email));
        }
    }

}