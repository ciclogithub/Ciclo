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
    public class EmpresasDB
    {
        public int Salvar(Empresas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Empresas (idorganizador, txempresa, txcnpj, txcodigo, nrcep, idcidade, txnumero, txlogradouro, txcomplemento, txbairro) output INSERTED.idempresa VALUES (@organizador, @empresa, @cnpj, @codigo, @cep, @cidade, @numero, @logradouro, @complemento, @bairro)");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("empresa", variavel.txempresa);
                query.SetParameter("cnpj", variavel.txcnpj);
                query.SetParameter("codigo", variavel.txcodigo);
                query.SetParameter("cep", variavel.nrcep);
                query.SetParameter("cidade", variavel.idcidade);
                query.SetParameter("numero", variavel.txnumero);
                query.SetParameter("logradouro", variavel.txlogradouro);
                query.SetParameter("complemento", variavel.txcomplemento);
                query.SetParameter("bairro", variavel.txbairro);
                int ident = query.ExecuteScalar();
                session.Close();

                return ident;

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Empresas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Empresas SET txempresa = @empresa, txcnpj = @cnpj, txcodigo = @codigo, nrcep = @cep, idcidade = @cidade, txnumero = @numero, txlogradouro = @logradouro, txcomplemento = @complemento, txbairro = @bairro WHERE idempresa = @id");
                query.SetParameter("id", variavel.idempresa);
                query.SetParameter("empresa", variavel.txempresa);
                query.SetParameter("cnpj", variavel.txcnpj);
                query.SetParameter("codigo", variavel.txcodigo);
                query.SetParameter("cep", variavel.nrcep);
                query.SetParameter("cidade", variavel.idcidade);
                query.SetParameter("numero", variavel.txnumero);
                query.SetParameter("logradouro", variavel.txlogradouro);
                query.SetParameter("complemento", variavel.txcomplemento);
                query.SetParameter("bairro", variavel.txbairro);
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
                Query queryi = sessioni.CreateQuery("DELETE FROM Empresas WHERE idempresa = @id");
                queryi.SetParameter("id", id);
                queryi.ExecuteUpdate();
                sessioni.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Empresas> Listar()
        {
            try
            {
                List<Empresas> list_empresa = new List<Empresas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Empresas WHERE idorganizador = @idorganizador ORDER by txempresa");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_empresa.Add(new Empresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["txcodigo"]), Convert.ToString(reader["nrcep"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txnumero"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcomplemento"]),0, Convert.ToString(reader["txbairro"]),0));
                }
                reader.Close();
                session.Close();

                return list_empresa;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Empresas> ListarRel()
        {
            try
            {
                List<Empresas> list_empresa = new List<Empresas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Empresas WHERE idorganizador = @idorganizador ORDER by txempresa");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_empresa.Add(new Empresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txempresa"])));
                }
                reader.Close();
                session.Close();

                return list_empresa;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Empresas> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Empresas> list_empresa = new List<Empresas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* ,isnull((case when em.txcnpj = '' then(select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = em.txcnpj OR EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) end), 0) as diagweb from Empresas em WHERE em.idorganizador = @idorganizador ORDER by em.txempresa OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_empresa.Add(new Empresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["txcodigo"]), Convert.ToString(reader["nrcep"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txnumero"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcomplemento"]), Convert.ToInt32(reader["total"]), Convert.ToString(reader["txbairro"]), Convert.ToInt32(reader["diagweb"])));
                }
                reader.Close();
                session.Close();

                return list_empresa;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Empresas> Listar(string empresa, int page = 1, int regs = 10)
        {
            try
            {
                List<Empresas> list_empresa = new List<Empresas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* ,isnull((case when em.txcnpj = '' then(select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = em.txcnpj OR EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) end), 0) as diagweb from Empresas em WHERE (txempresa LIKE @empresa OR txcnpj LIKE @empresa) AND idorganizador = @idorganizador ORDER by em.txempresa OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("empresa", "%" + empresa.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_empresa.Add(new Empresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["txcodigo"]), Convert.ToString(reader["nrcep"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txnumero"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcomplemento"]), Convert.ToInt32(reader["total"]), Convert.ToString(reader["txbairro"]), Convert.ToInt32(reader["diagweb"])));
                }
                reader.Close();
                session.Close();

                return list_empresa;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Empresas Buscar(int id)
        {
            try
            {
                Empresas empresas = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Empresas WHERE idempresa = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    empresas = new Empresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["txcodigo"]), Convert.ToString(reader["nrcep"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txnumero"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcomplemento"]),0, Convert.ToString(reader["txbairro"]),0);
                }
                reader.Close();
                session.Close();

                return empresas;
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
                Query query = session.CreateQuery("DELETE FROM Empresas_Emails WHERE idempresa = @id");
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
                Query query = session.CreateQuery("DELETE FROM Empresas_Telefones WHERE idempresa = @id");
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
                Query query = session.CreateQuery("INSERT INTO Empresas_Emails (idempresa, txemail) VALUES (@empresa, @email)");
                query.SetParameter("empresa", id);
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
                Query query = session.CreateQuery("INSERT INTO Empresas_Telefones (idempresa, txtelefone, flwhatsapp) VALUES (@empresa, @telefone, @whatsapp)");
                query.SetParameter("empresa", id);
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

        public List<EmailsEmpresas> ListarEmails(int id = 0)
        {
            try
            {
                List<EmailsEmpresas> list_email = new List<EmailsEmpresas>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Empresas_Emails where idempresa = @id order by txemail");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_email.Add(new EmailsEmpresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txemail"])));
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

        public List<TelefonesEmpresas> ListarTelefones(int id = 0)
        {
            try
            {
                List<TelefonesEmpresas> list_telefone = new List<TelefonesEmpresas>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Empresas_Telefones where idempresa = @id order by txtelefone");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_telefone.Add(new TelefonesEmpresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txtelefone"]), Convert.ToInt32(reader["flwhatsapp"])));
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
    }
}

