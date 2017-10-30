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
            //pega o cookies painel
            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

            //verifica se o cookie possui valor
            if (cookie != null)
            {
                //pega o valor do cookie
                int codigo = Convert.ToInt32(cookie.Value);

                //abre o painel e pesquisa no db
                PainelDB DB = new PainelDB();
                Painel user = DB.Buscar(codigo);

                //verifica se o usuário existe
                if (user != null)
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
