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

            //verifica se os dois possuem valores
            if (cookie != null)
            {
                //pega os valores dos cookies
                int id = Convert.ToInt32(cookie.Value);

                //pesquisa no db
                OrganizadoresDB db = new OrganizadoresDB();
                organizador = db.Buscar(id);

                if(organizador == null)
                {
                    return null;
                }
            }
            return organizador;
        }        

    }

    public class VerificaLogadoAluno
    {
        public Usuarios Buscar()
        {
            Usuarios usuario = null;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

            //verifica se os dois possuem valores
            if (cookie != null)
            {
                //pega os valores dos cookies
                int id = Convert.ToInt32(cookie.Value);

                //pesquisa no db
                UsuariosDB db = new UsuariosDB();
                    usuario = db.Buscar(id);

                if (usuario == null)
                {
                    return null;
                }
            }
            return usuario;
        }

    }
}