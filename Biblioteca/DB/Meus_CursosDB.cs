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
    public class Meus_CursosDB
    {
        public List<Meus_Cursos> ListarCursosAtivos(int id = 0)
        {
            try
            {
                List<Meus_Cursos> list = new List<Meus_Cursos>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select distinct c.txcurso, l.txlocal, cd.dtcurso, c.idcurso, (select top 1 c1.dtcurso from Cursos_Datas c1 where c1.idcurso = c.idcurso order by c1.dtcurso) as dtcurso from Usuarios_Alunos ua INNER JOIN Cursos_Alunos ca ON ca.idaluno = ua.idaluno inner join Cursos c on c.idcurso = ca.idcurso INNER JOIN Cursos_Datas cd on cd.idcurso = c.idcurso AND cd.dtcurso >= getdate() LEFT JOIN Locais l on l.idlocal = c.idlocal WHERE ua.idusuario = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), Convert.ToDateTime(reader["dtcurso"]), null, null, ""));
                }
                reader.Close();
                session.Close();

                return list;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Meus_Cursos> ListarCursosFinalizados(int id = 0)
        {
            try
            {
                List<Meus_Cursos> list = new List<Meus_Cursos>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select distinct c.txcurso, l.txlocal, cd.dtcurso, c.idcurso, (select top 1 c1.dtcurso from Cursos_Datas c1 where c1.idcurso = c.idcurso order by c1.dtcurso) as dtcurso from Usuarios_Alunos ua INNER JOIN Cursos_Alunos ca ON ca.idaluno = ua.idaluno inner join Cursos c on c.idcurso = ca.idcurso INNER JOIN Cursos_Datas cd on cd.idcurso = c.idcurso AND cd.dtcurso < getdate() LEFT JOIN Locais l on l.idlocal = c.idlocal WHERE ua.idusuario = @id");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), Convert.ToDateTime(reader["dtcurso"]), null, null, ""));
                }
                reader.Close();
                session.Close();

                return list;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Meus_Cursos> ListarInscricoesPendentes(int id = 0)
        {
            try
            {
                List<Meus_Cursos> list = new List<Meus_Cursos>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select c.idcurso, c.txcurso, (select top 1 c1.dtcurso from Cursos_Datas c1 where c1.idcurso = c.idcurso order by c1.dtcurso) as dtcurso, i.dtinscricao, ISNULL(i.dtstatus,'') as dtstatus, i.txmotivo, l.txlocal from Inscricoes i inner join Cursos c on c.idcurso = i.idcurso LEFT JOIN Locais l on l.idlocal = c.idlocal where i.idstatus = 0 and i.idusuario = @id order by i.dtinscricao, c.txcurso");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), Convert.ToDateTime(reader["dtcurso"]), Convert.ToDateTime(reader["dtinscricao"]), Convert.ToDateTime(reader["dtstatus"]), ""));
                }
                reader.Close();
                session.Close();

                return list;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Meus_Cursos> ListarInscricoesRecusadas(int id = 0)
        {
            try
            {
                List<Meus_Cursos> list = new List<Meus_Cursos>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select c.idcurso, c.txcurso, (select top 1 c1.dtcurso from Cursos_Datas c1 where c1.idcurso = c.idcurso order by c1.dtcurso) as dtcurso, i.dtinscricao, i.dtstatus, i.txmotivo, l.txlocal from Inscricoes i inner join Cursos c on c.idcurso = i.idcurso LEFT JOIN Locais l on l.idlocal = c.idlocal where i.idstatus = 2 and i.idusuario = @id order by i.dtinscricao, c.txcurso"); query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), Convert.ToDateTime(reader["dtcurso"]), Convert.ToDateTime(reader["dtinscricao"]), Convert.ToDateTime(reader["dtstatus"]), Convert.ToString(reader["txmotivo"])));
                }
                reader.Close();
                session.Close();

                return list;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
