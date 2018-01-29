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
    public class Modelos_EmailDB
    {
        public int Salvar(Modelos_Email variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Modelos_Email (idorganizador, txmodelo, txmensagem, idtipo) output INSERTED.idmodelo VALUES (@organizador, @modelo, @mensagem, @tipo)");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("modelo", variavel.txmodelo);
                query.SetParameter("mensagem", variavel.txmensagem);
                query.SetParameter("tipo", variavel.idtipo);
                int ident = query.ExecuteScalar();
                session.Close();
                return ident;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Modelos_Email variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Modelos_Email SET txmodelo = @modelo, txmensagem = @mensagem WHERE idmodelo = @id");
                query.SetParameter("id", variavel.idmodelo);
                query.SetParameter("modelo", variavel.txmodelo);
                query.SetParameter("mensagem", variavel.txmensagem);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Modelos_Email> Listar(int organizador = 0, int tipo = 0)
        {
            try
            {
                List<Modelos_Email> modelos = new List<Modelos_Email>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Modelos_Email where idorganizador = @organizador and idtipo = @tipo ORDER by txmodelo");
                query.SetParameter("organizador", organizador);
                query.SetParameter("tipo", tipo);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    modelos.Add(new Modelos_Email(Convert.ToInt32(reader["idmodelo"]), Convert.ToInt32(reader["idorganizador"]), Convert.ToString(reader["txmodelo"]), Convert.ToString(reader["txmensagem"]), Convert.ToInt32(reader["idtipo"])));
                }
                reader.Close();
                session.Close();

                return modelos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Modelos_Email Buscar(int id)
        {
            try
            {
                Modelos_Email modelo = null;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT * from Modelos_Email where idmodelo = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    modelo = new Modelos_Email(Convert.ToInt32(reader["idmodelo"]), Convert.ToInt32(reader["idorganizador"]), Convert.ToString(reader["txmodelo"]), Convert.ToString(reader["txmensagem"]), Convert.ToInt32(reader["idtipo"]));
                }
                reader.Close();
                session.Close();

                return modelo;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
