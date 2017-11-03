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
    public class TemasDB
    {
        public void Salvar(Temas variavel)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Temas (idorganizador, txtema, txsubtitulo, txdescritivo) VALUES (@organizador, @tema, @subtitulo, @descritivo)");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("tema", variavel.txtema);
                query.SetParameter("subtitulo", variavel.txsubtitulo);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Temas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Temas SET txtema = @tema, txsubtitulo = @subtitulo, txdescritivo = @descritivo WHERE idtema = @id");
                query.SetParameter("id", variavel.idtema);
                query.SetParameter("tema", variavel.txtema);
                query.SetParameter("subtitulo", variavel.txsubtitulo);
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
                Query query = session.CreateQuery("DELETE FROM Temas WHERE idtema = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Temas> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Temas> list_temas = new List<Temas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* from Temas WHERE idorganizador = @idorganizador ORDER by txtema OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_temas.Add(new Temas(Convert.ToInt32(reader["idtema"]), Convert.ToString(reader["txtema"]), Convert.ToString(reader["txsubtitulo"]), Convert.ToString(reader["txdescritivo"]), Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_temas;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Temas> Listar(string tema, int page = 1, int regs = 10)
        {
            try
            {
                List<Temas> list_temas = new List<Temas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* from Temas WHERE idorganizador = @idorganizador and txtema LIKE @tema ORDER by txtema OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("tema", "%" + tema.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_temas.Add(new Temas(Convert.ToInt32(reader["idtema"]), Convert.ToString(reader["txtema"]), Convert.ToString(reader["txsubtitulo"]), Convert.ToString(reader["txdescritivo"]), Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_temas;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Temas Buscar(int id)
        {
            try
            {
                Temas Temas = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Temas WHERE idtema = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    Temas = new Temas(Convert.ToInt32(reader["idtema"]), Convert.ToString(reader["txtema"]), Convert.ToString(reader["txsubtitulo"]), Convert.ToString(reader["txdescritivo"]), 0);
                }
                reader.Close();
                session.Close();

                return Temas;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
