using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Aluno.Controllers
{
    public class AlterarSenhaController : Controller
    {
        // GET: Aluno/AlterarSenha
        public ActionResult Index()
        {
            return View();
        }

        [Autenticacao]
        public JsonResult AlterarSenha(string senha = "", string novasenha = "")
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            string retorno = "";
            int id = Convert.ToInt32(cookie.Value);

            UsuariosDB db = new UsuariosDB();
            Usuarios usuarios = db.Buscar(id);
            usuarios.txsenhaaluno = MD5Hash.CalculaHash(senha);
            usuarios.txnovasenha = MD5Hash.CalculaHash(novasenha);

            bool painel = new PainelDB().BuscarUsuario(id, usuarios.txsenhaaluno);

            if (!painel)
            {
                retorno = "Dados incorretos";
                retorno = "{\"retorno\": \"" + retorno + "\"}";
            }
            else
            {
                db.AlteraSenha(usuarios);
                retorno = "OK";
                retorno = "{\"retorno\": \"" + retorno + "\"}";
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}