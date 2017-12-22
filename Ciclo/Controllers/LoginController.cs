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
        public ActionResult Index(int curso = 0)
        {
            ViewBag.curso = curso;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Login/Formulario")]
        public JsonResult Formulario(string txemailorganizador, string txsenhaorganizador)
        {
            string retorno = "";

            PainelDB db = new PainelDB();
            txsenhaorganizador = MD5Hash.CalculaHash(txsenhaorganizador);
            Painel painel = db.Buscar(txemailorganizador, txsenhaorganizador);

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
        public JsonResult FormularioAluno(string txemailaluno, string txsenha)
        {
            string retorno = "";

            UsuariosDB db = new UsuariosDB();
            txsenha = MD5Hash.CalculaHash(txsenha);
            Usuarios usuario = db.Buscar(txemailaluno, txsenha);

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
                    cookie.Value = Convert.ToString(usuario.idusuario);
                    cookie2.Value = "2";
                }
                else
                {
                    cookie = new HttpCookie("ciclo_usuario");
                    cookie.Value = Convert.ToString(usuario.idusuario);
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