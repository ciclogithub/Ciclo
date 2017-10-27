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
                HttpCookie cookie = Request.Cookies["ciclo_instrutores"];
                if (cookie != null)
                    cookie.Value = Convert.ToString(painel.codigo);   // update cookie value
                else
                {
                    cookie = new HttpCookie("ciclo_instrutores");
                    cookie.Value = Convert.ToString(painel.codigo);
                }
                Response.Cookies.Add(cookie);

                retorno = "OK";

                retorno = "{\"retorno\": \"" + retorno + "\"}";

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}