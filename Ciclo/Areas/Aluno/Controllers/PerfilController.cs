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
    public class PerfilController : Controller
    {
        // GET: Aluno/Perfil
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            return View(new PerfilView(Convert.ToInt32(cookie.Value)));
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(string nome = "", string telefone = "", int cidade = 0, string empresa = "", string redes = "", string mercados = "")
        {
            UsuariosDB db = new UsuariosDB();
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            int ident = Convert.ToInt32(cookie.Value);
            Usuarios usuario = db.Buscar(ident);
            usuario.txusuario = nome;
            usuario.idcidade = cidade;
            usuario.txempresa = empresa;

            db.Alterar(usuario);

            db.RemoverTelefones(ident);
            db.RemoverRedesSociais(ident);
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

            var arrT = telefone.Split(',');
            foreach (var i in arrT)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new UsuariosDB().SalvarTelefone(ident, arrTemp[1], Convert.ToInt32(arrTemp[0]));
                }

            }

            var arrR = redes.Split(',');
            foreach (var i in arrR)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new UsuariosDB().SalvarRedeSocial(ident, arrTemp[1], Convert.ToInt32(arrTemp[0]));
                }

            }

            return Json(new Retorno());
        }
    }
}