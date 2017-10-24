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
    public class LocaisDB
    {
        public void Salvar(Locais variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Locais (idorganizador, idcidade, txlocal, txlogradouro) VALUES (@organizador, @cidade, @local, @logradouro)");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("cidade", variavel.idcidade);
                query.SetParameter("local", variavel.txlocal);
                query.SetParameter("logradouro", variavel.txlogradouro);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Locais variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Locais SET idcidade = @cidade, txlocal = @local, txlogradouro = @logradouro WHERE idlocal = @id");
                query.SetParameter("id", variavel.idlocal);
                query.SetParameter("cidade", variavel.idcidade);
                query.SetParameter("local", variavel.txlocal);
                query.SetParameter("logradouro", variavel.txlogradouro);
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
                Query query = session.CreateQuery("DELETE FROM Locais WHERE idlocal = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Locais> Listar()
        {
            try
            {
                List<Locais> list_local = new List<Locais>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select l.*, c.txcidade, e.txestado from Locais l inner join cidades c on c.idcidade = l.idcidade inner join  estados e on e.idestado = c.idestado WHERE l.idorganizador = @idorganizador ORDER by l.txlocal");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_local.Add(new Locais(Convert.ToInt32(reader["idlocal"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txestado"])));
                }
                reader.Close();
                session.Close();

                return list_local;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Locais> Listar(string local)
        {
            try
            {
                List<Locais> list_local = new List<Locais>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select l.*, c.txcidade, e.txestado from Locais l inner join cidades c on c.idcidade = l.idcidade inner join  estados e on e.idestado = c.idestado WHERE (l.txlocal LIKE @local OR l.txlogradouro LIKE @local) AND l.idorganizador = @idorganizador ORDER by l.txlocal");
                query.SetParameter("local", "%" + local.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_local.Add(new Locais(Convert.ToInt32(reader["idlocal"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txestado"])));
                }
                reader.Close();
                session.Close();

                return list_local;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Locais Buscar(int id)
        {
            try
            {
                Locais alunos = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT l.*, c.txcidade, e.txestado FROM Locais l inner join cidades c on c.idcidade = l.idcidade inner join  estados e on e.idestado = c.idestado WHERE l.idlocal = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    alunos = new Locais(Convert.ToInt32(reader["idlocal"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txestado"]));
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
    }
}
