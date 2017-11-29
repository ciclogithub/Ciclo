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
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Instrutores (idorganizador, txinstrutor, txemail, txtelefone, txdescritivo) output INSERTED.idinstrutor VALUES (@organizador, @instrutor, @email, @telefone, @descritivo)");
                query.SetParameter("organizador", variavel.idorganizador);
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
                DBSession session2 = new DBSession();
                Query query2 = session2.CreateQuery("DELETE FROM Cursos_Instrutores WHERE idinstrutor = @id");
                query2.SetParameter("id", id);
                query2.ExecuteUpdate();
                session2.Close();

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

        public string ListarNomes(string id)
        {
            try
            {
                string result = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select SUBSTRING((select ', ' + txinstrutor from instrutores where idinstrutor in (" + id + ") FOR XML PATH('')),3,9999) as result");
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

        public List<Instrutores> Listar()
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Instrutores WHERE idorganizador = @idorganizador ORDER by txinstrutor");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]), 0));
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

        public List<Instrutores> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* from Instrutores WHERE idorganizador = @idorganizador ORDER by txinstrutor OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["total"])));
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

        public List<Instrutores> Listar(string instrutor, int page = 1, int regs = 10)
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* from Instrutores WHERE idorganizador = @idorganizador and txinstrutor LIKE @instrutor ORDER by txinstrutor OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("instrutor", "%" + instrutor.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["total"])));
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

        public List<Instrutores> ListarTodos(int id = 0)
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Instrutores where idorganizador = @organizador and idinstrutor not in (select idinstrutor from Cursos_Instrutores where idcurso = @id) order by txinstrutor");
                query.SetParameter("id", id);
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]),0));
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

        public List<Instrutores> ListarTodos(string instrutor = "", string lista = "")
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Instrutores where idorganizador = @organizador and txinstrutor like @instrutor and idinstrutor not in (" + lista + ") order by txinstrutor");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("instrutor", "%" + instrutor.Replace(" ", "%") + "%");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]),0));
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

        public List<Instrutores> ListarDoCurso(int id = 0)
        {
            try
            {
                List<Instrutores> list_instrutor = new List<Instrutores>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Instrutores where idorganizador = @organizador and idinstrutor in (select idinstrutor from Cursos_Instrutores where idcurso = @id) order by txinstrutor");
                query.SetParameter("id", id);
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_instrutor.Add(new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]),0));
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
                    instrutores = new Instrutores(Convert.ToInt32(reader["idinstrutor"]), Convert.ToString(reader["txinstrutor"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]),0);
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
