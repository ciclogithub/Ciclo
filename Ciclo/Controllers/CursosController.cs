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
        public ActionResult Index(int pagina = 1)
        {
            List<Cursos_Site> list = new List<Cursos_Site>();
            list = new Cursos_SiteDB().ListarPesquisa("", "", "", pagina, 12);

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

    }
}