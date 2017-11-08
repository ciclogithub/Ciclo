using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ciclo.Areas.Painel.Models;

namespace Ciclo.Areas.Painel.Controllers
{
    public class RelatoriosController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            return View(new RelatoriosView());
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult Relatorio(FormCollection collection)
        {
            return View(new RelatoriosDB().Listar());
        }

    }
}