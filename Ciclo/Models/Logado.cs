using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteca.DB;
using Biblioteca.Entidades;

namespace Ciclo.Models
{
    public class Logado
    {
        public Organizadores Buscar()
        {
            Organizadores organizadores = null;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

            //verifica se os dois possuem valores
            if (cookie != null)
            {
                //pega os valores dos cookies
                int id = Convert.ToInt32(cookie.Value);

                //pesquisa no db
                OrganizadoresDB db = new OrganizadoresDB();
                organizadores = db.Buscar(id);

                if(organizadores == null)
                {
                    return null;
                }
            }
            return organizadores;
        }

    }
}