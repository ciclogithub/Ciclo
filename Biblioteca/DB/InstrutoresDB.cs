using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca.DB
{
    public class InstrutoresDB
    {
        public int Salvar(Instrutores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Instrutores (txinstrutor, txemail, txtelefone, txdescritivo) output INSERTED.idinstrutor VALUES (@instrutor, @email, @telefone, @descritivo)");
                query.SetParameter("instrutor", variavel.txinstrutor);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("telefone", variavel.txtelefone);
                query.SetParameter("descritivo", variavel.txdescritivo);
                int ident = query.ExecuteScalar();
                session.Close();
                return ident;

          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Instrutores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Instrutores SET txinstrutor = @empresa, txemail = @email, txtelefone = @telefone, txdescritivo = @descritivo WHERE idinstrutor = @id");
                query.SetParameter("id", variavel.idinstrutor);
                query.SetParameter("empresa", variavel.txinstrutor);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("telefone", variavel.txtelefone);
                query.SetParameter("descritivo", variavel.txdescritivo);
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
                Query query = session.CreateQuery("DELETE FROM Instrutores WHERE idinstrutor = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Instrutores> Listar()
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Instrutores ORDER by txinstrutor");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"])));
                }
                reader.Close();
                session.Close();

                return list_instrutor;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Instrutores> Listar(string instrutor)
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Instrutores WHERE txinstrutor LIKE @instrutor ORDER by txinstrutor");
                query.SetParameter("instrutor", "%" + list_instrutor + "%");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"])));
                }
                reader.Close();
                session.Close();

                return list_instrutor;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
