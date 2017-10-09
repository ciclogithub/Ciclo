using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class PainelController : Controller
    {
        // GET: Painel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Instrutores()
        {
            return View();
        }

        public ActionResult Turmas()
        {
            return View();
        }

        public ActionResult Alunos()
        {
            return View();
        }

        public ActionResult Configuracoes()
        {
            return View();
        }
    }
}