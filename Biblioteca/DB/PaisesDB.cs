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
    public class PaisesDB
    {
        public List<Paises> Listar()
        {
            try
            {
                List<Paises> list_paises = new List<Paises>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Paises ORDER by txpais");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_paises.Add(new Paises(Convert.ToInt32(reader["idpais"]), Convert.ToString(reader["txpais"])));
                }
                reader.Close();
                session.Close();

                return list_paises;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public int Buscar(int id)
        {
            try
            {
                int ident = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idpais from Estados where idestado = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    ident = Convert.ToInt32(reader["idpais"]);
                }
                reader.Close();
                session.Close();

                return ident;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public string BuscarPais(int id = 0)
        {
            try
            {
                string pais = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT txpais from paises where idpais = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    pais = Convert.ToString(reader["txpais"]);
                }
                reader.Close();
                session.Close();

                return pais;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public int Buscar(string pais)
        {
            try
            {
                int ident = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idpais from Paises where txpais = @pais");
                query.SetParameter("pais", pais);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    ident = Convert.ToInt32(reader["idpais"]);
                }
                reader.Close();
                session.Close();

                return ident;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
