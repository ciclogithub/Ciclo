using Ciclo.Areas.Ciclo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Ciclo.Controllers
{
    public class CicloController : Controller
    {
        public ActionResult Index(int tprelatorio = 0, string dtini = "", string dtfim = "")
        {
            ViewBag.tipo = tprelatorio;
            ViewBag.dtini = dtini;
            ViewBag.dtfim = dtfim;
            return View(new CicloView(dtini, dtfim));
        }
    }
}