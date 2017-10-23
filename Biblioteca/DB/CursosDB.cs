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
    public class CursosDB
    {
        public int Salvar(Cursos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Cursos (txcurso, idtema,idcategoria, idlocal, nrminimo, nrmaximo, txcargahoraria, flgratuito, txlocal, txdescritivo, txfoto) output INSERTED.idcurso VALUES (@curso, @tema, @categoria, @codlocal, @minimo, @maximo, @carga, @gratuito, @local, @descritivo, @foto)");
                query.SetParameter("curso", variavel.txcurso);
                query.SetParameter("tema", variavel.idtema);
                query.SetParameter("categoria", variavel.idcategoria);
                query.SetParameter("codlocal", variavel.idlocal);
                query.SetParameter("minimo", variavel.nrminimo);
                query.SetParameter("maximo", variavel.nrmaximo);
                query.SetParameter("carga", variavel.txcargahoraria);
                query.SetParameter("gratuito", variavel.flgratuito);
                query.SetParameter("local", variavel.txlocal);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.SetParameter("foto", variavel.txfoto);
                int ident = query.ExecuteScalar();
                session.Close();

                return ident;
          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Cursos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Cursos SET txcurso = @curso, idtema = @tema, idcategoria = @categoria, idlocal = @codlocal WHERE idcurso = @id");
                query.SetParameter("id", variavel.idcurso);
                query.SetParameter("curso", variavel.txcurso);
                query.SetParameter("tema", variavel.idtema);
                query.SetParameter("categoria", variavel.idcategoria);
                query.SetParameter("codlocal", variavel.idlocal);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void AlterarFoto(Cursos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Cursos SET txfoto = @foto WHERE idcurso = @id");
                query.SetParameter("id", variavel.idcurso);
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
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Cursos WHERE idcurso = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Cursos> Listar()
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select c.* from cursos c inner join Cursos_Instrutores ci on c.idcurso = ci.idcurso inner join Organizadores_Instrutores oi on oi.idinstrutor = ci.idinstrutor where oi.idorganizador = @idorganizador ORDER by c.txcurso");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToInt32(reader["nrminimo"]), Convert.ToInt32(reader["nrmaximo"]), Convert.ToInt32(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"])));
                }
                reader.Close();
                session.Close();

                return list_cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Cursos> Listar(string curso)
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select c.* from cursos c inner join Cursos_Instrutores ci on c.idcurso = ci.idcurso inner join Organizadores_Instrutores oi on oi.idinstrutor = ci.idinstrutor where oi.idorganizador = @idorganizador and c.txcurso LIKE @curso ORDER by c.txcurso");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("curso", "%" + curso.Replace(" ", "%") + "%");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToInt32(reader["nrminimo"]), Convert.ToInt32(reader["nrmaximo"]), Convert.ToInt32(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"])));
                }
                reader.Close();
                session.Close();

                return list_cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Cursos Buscar(int id)
        {
            try
            {
                Cursos Cursos = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Cursos WHERE idcurso = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    Cursos = new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToInt32(reader["nrminimo"]), Convert.ToInt32(reader["nrmaximo"]), Convert.ToInt32(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"]));
                }
                reader.Close();
                session.Close();

                return Cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
