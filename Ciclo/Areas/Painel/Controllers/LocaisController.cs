using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class LocaisController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            return View();
        }
    }
}