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
                Query query = session.CreateQuery("INSERT INTO Cursos (idorganizador, txcurso, idtema,idcategoria, idlocal, nrminimo, nrmaximo, txcargahoraria, flgratuito, txlocal, txdescritivo, idcor, txidentificador) output INSERTED.idcurso VALUES (@organizador, @curso, @tema, @categoria, @codlocal, @minimo, @maximo, @carga, @gratuito, @local, @descritivo, @cor, @identificador)");
                query.SetParameter("organizador", variavel.idorganizador);
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
                query.SetParameter("cor", variavel.idcor);
                query.SetParameter("identificador", variavel.txidentificador);
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
                Query query = session.CreateQuery("UPDATE Cursos SET txcurso = @curso, idtema = @tema, idcategoria = @categoria, idlocal = @codlocal, nrminimo = @minimo, nrmaximo = @maximo, txcargahoraria = @carga, flgratuito = @gratuito, txlocal = @local, txdescritivo = @descritivo, idcor = @cor, txidentificador = @identificador WHERE idcurso = @id");
                query.SetParameter("id", variavel.idcurso);
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
                query.SetParameter("cor", variavel.idcor);
                query.SetParameter("identificador", variavel.txidentificador);
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

        public List<Cursos> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, idcurso, txcurso, isnull(idtema,0) as idtema, isnull(idcategoria,0) as idcategoria, isnull(idlocal,0) as idlocal, txlocal, isnull(nrminimo,0) as nrminimo, isnull(nrmaximo,0) as nrmaximo, txcargahoraria, txdescritivo, isnull(flgratuito,0) as flgratuito, txfoto, isnull(idcor,0) as idcor, txidentificador from cursos where idorganizador = @organizador ORDER by txcurso OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["nrminimo"]), Convert.ToString(reader["nrmaximo"]), Convert.ToString(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["idcor"]), Convert.ToString(reader["txidentificador"]), Convert.ToInt32(reader["total"])));
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

        public List<Cursos> Listar(string curso, int page = 1, int regs = 10)
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, idcurso, txcurso, isnull(idtema,0) as idtema, isnull(idcategoria,0) as idcategoria, isnull(idlocal,0) as idlocal, txlocal, isnull(nrminimo,0) as nrminimo, isnull(nrmaximo,0) as nrmaximo, txcargahoraria, txdescritivo, isnull(flgratuito,0) as flgratuito, txfoto, isnull(idcor,0) as idcor, txidentificador from cursos where idorganizador = @organizador and txcurso LIKE @curso OR txidentificador LIKE @curso ORDER by txcurso OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("curso", "%" + curso.Replace(" ", "%") + "%");
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["nrminimo"]), Convert.ToString(reader["nrmaximo"]), Convert.ToString(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["idcor"]), Convert.ToString(reader["txidentificador"]), Convert.ToInt32(reader["total"])));
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
                Query quey = session.CreateQuery("SELECT idcurso, txcurso, isnull(idtema,0) as idtema, isnull(idcategoria,0) as idcategoria, isnull(idlocal,0) as idlocal, txlocal, isnull(nrminimo,0) as nrminimo, isnull(nrmaximo,0) as nrmaximo, txcargahoraria, txdescritivo, isnull(flgratuito,0) as flgratuito, txfoto, isnull(idcor,0) as idcor, txidentificador FROM Cursos WHERE idcurso = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    Cursos = new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["nrminimo"]), Convert.ToString(reader["nrmaximo"]), Convert.ToString(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["idcor"]), Convert.ToString(reader["txidentificador"]), 0);
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

        public void RemoverInstrutores(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Cursos_Instrutores WHERE idcurso = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarInstrutor(int id, int instrutor)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Cursos_Instrutores (idcurso, idinstrutor) VALUES (@curso, @instrutor)");
                query.SetParameter("curso", id);
                query.SetParameter("instrutor", instrutor);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void RemoverAlunos(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Cursos_Alunos WHERE idcurso = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarAlunos(int id, int aluno)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Cursos_Alunos (idcurso, idaluno, txavaliacao) VALUES (@curso, @aluno, '')");
                query.SetParameter("curso", id);
                query.SetParameter("aluno", aluno);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void InserirAvaliacao(int id = 0, string avaliacao = "")
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Cursos_Alunos SET txavaliacao = @avaliacao WHERE idcursoaluno = @id");
                query.SetParameter("id", id);
                query.SetParameter("avaliacao", avaliacao);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}
