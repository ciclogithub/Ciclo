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
    public class AlunosDB
    {
        public int Salvar(Alunos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Alunos (idorganizador, txaluno, txcpf) output INSERTED.idaluno VALUES (@organizador, @aluno, @cpf)");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("cpf", variavel.txcpf);
                int ident = query.ExecuteScalar();
                session.Close();

                return ident;

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Alunos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Alunos SET txaluno = @aluno, txcpf = @cpf WHERE idaluno = @id");
                query.SetParameter("id", variavel.idaluno);
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("cpf", variavel.txcpf);
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
                Query queryi = sessioni.CreateQuery("DELETE FROM Organizadores_Alunos WHERE idaluno = @id");
                queryi.SetParameter("id", id);
                queryi.ExecuteUpdate();
                sessioni.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Alunos> Listar()
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos WHERE idorganizador = @idorganizador ORDER by txaluno");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"])));
                }
                reader.Close();
                session.Close();

                return list_aluno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Alunos> Listar(string aluno)
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos WHERE (txaluno LIKE @aluno OR txcpf LIKE @aluno) AND idorganizador = @idorganizador ORDER by txaluno");
                query.SetParameter("aluno", "%" + aluno.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"])));
                }
                reader.Close();
                session.Close();

                return list_aluno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Alunos Buscar(int id)
        {
            try
            {
                Alunos alunos = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Alunos WHERE idaluno = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    alunos = new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]));
                }
                reader.Close();
                session.Close();

                return alunos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RemoverEmails(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Alunos_Emails WHERE idaluno = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void RemoverTelefones(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Alunos_Telefones WHERE idaluno = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarEmail(int id, string email)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Alunos_Emails (idaluno, txemail) VALUES (@aluno, @email)");
                query.SetParameter("aluno", id);
                query.SetParameter("email", email);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarTelefone(int id, string telefone)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Alunos_Telefones (idaluno, txtelefone) VALUES (@aluno, @telefone)");
                query.SetParameter("aluno", id);
                query.SetParameter("telefone", telefone);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Emails> ListarEmails(int id = 0)
        {
            try
            {
                List<Emails> list_email = new List<Emails>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos_Emails where idaluno = @id order by txemail");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_email.Add(new Emails(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txemail"])));
                }
                reader.Close();
                session.Close();

                return list_email;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Telefones> ListarTelefones(int id = 0)
        {
            try
            {
                List<Telefones> list_telefone = new List<Telefones>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos_Telefones where idaluno = @id order by txtelefone");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_telefone.Add(new Telefones(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txtelefone"])));
                }
                reader.Close();
                session.Close();

                return list_telefone;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Alunos> ListarTodos(int id = 0)
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos where idorganizador = @organizador and idaluno not in (select idaluno from Cursos_Alunos where idcurso = @id) order by txaluno");
                query.SetParameter("id", id);
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"])));
                }
                reader.Close();
                session.Close();

                return list_aluno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Alunos> ListarTodos(string aluno = "", string lista = "")
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos where idorganizador = @organizador and txaluno like @aluno and idaluno not in (" + lista + ") order by txaluno");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("aluno", "%" + aluno.Replace(" ", "%") + "%");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"])));
                }
                reader.Close();
                session.Close();

                return list_aluno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Alunos> ListarDoCurso(int id = 0)
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos where idorganizador = @organizador and idaluno in (select idaluno from Cursos_Alunos where idcurso = @id) order by txaluno");
                query.SetParameter("id", id);
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"])));
                }
                reader.Close();
                session.Close();

                return list_aluno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

