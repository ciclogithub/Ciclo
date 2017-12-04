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
        public List<Estados> Listar(int pais = 0)
        {
            try
            {
                List<Estados> list_estados = new List<Estados>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Estados WHERE idpais = @pais ORDER by txestado");
                query.SetParameter("pais", pais);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_estados.Add(new Estados(Convert.ToInt32(reader["idestado"]), Convert.ToString(reader["txestado"]), Convert.ToString(reader["txsigla"])));
                }
                reader.Close();
                session.Close();

                return list_estados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

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

        public int Buscar(int id)
        {
            try
            {
                int ident = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idestado from Cidades where idcidade = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    ident = Convert.ToInt32(reader["idestado"]);
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

        public int Buscar(string uf)
        {
            try
            {
                int ident = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idestado from Estados where txsigla = @uf");
                query.SetParameter("uf", uf);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    ident = Convert.ToInt32(reader["idestado"]);
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
