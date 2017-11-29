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
    public class CoresDB
    {
        public List<Cores> Listar()
        {
            try
            {
                List<Cores> list_cores = new List<Cores>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Cores ORDER by txcor");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cores.Add(new Cores(Convert.ToInt32(reader["idcor"]), Convert.ToString(reader["txcor"])));
                }
                reader.Close();
                session.Close();

                return list_cores;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public string ListarNomes(string id)
        {
            try
            {
                string result = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select SUBSTRING((select ', ' + txcor from cores where idcor in (" + id + ") FOR XML PATH('')),3,9999) as result");
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    result = Convert.ToString(reader["result"]);
                }
                reader.Close();
                session.Close();

                return result;
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
                string cor = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT txcor from Cores where idcor = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    cor = Convert.ToString(reader["txcor"]);
                }
                reader.Close();
                session.Close();

                return cor;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
