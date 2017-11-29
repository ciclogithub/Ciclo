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
    public class CategoriasDB
    {
        public List<Categorias> Listar()
        {
            try
            {
                List<Categorias> list_categorias = new List<Categorias>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Categorias Order by txcategoria");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_categorias.Add(new Categorias(Convert.ToInt32(reader["idcategoria"]), Convert.ToString(reader["txcategoria"])));
                }
                reader.Close();
                session.Close();

                return list_categorias;
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
                Query query = session.CreateQuery("select SUBSTRING((select ', ' + txcategoria from categorias where idcategoria in (" + id + ") FOR XML PATH('')),3,9999) as result");
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

        public Categorias Buscar(int id)
        {
            try
            {
                Categorias Categorias = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Categorias WHERE idcategoria = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    Categorias = new Categorias(Convert.ToInt32(reader["idcategoria"]), Convert.ToString(reader["txcategoria"]));
                }
                reader.Close();
                session.Close();

                return Categorias;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
