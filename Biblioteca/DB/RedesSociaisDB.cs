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
    public class RedesSociaisDB
    {
        public List<RedesSociais> Listar()
        {
            try
            {
                List<RedesSociais> list_redes = new List<RedesSociais>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from RedesSociais ORDER by txredesocial");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_redes.Add(new RedesSociais(Convert.ToInt32(reader["idredesocial"]), Convert.ToString(reader["txredesocial"]), Convert.ToString(reader["txicone"])));
                }
                reader.Close();
                session.Close();

                return list_redes;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public string Buscar(int id)
        {
            try
            {
                string rede = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT txredesocial from RedesSociais where idredesocial = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    rede = Convert.ToString(reader["txredesocial"]);
                }
                reader.Close();
                session.Close();

                return rede;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
