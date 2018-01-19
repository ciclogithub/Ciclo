using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Ciclo.Areas.Aluno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Aluno.Controllers
{
    public class MercadoController : Controller
    {
        // GET: Aluno/Perfil
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            return View(new PerfilView(Convert.ToInt32(cookie.Value)));
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(string especialidades = "", string mercados = "")
        {
            UsuariosDB db = new UsuariosDB();
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            int ident = Convert.ToInt32(cookie.Value);

            db.RemoverEspecialidades(ident);
            db.RemoverMercados(ident);

            var arrM = mercados.Split(',');
            foreach (var i in arrM)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new UsuariosDB().SalvarMercado(ident, Convert.ToInt32(arrTemp[0]));
                }
            }

            var arrE = especialidades.Split(',');
            foreach (var i in arrE)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new UsuariosDB().SalvarEspecialidade(ident, Convert.ToInt32(arrTemp[0]));
                }

            }

            return Json(new Retorno());
        }
    }
}