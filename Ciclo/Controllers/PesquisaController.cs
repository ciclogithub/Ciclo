using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class PesquisaController : Controller
    {
        // GET: Pesquisa
        public ActionResult Index(string filtro_curso = "", string filtro_cidade = "", string filtro_data = "", int pagina = 1)
        {
            List<Cursos_Site> list = new List<Cursos_Site>();
            ViewBag.curso = filtro_curso;
            ViewBag.cidade = filtro_cidade;
            ViewBag.data = filtro_data;

            list = new Cursos_SiteDB().ListarPesquisa(filtro_curso, filtro_cidade, filtro_data, pagina, 12);

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

        public JsonResult Cidades(string term)
        {
            return Json(new CidadesDB().ListarPesquisa(term));
        }
    }
}