using Biblioteca.DB;
using Biblioteca.Entidades;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class CursosController : Controller
    {
        // GET: Curso
        [Route("Cursos/Todos")]
        public ActionResult Index()
        {
            List<Cursos_Site> list = new List<Cursos_Site>();
            list = new Cursos_SiteDB().ListarPesquisa("", "", "", 1, 12);

            if (list.Count > 0)
            {
                ViewBag.total = 1; /* list.First().total;*/
                ViewBag.pagina = 1;
            }
            else
            {
                ViewBag.total = 0;
                ViewBag.pagina = 0;
            }

            return View(list);
        }

    }
}