using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Biblioteca.Funcoes;

namespace Biblioteca.DB
{
    public class AlunosDB
    {
        public int Salvar(Alunos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Alunos (idorganizador, txaluno, txcpf, idcidade, idcor, idempresa, txobs) output INSERTED.idaluno VALUES (@organizador, @aluno, @cpf, @especialidade, @cidade, @cor, @empresa, @obs)");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("cpf", variavel.txcpf);
                query.SetParameter("cidade", variavel.idcidade);
                query.SetParameter("cor", variavel.idcor);
                query.SetParameter("empresa", variavel.idempresa);
                query.SetParameter("obs", variavel.txobs);
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
                Query query = session.CreateQuery("UPDATE Alunos SET txaluno = @aluno, txcpf = @cpf, idcidade = @cidade, idcor = @cor, idempresa = @empresa, txobs = @obs WHERE idaluno = @id");
                query.SetParameter("id", variavel.idaluno);
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("cpf", variavel.txcpf);
                query.SetParameter("cidade", variavel.idcidade);
                query.SetParameter("cor", variavel.idcor);
                query.SetParameter("empresa", variavel.idempresa);
                query.SetParameter("obs", variavel.txobs);
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

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos WHERE idorganizador = @idorganizador ORDER by txaluno");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToInt32(reader["idcidade"]), Convert.ToInt32(reader["idcor"]), Convert.ToInt32(reader["idempresa"]), 0, Convert.ToString(reader["txobs"]),0));
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

        public List<Alunos> ListarRel()
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos WHERE idorganizador = @idorganizador ORDER by txaluno");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"])));
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

        public List<Alunos> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,*, isnull((case when A.txcpf = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = a.txcpf OR EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) end), 0) as diagweb from Alunos A WHERE A.idorganizador = @idorganizador ORDER by A.txaluno OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToInt32(reader["idcidade"]), Convert.ToInt32(reader["idcor"]), Convert.ToInt32(reader["idempresa"]), Convert.ToInt32(reader["total"]), Convert.ToString(reader["txobs"]), Convert.ToInt32(reader["diagweb"])));
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

        public List<Alunos> Listar(string aluno, int page = 1, int regs = 10)
        {
            try
            {
                List<Alunos> list_aluno = new List<Alunos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,*, isnull((case when A.txcpf = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = a.txcpf OR EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) end), 0) as diagweb from Alunos A WHERE (A.txaluno LIKE @aluno OR A.txcpf LIKE @aluno) AND A.idorganizador = @idorganizador ORDER by A.txaluno OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("aluno", "%" + aluno.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToInt32(reader["idcidade"]), Convert.ToInt32(reader["idcor"]), Convert.ToInt32(reader["idempresa"]), Convert.ToInt32(reader["total"]), Convert.ToString(reader["txobs"]), Convert.ToInt32(reader["diagweb"])));
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
                    alunos = new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToInt32(reader["idcidade"]), Convert.ToInt32(reader["idcor"]), Convert.ToInt32(reader["idempresa"]), 0, Convert.ToString(reader["txobs"]),0);
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

        public void RemoverMercados(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Alunos_Mercados WHERE idaluno = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
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

        public void RemoverRedesSociais(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Alunos_RedesSociais WHERE idaluno = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarMercado(int id, int mercado)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Alunos_Mercados (idaluno, idmercado) VALUES (@aluno, @mercado)");
                query.SetParameter("aluno", id);
                query.SetParameter("mercado", mercado);
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

        public void SalvarTelefone(int id, string telefone, int whatsapp)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Alunos_Telefones (idaluno, txtelefone, flwhatsapp) VALUES (@aluno, @telefone, @whatsapp)");
                query.SetParameter("aluno", id);
                query.SetParameter("telefone", telefone);
                query.SetParameter("whatsapp", whatsapp);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarRedeSocial(int id, string rede, int codigo)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Alunos_RedesSociais (idaluno, idredesocial, txredesocial) VALUES (@aluno, @codigo, @rede)");
                query.SetParameter("aluno", id);
                query.SetParameter("codigo", codigo);
                query.SetParameter("rede", rede);
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

        public List<Mercados> ListarMercados(int id = 0)
        {
            try
            {
                List<Mercados> list_mercados = new List<Mercados>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select m.idmercado, m.txmercado from Alunos_Mercados am left join mercados m on m.idmercado = am.idmercado where am.idaluno = @id order by m.txmercado");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_mercados.Add(new Mercados(Convert.ToInt32(reader["idmercado"]), Convert.ToString(reader["txmercado"]), 0));
                }
                reader.Close();
                session.Close();

                return list_mercados;
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
                    list_telefone.Add(new Telefones(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txtelefone"]), Convert.ToInt32(reader["flwhatsapp"])));
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

        public List<Redes> ListarRedesSociais(int id = 0)
        {
            try
            {
                List<Redes> list_redes = new List<Redes>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select AR.*, RS.txicone from Alunos_RedesSociais AR Inner Join RedesSociais RS on RS.idredesocial = AR.idredesocial where AR.idaluno = @id order by RS.txredesocial");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_redes.Add(new Redes(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txredesocial"]), Convert.ToString(reader["txicone"]), Convert.ToInt32(reader["idredesocial"])));
                }
                reader.Close();
                session.Close();

                return list_redes;
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

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos where idorganizador = @organizador and idaluno not in (select idaluno from Cursos_Alunos where idcurso = @id) order by txaluno");
                query.SetParameter("id", id);
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"])));
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

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos where idorganizador = @organizador and txaluno like @aluno and idaluno not in (" + lista + ") order by txaluno");
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("aluno", "%" + aluno.Replace(" ", "%") + "%");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"])));
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

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Alunos where idorganizador = @organizador and idaluno in (select idaluno from Cursos_Alunos where idcurso = @id) order by txaluno");
                query.SetParameter("id", id);
                query.SetParameter("organizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Alunos(Convert.ToInt32(reader["idaluno"]), Convert.ToString(reader["txaluno"])));
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

