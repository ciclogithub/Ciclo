using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Biblioteca.Entidades;

namespace Biblioteca.DB
{
    public class PainelDB
    {
        public Painel Buscar(int codigo)
        {
            try
            {
                Painel painel = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Organizadores WHERE idorganizador = @codigo");
                quey.SetParameter("codigo", codigo);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    painel = new Painel(Convert.ToInt32(reader["idorganizador"]), Convert.ToString(reader["txorganizador"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]));
                }
                reader.Close();
                session.Close();

                return painel;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public bool Buscar(int codigo, string senha)
        {
            try
            {
                bool retorno = false;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idorganizador FROM Organizadores WHERE idorganizador = @codigo AND txsenha = @senha");
                query.SetParameter("codigo", codigo);
                query.SetParameter("senha", senha);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    retorno = true;
                }
                reader.Close();
                session.Close();

                return retorno;

                return retorno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Painel Buscar(string email, string senha)
        {
            try
            {
                Painel painel = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Organizadores WHERE txemail = @email AND txsenha = @senha");
                quey.SetParameter("email", email);
                quey.SetParameter("senha", senha);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    painel = new Painel(Convert.ToInt32(reader["idorganizador"]), Convert.ToString(reader["txorganizador"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]));
                }
                reader.Close();
                session.Close();

                return painel;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
