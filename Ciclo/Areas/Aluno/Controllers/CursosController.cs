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
            return View();
        }
    }
}