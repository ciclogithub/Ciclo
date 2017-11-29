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
    public class MercadosDB
    {
        public void Salvar(Mercados variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Mercados (txmercado) VALUES (@mercado)");
                query.SetParameter("mercado", variavel.txmercado);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Mercados variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Mercados SET txmercado = @mercado WHERE idmercado = @id");
                query.SetParameter("id", variavel.idmercado);
                query.SetParameter("mercado", variavel.txmercado);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Mercados WHERE idmercado = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public string ListarNomes(string id)
        {
            try
            {
                string result = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select SUBSTRING((select ', ' + txmercado from mercados where idmercado in (" + id + ") FOR XML PATH('')),3,9999) as result");
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

        public List<Mercados> Listar()
        {
            try
            {
                List<Mercados> list_mercados = new List<Mercados>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select  * from Mercados ORDER by txmercado");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_mercados.Add(new Mercados(Convert.ToInt32(reader["idmercado"]), Convert.ToString(reader["txmercado"]), 0));
                }
                reader.Close();
                session.Close();

                return list_mercados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Mercados> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Mercados> list_mercados = new List<Mercados>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select  COUNT(*) OVER() as total, * from Mercados ORDER by txmercado OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_mercados.Add(new Mercados(Convert.ToInt32(reader["idmercado"]), Convert.ToString(reader["txmercado"]), Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_mercados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Mercados> Listar(string mercado, int page = 1, int regs = 10)
        {
            try
            {
                List<Mercados> list_mercados = new List<Mercados>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, * from Mercados WHERE txmercado LIKE @mercado ORDER by txmercado OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("mercado", "%" + mercado.Replace(" ", "%") + "%");
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_mercados.Add(new Mercados(Convert.ToInt32(reader["idmercado"]), Convert.ToString(reader["txmercado"]), Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_mercados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Mercados Buscar(int id)
        {
            try
            {
                Mercados Mercados = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Mercados WHERE idmercado = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    Mercados = new Mercados(Convert.ToInt32(reader["idmercado"]), Convert.ToString(reader["txmercado"]), 0);
                }
                reader.Close();
                session.Close();

                return Mercados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
