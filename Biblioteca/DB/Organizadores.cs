using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;

namespace Biblioteca.DB
{
    public class OrganizadoresDB
    {

        public Organizadores Email(string email)
        {
            try
            {
                Organizadores organizadores = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT isnull(idorganizador, 0) AS idorganizador, txorganizador, txemail, txsenha, txtelefone, txdescritivo FROM Organizadores WHERE txemail = @email");
                quey.SetParameter("email", email);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    organizadores = new Organizadores(Convert.ToInt32(reader["idorganizador"]), Convert.ToString(reader["txorganizador"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]));
                }
                reader.Close();
                session.Close();

                return organizadores;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Organizadores Buscar(int codigo)
        {
            try
            {
                Organizadores organizadores = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT isnull(idorganizador, 0) AS idorganizador, txorganizador, txemail, txsenha, txtelefone, txdescritivo FROM Organizadores WHERE idorganizador = @codigo");
                quey.SetParameter("codigo", codigo);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    organizadores = new Organizadores(Convert.ToInt32(reader["idorganizador"]), Convert.ToString(reader["txorganizador"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]), Convert.ToString(reader["txtelefone"]), Convert.ToString(reader["txdescritivo"]));
                }
                reader.Close();
                session.Close();

                return organizadores;
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
                Query query = session.CreateQuery("SELECT idorganizador FROM Organizadores WHERE txemail = @email");
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

        public void Salvar(Organizadores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Organizadores (txorganizador, txemail, txsenha, txtelefone, txdescritivo) VALUES (@organizador, @email, @senha, @telefone, @descritivo) ");
                query.SetParameter("organizador", variavel.txorganizador);
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

        public void Alterar(Organizadores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Organizadores SET txorganizador = @organizador, txemail = @email, txsenha = @senha, txtelefone = @telefone, txdescritivo = @descritivo WHERE idorganizador = @id");
                query.SetParameter("id", variavel.idorganizador);
                query.SetParameter("organizador", variavel.txorganizador);
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

        public void Excluir(Organizadores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Organizadores WHERE idorganizador = @id");
                query.SetParameter("id", variavel.idorganizador);
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
