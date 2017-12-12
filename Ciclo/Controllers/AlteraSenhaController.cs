using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class AlteraSenhaController : Controller
    {
        // GET: AlteraSenha
        public ActionResult Index()
        {
            string code = Request.QueryString["q"];
            {
                return PartialView(new Altera_SenhaDB().Buscar(code));
            }
        }
    }
}