using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class RelatoriosController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_instrutores"];

            Organizadores organizador = new Organizadores();
            organizador = new OrganizadoresDB().Buscar(Convert.ToInt32(cookie.Value));

            ViewBag.temas = new TemasDB().Listar();
            ViewBag.categorias = new CategoriasDB().Listar();
            ViewBag.locais = new LocaisDB().Listar();
            ViewBag.instrutores = new InstrutoresDB().Listar();
            //ViewBag.alunos = new AlunosDB().Listar();
            ViewBag.especialidades = new EspecialidadesDB().Listar();
            //ViewBag.cursos = new CursosDB().Listar();
            ViewBag.cores = new CoresDB().Listar();

            return View(organizador);
        }
       
    }
}