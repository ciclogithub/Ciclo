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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Login/Formulario")]
        public JsonResult Formulario(Organizadores organizadoresview)
        {
            string retorno = "";

            PainelDB db = new PainelDB();
            organizadoresview.txsenha = MD5Hash.CalculaHash(organizadoresview.txsenha);
            Painel painel = db.Buscar(organizadoresview.txemail, organizadoresview.txsenha);

            if (painel == null)
            {
                retorno = "Dados incorretos";

                retorno = "{\"retorno\": \"" + retorno + "\"}";
            }
            else
            {

                //salva o cookies com o codigo do acesso_cookies
                HttpCookie cookie = Request.Cookies["ciclo_usuario"];
                HttpCookie cookie2 = Request.Cookies["ciclo_perfil"];
                if (cookie != null)
                {
                    cookie.Value = Convert.ToString(painel.codigo);
                    cookie2.Value = "1";
                }
                else
                {
                    cookie = new HttpCookie("ciclo_usuario");
                    cookie.Value = Convert.ToString(painel.codigo);
                    cookie2 = new HttpCookie("ciclo_perfil");
                    cookie2.Value = "1";
                }
                Response.Cookies.Add(cookie);
                Response.Cookies.Add(cookie2);

                retorno = "OK";

                retorno = "{\"retorno\": \"" + retorno + "\"}";

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Login/FormularioAluno")]
        public JsonResult FormularioAluno(Usuarios usuariosview)
        {
            string retorno = "";

            UsuariosDB db = new UsuariosDB();
            usuariosview.txsenha = MD5Hash.CalculaHash(usuariosview.txsenha);
            Usuarios usuario = db.Buscar(usuariosview.txemail, usuariosview.txsenha);

            if (usuario == null)
            {
                retorno = "Dados incorretos";

                retorno = "{\"retorno\": \"" + retorno + "\"}";
            }
            else
            {

                //salva o cookies com o codigo do acesso_cookies
                HttpCookie cookie = Request.Cookies["ciclo_usuario"];
                HttpCookie cookie2 = Request.Cookies["ciclo_perfil"];
                if (cookie != null)
                {
                    cookie.Value = Convert.ToString(usuario.idaluno);
                    cookie2.Value = "2";
                }
                else
                {
                    cookie = new HttpCookie("ciclo_usuario");
                    cookie.Value = Convert.ToString(usuario.idaluno);
                    cookie2 = new HttpCookie("ciclo_perfil");
                    cookie2.Value = "2";
                }
                Response.Cookies.Add(cookie);
                Response.Cookies.Add(cookie2);

                retorno = "OK";

                retorno = "{\"retorno\": \"" + retorno + "\"}";

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}