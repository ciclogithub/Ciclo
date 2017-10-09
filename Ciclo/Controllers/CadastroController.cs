using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Entidades;
using Biblioteca.DB;
using Biblioteca.Funcoes;

namespace Ciclo.Controllers
{
    public class CadastroController : Controller
    {
        // GET: Cadastro
        public ActionResult Index()
        {
            return View();
        }

        [Route("Cadastro/Formulario")]
        public JsonResult Formulario(Empresas empresaview)
        {
            return Json(new Inclusao().Cadastro(empresaview));
        }
    }
}