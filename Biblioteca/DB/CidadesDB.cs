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
    public class CidadesDB
    {
        public List<Cidades> Listar(int estado = 0)
        {
            try
            {
                List<Cidades> list_cidades = new List<Cidades>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Cidades WHERE idestado = @estado ORDER by txcidade");
                query.SetParameter("estado", estado);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cidades.Add(new Cidades(Convert.ToInt32(reader["idcidade"]), Convert.ToInt32(reader["idestado"]), Convert.ToString(reader["txcidade"])));
                }
                reader.Close();
                session.Close();

                return list_cidades;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public int Buscar(int estado, string cidade)
        {
            try
            {
                int ident = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idcidade from Cidades where idestado = @estado and txcidade = @cidade");
                query.SetParameter("estado", estado);
                query.SetParameter("cidade", cidade);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    ident = Convert.ToInt32(reader["idcidade"]);
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
