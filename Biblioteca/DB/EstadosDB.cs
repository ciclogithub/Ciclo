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
    public class EstadosDB
    {
        public List<Estados> Listar()
        {
            try
            {
                List<Estados> list_estado = new List<Estados>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Estados ORDER by txestado");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_estado.Add(new Estados(Convert.ToInt32(reader["idestado"]), Convert.ToString(reader["txestado"]), Convert.ToString(reader["txsigla"])));
                }
                reader.Close();
                session.Close();

                return list_estado;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        
    }
}
