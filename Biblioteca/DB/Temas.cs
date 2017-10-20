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
        public int Salvar(Temas variavel)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Temas (txtema, txsubtitulo, txdescritivo) output INSERTED.idtema VALUES (@tema, @subtitulo, @descritivo)");
                query.SetParameter("tema", variavel.txtema);
                query.SetParameter("subtitulo", variavel.txsubtitulo);
                query.SetParameter("descritivo", variavel.txdescritivo);
                int ident = query.ExecuteScalar();
                session.Close();

                DBSession sessioni = new DBSession();
                Query queryi = sessioni.CreateQuery("INSERT INTO Organizadores_Temas (idorganizador, idtema) VALUES (@organizador, @tema)");
                queryi.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                queryi.SetParameter("tema", ident);
                queryi.ExecuteUpdate();
                sessioni.Close();

                return ident;
          
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
                DBSession sessioni = new DBSession();
                Query queryi = sessioni.CreateQuery("DELETE FROM Organizadores_Temas WHERE idtema = @id");
                queryi.SetParameter("id", id);
                queryi.ExecuteUpdate();
                sessioni.Close();

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

        public List<Temas> Listar()
        {
            try
            {
                List<Temas> list_temas = new List<Temas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select t.* from Temas t inner join Organizadores_Temas ot on ot.idtema = t.idtema WHERE ot.idorganizador = @idorganizador ORDER by t.txtema");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_temas.Add(new Temas(Convert.ToInt32(reader["idtema"]), Convert.ToString(reader["txtema"]), Convert.ToString(reader["txsubtitulo"]), Convert.ToString(reader["txdescritivo"])));
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

        public List<Temas> Listar(string tema)
        {
            try
            {
                List<Temas> list_temas = new List<Temas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select t.* from Temas t inner join Organizadores_Temas ot on ot.idtema = t.idtema WHERE ot.idorganizador = @idorganizador and t.txtema LIKE @tema ORDER by t.txtema");
                query.SetParameter("tema", "%" + tema.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_temas.Add(new Temas(Convert.ToInt32(reader["idtema"]), Convert.ToString(reader["txtema"]), Convert.ToString(reader["txsubtitulo"]), Convert.ToString(reader["txdescritivo"])));
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
                    Temas = new Temas(Convert.ToInt32(reader["idtema"]), Convert.ToString(reader["txtema"]), Convert.ToString(reader["txsubtitulo"]), Convert.ToString(reader["txdescritivo"]));
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
