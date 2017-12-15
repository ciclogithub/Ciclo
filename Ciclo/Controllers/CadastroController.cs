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
        public ActionResult Index(int curso = 0)
        {
            ViewBag.curso = curso;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Cadastro/Formulario")]
        public JsonResult Formulario(Organizadores organizadoresview)
        {
            return Json(new Inclusao().Cadastro(organizadoresview));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Cadastro/FormularioAluno")]
        public JsonResult FormularioAluno(Usuarios usuariosview)
        {
            return Json(new Inclusao().CadastroAluno(usuariosview));
        }
    }
}