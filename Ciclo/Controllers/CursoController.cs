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
        [Route("Curso/{curso}")]
        public ActionResult Index(string curso = "")
        {
            string code = "";
            if (curso.IndexOf("-") > 0)
            {
                code = curso.Substring(0, curso.IndexOf("-"));
            }
            else
            {
                code = curso;
            }

            return View(new CursoView(Convert.ToInt32(code)));
        }

    }
}