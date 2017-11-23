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
    public class EspecialidadesDB
    {
        public void Salvar(Especialidades variavel)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Especialidades (idorganizador, txespecialidade, txsigla) VALUES (@organizador, @especialidade, @sigla)");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("especialidade", variavel.txespecialidade);
                query.SetParameter("sigla", variavel.txsigla);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Especialidades variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Especialidades SET txespecialidade = @especialidade, txsigla = @sigla WHERE idespecialidade = @id");
                query.SetParameter("id", variavel.idespecialidade);
                query.SetParameter("especialidade", variavel.txespecialidade);
                query.SetParameter("sigla", variavel.txsigla);
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
                Query query = session.CreateQuery("DELETE FROM Especialidades WHERE idespecialidade = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Especialidades> Listar()
        {
            try
            {
                List<Especialidades> list_especialidades = new List<Especialidades>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select  * from Especialidades WHERE idorganizador = @idorganizador ORDER by txespecialidade");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_especialidades.Add(new Especialidades(Convert.ToInt32(reader["idespecialidade"]), Convert.ToString(reader["txespecialidade"]), Convert.ToString(reader["txsigla"]), 0));
                }
                reader.Close();
                session.Close();

                return list_especialidades;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Especialidades> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Especialidades> list_especialidades = new List<Especialidades>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select  COUNT(*) OVER() as total, * from Especialidades WHERE idorganizador = @idorganizador ORDER by txespecialidade OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_especialidades.Add(new Especialidades(Convert.ToInt32(reader["idespecialidade"]), Convert.ToString(reader["txespecialidade"]), Convert.ToString(reader["txsigla"]), Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_especialidades;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Especialidades> Listar(string especialidade, int page = 1, int regs = 10)
        {
            try
            {
                List<Especialidades> list_especialidades = new List<Especialidades>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, * from Especialidades WHERE idorganizador = @idorganizador and txespecialidade LIKE @especialidade OR txsigla LIKE @especialidade ORDER by txespecialidade OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("especialidade", "%" + especialidade.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_especialidades.Add(new Especialidades(Convert.ToInt32(reader["idespecialidade"]), Convert.ToString(reader["txespecialidade"]), Convert.ToString(reader["txsigla"]), Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_especialidades;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Especialidades Buscar(int id)
        {
            try
            {
                Especialidades Especialidades = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Especialidades WHERE idespecialidade = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    Especialidades = new Especialidades(Convert.ToInt32(reader["idespecialidade"]), Convert.ToString(reader["txespecialidade"]), Convert.ToString(reader["txsigla"]),0);
                }
                reader.Close();
                session.Close();

                return Especialidades;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
