using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class InscricoesController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string filtro = "", int status = 0, int pagina = 1)
        {
            List<Inscricoes> list = new List<Inscricoes>();
            ViewBag.filtro = filtro;
            ViewBag.status = status;

            if (filtro == "")
                list = new InscricoesDB().Listar(status, pagina, 10);
            else
                list = new InscricoesDB().Listar(status, filtro, pagina, 10);

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