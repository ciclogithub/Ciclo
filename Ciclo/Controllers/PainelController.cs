using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Filters;

namespace Ciclo.Controllers
{
    [Autenticacao]
    public class PainelController : Controller
    {
        // GET: Painel
        [Autenticacao]
        public ActionResult Index()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Instrutores()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Cursos()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Locais()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Alunos()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Configuracoes()
        {
            return View();
        }
    }
}