using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;

namespace Biblioteca.DB
{
    public class EmpresasDB
    {

        public Empresas Email(string email)
        {
            try
            {
                Empresas empresas = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT isnull(idempresa, 0) AS idempresa, txempresa, txemail, txsenha, txtelefone, txdescritivo FROM Empresas WHERE txemail = @email");
                quey.SetParameter("email", email);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    empresas = new Empresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]));
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

        public Empresas Buscar(int codigo)
        {
            try
            {
                Empresas empresas = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT isnull(idempresa, 0) AS idempresa, txempresa, txemail, txsenha, txtelefone, txdescritivo FROM Empresas WHERE idempresa = @codigo");
                quey.SetParameter("codigo", codigo);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    empresas = new Empresas(Convert.ToInt32(reader["idempresa"]), Convert.ToString(reader["txempresa"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]));
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

        public bool ExisteEmail(string email)
        {
            try
            {
                bool empresa = false;

                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idempresa FROM Empresas WHERE txemail = @email");
                query.SetParameter("email", email);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    empresa = true;
                }
                reader.Close();
                session.Close();

                return empresa;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void Salvar(Empresas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Empresas (txempresa, txemail, txsenha, txtelefone, txdescritivo) VALUES (@empresa, @email, @senha, @telefone, @descritivo) ");
                query.SetParameter("empresa", variavel.txempresa);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("senha", variavel.txsenha);
                query.SetParameter("telefone", variavel.txtelefone);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.ExecuteUpdate();
                session.Close();
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
                Query query = session.CreateQuery("UPDATE Empresas SET txempresa = @empresa, txemail = @email, txsenha = @senha, txtelefone = @telefone, txdescritivo = @descritivo WHERE idempresa = @id");
                query.SetParameter("id", variavel.idempresa);
                query.SetParameter("empresa", variavel.txempresa);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("senha", variavel.txsenha);
                query.SetParameter("telefone", variavel.txtelefone);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Excluir(Empresas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Empresas WHERE idempresa = @id");
                query.SetParameter("id", variavel.idempresa);
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
