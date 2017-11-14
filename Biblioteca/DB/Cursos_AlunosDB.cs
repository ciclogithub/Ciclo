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
                Query quey = session.CreateQuery("SELECT A.TXALUNO, SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAIL, C.TXCURSO, CV.DTAVALIACAO FROM CURSOS_ALUNOS CA LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO LEFT JOIN CURSOS C ON C.IDCURSO = CA.IDCURSO LEFT JOIN CURSOS_AVALIACOES CV ON CV.IDCURSOALUNO = CA.IDCURSOALUNO WHERE CA.IDCURSOALUNO = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    alunos = new Cursos_Alunos(id, Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["EMAIL"]), Convert.ToString(reader["TXCURSO"]), Convert.ToString(reader["DTAVALIACAO"]));
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
