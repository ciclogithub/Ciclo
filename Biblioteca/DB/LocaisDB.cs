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

        public string ListarNomes(string id)
        {
            try
            {
                string result = "";
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select SUBSTRING((select ', ' + txlocal from locais where idlocal in (" + id + ") FOR XML PATH('')),3,9999) as result");
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


            public List<Locais> Listar()
        {
            try
            {
                List<Locais> list_local = new List<Locais>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select *, c.txcidade, e.txestado, p.txpais from Locais l left join cidades c on c.idcidade = l.idcidade left join estados e on e.idestado = c.idestado left join paises p on p.idpais = e.idpais WHERE l.idorganizador = @idorganizador ORDER by l.txlocal");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_local.Add(new Locais(Convert.ToInt32(reader["idlocal"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txestado"]), Convert.ToString(reader["txpais"]), 0));
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

        public List<Locais> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Locais> list_local = new List<Locais>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, l.*, c.txcidade, e.txestado, p.txpais from Locais l left join cidades c on c.idcidade = l.idcidade left join estados e on e.idestado = c.idestado left join paises p on p.idpais = e.idpais WHERE l.idorganizador = @idorganizador ORDER by l.txlocal OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_local.Add(new Locais(Convert.ToInt32(reader["idlocal"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txestado"]), Convert.ToString(reader["txpais"]), Convert.ToInt32(reader["total"])));
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

        public List<Locais> Listar(string local, int page = 1, int regs = 10)
        {
            try
            {
                List<Locais> list_local = new List<Locais>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, l.*, c.txcidade, e.txestado, p.txpais from Locais l left join cidades c on c.idcidade = l.idcidade left join  estados e on e.idestado = c.idestado left join paises p on p.idpais = e.idpais WHERE (l.txlocal LIKE @local OR l.txlogradouro LIKE @local OR c.txcidade LIKE @local OR e.txestado LIKE @local) AND l.idorganizador = @idorganizador ORDER by l.txlocal OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("local", "%" + local.Replace(" ", "%") + "%");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_local.Add(new Locais(Convert.ToInt32(reader["idlocal"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txestado"]), Convert.ToString(reader["txpais"]), Convert.ToInt32(reader["total"])));
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
                Query quey = session.CreateQuery("SELECT l.*, c.txcidade, e.txestado, p.txpais FROM Locais l left join cidades c on c.idcidade = l.idcidade left join estados e on e.idestado = c.idestado left join paises p on p.idpais = e.idpais WHERE l.idlocal = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    alunos = new Locais(Convert.ToInt32(reader["idlocal"]), Convert.ToInt32(reader["idcidade"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txestado"]), Convert.ToString(reader["txpais"]), 0);
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

        public int VerificaLocais(int id, string nome)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from locais where idorganizador = @idorganizador and idlocal <> @id and txlocal COLLATE Latin1_general_CI_AI = @nome COLLATE Latin1_general_CI_AI");
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

        public int VerificaLocaisExcluir(string ids)
        {
            try
            {
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select top 1 * from cursos where idlocal in (" + ids + ")");
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
