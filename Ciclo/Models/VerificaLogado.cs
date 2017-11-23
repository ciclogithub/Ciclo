using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteca.DB;
using Biblioteca.Entidades;

namespace Ciclo.Models
{
    public class VerificaLogado
    {
        public Organizadores Buscar()
        {
            Organizadores organizador = null;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
            HttpCookie cookie2 = HttpContext.Current.Request.Cookies["ciclo_perfil"];

            //verifica se os dois possuem valores
            if (cookie != null)
            {
                //pega os valores dos cookies
                int id = Convert.ToInt32(cookie.Value);

                //pesquisa no db
                OrganizadoresDB db = new OrganizadoresDB();
                if (Convert.ToInt32(cookie2.Value) == 1)
                {
                    organizador = db.Buscar(id);
                }
                    else
                {
                    //organizador = db.BuscarAluno(id);
                }

                if(organizador == null)
                {
                    return null;
                }
            }
            return organizador;
        }        

    }
}