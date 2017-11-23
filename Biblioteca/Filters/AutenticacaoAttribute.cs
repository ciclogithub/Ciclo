using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Biblioteca.Filters
{
    public class AutenticacaoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            Logado logado = null;

            //pega o cookies painel
            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
            HttpCookie cookie2 = HttpContext.Current.Request.Cookies["ciclo_perfil"];

            //verifica se o cookie possui valor
            if ((cookie != null) && (cookie2 != null))
                {
                //pega o valor do cookie
                int codigo = Convert.ToInt32(cookie.Value);

                //pega o valor do perfil
                int perfil = Convert.ToInt32(cookie2.Value);
                
                LogadoDB DB = new LogadoDB();
                if (perfil == 1)
                {
                    logado = DB.BuscarOrganizador(codigo);
                }
                else
                {
                    logado = DB.BuscarAluno(codigo);
                }

                //verifica se o usuário existe
                if (logado != null)
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    //se não existe, redireciona para o index.
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "../Login", action = "Index" }));
                }

            }
            else
            {
                //se os cookeis não existem, redireciona para o index
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "../Login", action = "Index" }));
            }

        }
    }

}
