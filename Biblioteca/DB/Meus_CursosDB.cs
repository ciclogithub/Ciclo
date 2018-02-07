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
                Query query = session.CreateQuery("select distinct c.txcurso, l.txlocal, c.idcurso from Usuarios_Alunos ua INNER JOIN Cursos_Alunos ca ON ca.idaluno = ua.idaluno inner join Cursos c on c.idcurso = ca.idcurso LEFT JOIN Locais l on l.idlocal = c.idlocal WHERE ua.idusuario = @id and(((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso desc) >= GETDATE()) or((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso desc) is null))");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), null, null, "", null));
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
                Query query = session.CreateQuery("select distinct c.txcurso, l.txlocal, c.idcurso, (select top 1 c1.dtcurso from Cursos_Datas c1 where c1.idcurso = c.idcurso order by c1.dtcurso) as dtcurso, ISNULL((SELECT ISNULL(NRNOTA1, 0) + ISNULL(NRNOTA2, 0) + ISNULL(NRNOTA3, 0) + ISNULL(NRNOTA4, 0) + ISNULL(NRNOTA5, 0) FROM CURSOS_AVALIACOES WHERE IDCURSOALUNO = CA.IDCURSOALUNO) ,0) AS AVALIACAO from Usuarios_Alunos ua INNER JOIN Cursos_Alunos ca ON ca.idaluno = ua.idaluno inner join Cursos c on c.idcurso = ca.idcurso LEFT JOIN Locais l on l.idlocal = c.idlocal WHERE ua.idusuario = @id and ((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso desc) < GETDATE())");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), null, null, "", Convert.ToInt32(reader["AVALIACAO"])));
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
                Query query = session.CreateQuery("select c.idcurso, c.txcurso, i.dtinscricao, ISNULL(i.dtstatus,'') as dtstatus, i.txmotivo, l.txlocal from Inscricoes i inner join Cursos c on c.idcurso = i.idcurso LEFT JOIN Locais l on l.idlocal = c.idlocal where i.idstatus = 0 and i.idusuario = @id order by i.dtinscricao, c.txcurso");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), Convert.ToDateTime(reader["dtinscricao"]), Convert.ToDateTime(reader["dtstatus"]), "", null));
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
                Query query = session.CreateQuery("select c.idcurso, c.txcurso, i.dtinscricao, i.dtstatus, i.txmotivo, l.txlocal from Inscricoes i inner join Cursos c on c.idcurso = i.idcurso LEFT JOIN Locais l on l.idlocal = c.idlocal where i.idstatus = 2 and i.idusuario = @id order by i.dtinscricao, c.txcurso"); query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list.Add(new Meus_Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txlocal"]), Convert.ToDateTime(reader["dtinscricao"]), Convert.ToDateTime(reader["dtstatus"]), Convert.ToString(reader["txmotivo"]), null));
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
