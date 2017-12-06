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
                Query query = session.CreateQuery("INSERT INTO Cursos (idorganizador, txcurso, idtema,idcategoria, idlocal, nrminimo, nrmaximo, txcargahoraria, flgratuito, txlocal, txdescritivo, idcor, txidentificador, idespecialidade) output INSERTED.idcurso VALUES (@organizador, @curso, @tema, @categoria, @codlocal, @minimo, @maximo, @carga, @gratuito, @local, @descritivo, @cor, @identificador, @especialidade)");
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
                query.SetParameter("especialidade", variavel.idespecialidade);
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
                Query query = session.CreateQuery("UPDATE Cursos SET txcurso = @curso, idtema = @tema, idcategoria = @categoria, idlocal = @codlocal, nrminimo = @minimo, nrmaximo = @maximo, txcargahoraria = @carga, flgratuito = @gratuito, txlocal = @local, txdescritivo = @descritivo, idcor = @cor, txidentificador = @identificador, idespecialidade = @especialidade WHERE idcurso = @id");
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
                query.SetParameter("especialidade", variavel.idespecialidade);
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
                DBSession sessionca = new DBSession();
                Query queryca = sessionca.CreateQuery("DELETE FROM Cursos_Alunos WHERE idcurso = @id");
                queryca.SetParameter("id", id);
                queryca.ExecuteUpdate();
                sessionca.Close();

                DBSession sessioncd = new DBSession();
                Query querycd = sessioncd.CreateQuery("DELETE FROM Cursos_datas WHERE idcurso = @id");
                querycd.SetParameter("id", id);
                querycd.ExecuteUpdate();
                sessioncd.Close();

                DBSession sessioncv = new DBSession();
                Query querycv = sessioncv.CreateQuery("DELETE FROM Cursos_valores WHERE idcurso = @id");
                querycv.SetParameter("id", id);
                querycv.ExecuteUpdate();
                sessioncv.Close();

                DBSession sessionci = new DBSession();
                Query queryci = sessionci.CreateQuery("DELETE FROM Cursos_Instrutores WHERE idcurso = @id");
                queryci.SetParameter("id", id);
                queryci.ExecuteUpdate();
                sessionci.Close();

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

        public string ListarNomes(string id)
        {
            try
            {
                string result = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select SUBSTRING((select ', ' + txcurso from cursos where idcurso in (" + id + ") FOR XML PATH('')),3,9999) as result");
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

        public List<Cursos> Listar()
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select idcurso, txcurso, isnull(idtema,0) as idtema, isnull(idcategoria,0) as idcategoria, isnull(idlocal,0) as idlocal, txlocal, isnull(nrminimo,0) as nrminimo, isnull(nrmaximo,0) as nrmaximo, txcargahoraria, txdescritivo, isnull(flgratuito,0) as flgratuito, txfoto, isnull(idcor,0) as idcor, txidentificador, isnull(idespecialidade,0) as idespecialidade from cursos where idorganizador = @organizador ORDER by txcurso");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["nrminimo"]), Convert.ToString(reader["nrmaximo"]), Convert.ToString(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["idcor"]), Convert.ToString(reader["txidentificador"]), 0, Convert.ToInt32(reader["idespecialidade"])));
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

        public List<Cursos> ListarRel()
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select idcurso, txcurso from cursos where idorganizador = @organizador ORDER by txcurso");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"])));
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

        public List<Cursos> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, idcurso, txcurso, isnull(idcategoria,0) as idcategoria, isnull(flgratuito,0) as flgratuito, isnull(idcor,0) as idcor from cursos where idorganizador = @organizador ORDER by txcurso OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToInt32(reader["idcor"]), Convert.ToInt32(reader["total"])));
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

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, idcurso, txcurso, isnull(idtema,0) as idtema, isnull(idcategoria,0) as idcategoria, isnull(idlocal,0) as idlocal, txlocal, isnull(nrminimo,0) as nrminimo, isnull(nrmaximo,0) as nrmaximo, txcargahoraria, txdescritivo, isnull(flgratuito,0) as flgratuito, txfoto, isnull(idcor,0) as idcor, txidentificador, isnull(idespecialidade,0) as idespecialidade from cursos where idorganizador = @organizador and txcurso LIKE @curso OR txidentificador LIKE @curso ORDER by txcurso OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("curso", "%" + curso.Replace(" ", "%") + "%");
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["nrminimo"]), Convert.ToString(reader["nrmaximo"]), Convert.ToString(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["idcor"]), Convert.ToString(reader["txidentificador"]), Convert.ToInt32(reader["total"]), Convert.ToInt32(reader["idespecialidade"])));
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
                Query quey = session.CreateQuery("SELECT idcurso, txcurso, isnull(idtema,0) as idtema, isnull(idcategoria,0) as idcategoria, isnull(idlocal,0) as idlocal, txlocal, isnull(nrminimo,0) as nrminimo, isnull(nrmaximo,0) as nrmaximo, txcargahoraria, txdescritivo, isnull(flgratuito,0) as flgratuito, txfoto, isnull(idcor,0) as idcor, txidentificador, isnull(idespecialidade,0) as idespecialidade FROM Cursos WHERE idcurso = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    Cursos = new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idtema"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToInt32(reader["idlocal"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["nrminimo"]), Convert.ToString(reader["nrmaximo"]), Convert.ToString(reader["txcargahoraria"]), Convert.ToString(reader["txdescritivo"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToString(reader["txfoto"]), Convert.ToInt32(reader["idcor"]), Convert.ToString(reader["txidentificador"]), 0, Convert.ToInt32(reader["idespecialidade"]));
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

                Cursos_Alunos curso_aluno = new CursosAlunosDB().Buscar(id);
                String msg = "Prezado(a) " + curso_aluno.aluno + ", clique no link abaixo, ou copie e cole no seu navegador para responder a pesquisa de avaliação do curso " + curso_aluno.curso + ".<br><br><a href='http://www.treinaauto.com.br/avaliacao?c=" + avaliacao + "'>http://www.treinaauto.com.br/avaliacao?c=" + avaliacao + "</a>";
                String assunto = "Pesquisa de avaliação do curso " + curso_aluno.curso;

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session2 = new DBSession();
                Query query2 = session2.CreateQuery("INSERT INTO Emails (idorganizador, txassunto, txpara, txresposta, txmensagem) VALUES (@organizador, @assunto, @para, @resposta, @mensagem)");
                query2.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query2.SetParameter("assunto", assunto);
                query2.SetParameter("para", curso_aluno.emails);
                query2.SetParameter("resposta", curso_aluno.emails);
                query2.SetParameter("mensagem", msg);
                query2.ExecuteUpdate();
                session2.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public int VerificaCursoIdent(int id, string identificador)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from cursos where idorganizador = @idorganizador and idcurso <> @id and txidentificador = @identificador");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("id", id);
                query.SetParameter("identificador", identificador);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    result = 2;
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

        public int VerificaCurso(int id, string nome)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from cursos where idorganizador = @idorganizador and idcurso <> @id and txcurso COLLATE Latin1_general_CI_AI = @nome COLLATE Latin1_general_CI_AI");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("id", id);
                query.SetParameter("nome", nome);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    result = 1;
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

        public int VerificaCursoExcluir(string ids)
        {
            try
            {
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select top 1 * from Cursos_Alunos where idcurso in (" + ids + ")");
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    result = 1;
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
    }
}
