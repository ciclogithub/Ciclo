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
    public class Cursos_DatasDB
    {
        public void Salvar(Cursos_Datas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Cursos_Datas (idcurso, dtcurso, hrinicial, hrfinal, txobs) VALUES (@id, @data, @horaini, @horafim, @obs)");
                query.SetParameter("id", variavel.idcurso);
                query.SetParameter("data", variavel.dtcurso);
                query.SetParameter("horaini", variavel.hrinicial);
                query.SetParameter("horafim", variavel.hrfinal);
                query.SetParameter("obs", variavel.txobs);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Cursos_Datas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Cursos_Datas SET dtcurso = @data, hrinicial = @horaini, hrfinal = @horafim, txobs = @obs WHERE iddatacurso = @id");
                query.SetParameter("id", variavel.iddatacurso);
                query.SetParameter("data", variavel.dtcurso);
                query.SetParameter("horaini", variavel.hrinicial);
                query.SetParameter("horafim", variavel.hrfinal);
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
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Cursos_Datas WHERE iddatacurso = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Cursos_Datas> Listar(int id = 0)
        {
            try
            {
                List<Cursos_Datas> list_datas = new List<Cursos_Datas>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select *, convert(varchar, dtcurso, 103) as data from Cursos_Datas WHERE idcurso = @id ORDER by dtcurso");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_datas.Add(new Cursos_Datas(Convert.ToInt32(reader["iddatacurso"]), Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["data"]), Convert.ToString(reader["hrinicial"]), Convert.ToString(reader["hrfinal"]), Convert.ToString(reader["txobs"])));
                }
                reader.Close();
                session.Close();

                return list_datas;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Cursos_Datas> Listar(int id = 0, string data = "")
        {
            try
            {
                List<Cursos_Datas> list_datas = new List<Cursos_Datas>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select *, convert(varchar, dtcurso, 103) as data from Cursos_Datas WHERE idcurso = @id and dtcurso = @data ORDER by dtcurso");
                query.SetParameter("id", id);
                query.SetParameter("data", data);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_datas.Add(new Cursos_Datas(Convert.ToInt32(reader["iddatacurso"]), Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["data"]), Convert.ToString(reader["hrinicial"]), Convert.ToString(reader["hrfinal"]), Convert.ToString(reader["txobs"])));
                }
                reader.Close();
                session.Close();

                return list_datas;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Cursos_Datas Buscar(int id)
        {
            try
            {
                Cursos_Datas datas = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT *, convert(varchar, dtcurso, 103) as data FROM Cursos_Datas WHERE iddatacurso = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    datas = new Cursos_Datas(Convert.ToInt32(reader["iddatacurso"]), Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["data"]), Convert.ToString(reader["hrinicial"]), Convert.ToString(reader["hrfinal"]), Convert.ToString(reader["txobs"]));
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
