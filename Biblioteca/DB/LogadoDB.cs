using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Biblioteca.DB
{
    public class LogadoDB
    {
        public Logado BuscarOrganizador(int id)
        {
            try
            {
                Logado logado = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Organizadores WHERE idorganizador = @codigo");
                quey.SetParameter("codigo", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    logado = new Logado(Convert.ToInt32(reader["idorganizador"]), Convert.ToString(reader["txorganizador"]),1);
                }
                reader.Close();
                session.Close();

                return logado;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Logado BuscarAluno(int id)
        {
            try
            {
                Logado logado = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Usuarios WHERE idusuario = @codigo");
                quey.SetParameter("codigo", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    logado = new Logado(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["txusuario"]),2);
                }
                reader.Close();
                session.Close();

                return logado;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
