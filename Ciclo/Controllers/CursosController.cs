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
        public ActionResult Index()
        {
            List<Cursos_Site> list = new List<Cursos_Site>();
            list = new Cursos_SiteDB().ListarPesquisa("", "", "", 1, 12);

            return View(list);
        }

    }
}