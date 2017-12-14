using Biblioteca.DB;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class CursoController : Controller
    {
        // GET: Curso
        public ActionResult Index()
        {
            string code = Request.QueryString["c"];
            return View(new CursoView(Convert.ToInt32(code)));
        }

    }
}