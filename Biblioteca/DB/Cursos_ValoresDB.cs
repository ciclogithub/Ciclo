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
    public class Cursos_ValoresDB
    {
        public void Salvar(Cursos_Valores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Cursos_Valores (idcurso, nrvalor, dtvalor) VALUES (@id, @valor, @data)");
                query.SetParameter("id", variavel.idcurso);
                query.SetParameter("valor", variavel.nrvalor);
                query.SetParameter("data", variavel.dtvalor);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Cursos_Valores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Cursos_Valores SET nrvalor = @valor, dtvalor = @data WHERE idvalorcurso = @id");
                query.SetParameter("id", variavel.idvalorcurso);
                query.SetParameter("valor", variavel.nrvalor);
                query.SetParameter("data", variavel.dtvalor);
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
                Query query = session.CreateQuery("DELETE FROM Cursos_Valores WHERE idvalorcurso = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Cursos_Valores> Listar(int id = 0)
        {
            try
            {
                List<Cursos_Valores> list_valores = new List<Cursos_Valores>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select *, convert(varchar, dtvalor, 103) as data from Cursos_Valores WHERE idcurso = @id ORDER by dtvalor");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_valores.Add(new Cursos_Valores(Convert.ToInt32(reader["idvalorcurso"]), Convert.ToInt32(reader["idcurso"]), Convert.ToDouble(reader["nrvalor"]), Convert.ToString(reader["data"])));
                }
                reader.Close();
                session.Close();

                return list_valores;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Cursos_Valores Buscar(int id)
        {
            try
            {
                Cursos_Valores datas = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT *, convert(varchar, dtvalor, 103) as data FROM Cursos_Valores WHERE idvalorcurso = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    datas = new Cursos_Valores(Convert.ToInt32(reader["idvalorcurso"]), Convert.ToInt32(reader["idcurso"]), Convert.ToDouble(reader["nrvalor"]), Convert.ToString(reader["data"]));
                }
                reader.Close();
                session.Close();

                return datas;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
