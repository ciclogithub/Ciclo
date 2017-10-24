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
    public class ConfiguracoesController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

            Organizadores organizador = new Organizadores();
            organizador = new OrganizadoresDB().Buscar(Convert.ToInt32(cookie.Value));
            
            return View(organizador);
        }
    }
}