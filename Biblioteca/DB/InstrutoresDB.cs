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
    public class InstrutoresDB
    {
        public int Salvar(Instrutores variavel)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Instrutores (txinstrutor, txemail, txtelefone, txdescritivo) output INSERTED.idinstrutor VALUES (@instrutor, @email, @telefone, @descritivo)");
                query.SetParameter("instrutor", variavel.txinstrutor);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("telefone", variavel.txtelefone);
                query.SetParameter("descritivo", variavel.txdescritivo);
                int ident = query.ExecuteScalar();
                session.Close();

                DBSession sessioni = new DBSession();
                Query queryi = sessioni.CreateQuery("INSERT INTO Organizadores_Instrutores (idorganizador, idinstrutor) VALUES (@organizador, @instrutor)");
                queryi.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                queryi.SetParameter("instrutor", ident);
                queryi.ExecuteUpdate();
                sessioni.Close();

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

        public void AlterarFoto(Instrutores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Instrutores SET txfoto = @foto WHERE idinstrutor = @id");
                query.SetParameter("id", variavel.idinstrutor);
                query.SetParameter("foto", variavel.txfoto);
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
                DBSession sessioni = new DBSession();
                Query queryi = sessioni.CreateQuery("DELETE FROM Organizadores_Instrutores WHERE idinstrutor = @id");
                queryi.SetParameter("id", id);
                queryi.ExecuteUpdate();
                sessioni.Close();

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

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select i.* from Instrutores i inner join Organizadores_Instrutores oi on oi.idinstrutor = i.idinstrutor WHERE oi.idorganizador = @idorganizador ORDER by i.txinstrutor");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"])));
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

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select i.* from Instrutores i inner join Organizadores_Instrutores oi on oi.idinstrutor = i.idinstrutor WHERE oi.idorganizador = @idorganizador and i.txinstrutor LIKE @instrutor ORDER by i.txinstrutor");
                query.SetParameter("instrutor", "%" + instrutor.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"])));
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

        public Instrutores Buscar(int id)
        {
            try
            {
                Instrutores instrutores = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM instrutores WHERE idinstrutor = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    instrutores = new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]));
                }
                reader.Close();
                session.Close();

                return instrutores;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
