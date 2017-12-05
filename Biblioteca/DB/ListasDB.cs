using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.DB
{
    public class ListasDB
    {

        public List<Listas> ListarAlunosSemProdutos()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select a.txaluno, a.txcpf, SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES from alunos a where a.idorganizador = " + cookie.Value + " and isnull((case when A.txcpf = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = REPLACE(REPLACE(A.txcpf,'.',''),'-','')) end), 0) = 1 order by a.txaluno";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Listas> ListarAlunosComProdutos()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select a.txaluno, a.txcpf, SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES from alunos a where a.idorganizador = " + cookie.Value + " and isnull((case when A.txcpf = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = REPLACE(REPLACE(A.txcpf,'.',''),'-','')) end), 0) = 2 order by a.txaluno";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Listas> ListarAlunosSemDados()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select a.txaluno, a.txcpf, SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES from alunos a where a.idorganizador = " + cookie.Value + " AND a.idaluno NOT IN(SELECT IDALUNO FROM ALUNOS_EMAILS) AND a.txcpf = '' order by a.txaluno";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Listas> ListarAlunosNaoCadastrados()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select a.txaluno, a.txcpf, SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES from alunos a where a.idorganizador = " + cookie.Value + " and (a.txcpf <> '' or a.idaluno in (select idaluno from alunos_emails)) and isnull((case when A.txcpf = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = REPLACE(REPLACE(A.txcpf,'.',''),'-','')) end), 0) = 0 order by a.txaluno";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txaluno"]), Convert.ToString(reader["txcpf"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Listas> ListarEmpresasSemProdutos()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select em.txempresa, em.txcnpj, SUBSTRING((SELECT ', ' + TXEMAIL FROM empresas_EMAILS WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM empresas_TELEFONES WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS TELEFONES from empresas em where em.idorganizador = " + cookie.Value + " and isnull((case when em.txcnpj = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = REPLACE(REPLACE(em.txcnpj,'.',''),'-','')) end), 0) = 1 order by em.txempresa";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Listas> ListarEmpresasComProdutos()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select em.txempresa, em.txcnpj, SUBSTRING((SELECT ', ' + TXEMAIL FROM empresas_EMAILS WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM empresas_TELEFONES WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS TELEFONES from empresas em where em.idorganizador = " + cookie.Value + " and isnull((case when em.txcnpj = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = REPLACE(REPLACE(em.txcnpj,'.',''),'-','')) end), 0) = 2 order by em.txempresa";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Listas> ListarEmpresasSemDados()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select em.txempresa, em.txcnpj, SUBSTRING((SELECT ', ' + TXEMAIL FROM empresas_EMAILS WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM empresas_TELEFONES WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS TELEFONES from empresas em where em.idorganizador = " + cookie.Value + " and em.idempresa NOT IN(SELECT idempresa FROM empresas_EMAILS) AND em.txcnpj = '' order by em.txempresa";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Listas> ListarEmpresasNaoCadastradas()
        {
            try
            {
                List<Listas> list_relat = new List<Listas>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "select em.txempresa, em.txcnpj, SUBSTRING((SELECT ', ' + TXEMAIL FROM empresas_EMAILS WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS EMAILS, SUBSTRING((SELECT ', ' + TXTELEFONE FROM empresas_TELEFONES WHERE idempresa = em.idempresa FOR XML PATH('')),3,999) AS TELEFONES from empresas em where em.idorganizador = " + cookie.Value + " and (em.txcnpj <> '' or em.idempresa in (select idempresa from empresas_emails)) and isnull((case when em.txcnpj = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = REPLACE(REPLACE(em.txcnpj,'.',''),'-','')) end), 0) = 0 and em.idempresa IN(SELECT idempresa FROM empresas_EMAILS) order by em.txempresa";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Listas(1, Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txcnpj"]), Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
