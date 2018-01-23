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
    public class CursosAlunosDB
    {

        public Cursos_Alunos Buscar(int id)
        {
            try
            {
                Cursos_Alunos alunos = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT A.TXALUNO, SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAIL, SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES, C.TXCURSO, CV.DTAVALIACAO FROM CURSOS_ALUNOS CA LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO LEFT JOIN CURSOS C ON C.IDCURSO = CA.IDCURSO LEFT JOIN CURSOS_AVALIACOES CV ON CV.IDCURSOALUNO = CA.IDCURSOALUNO WHERE CA.IDCURSOALUNO = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    alunos = new Cursos_Alunos(id, Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["EMAIL"]), Convert.ToString(reader["TXCURSO"]), Convert.ToString(reader["DTAVALIACAO"]), Convert.ToString(reader["TELEFONES"]));
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

        public List<Cursos_Alunos> ListarDoCurso(int id = 0)
        {
            try
            {
                List<Cursos_Alunos> alunos = new List<Cursos_Alunos>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select A.TXALUNO, SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAIL, SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES from CURSOS_ALUNOS ca INNER JOIN Alunos A ON A.idaluno = ca.idaluno where ca.idcurso = @id ORDER BY A.TXALUNO");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    alunos.Add(new Cursos_Alunos(id, Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["EMAIL"]), "", "", Convert.ToString(reader["TELEFONES"])));
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
