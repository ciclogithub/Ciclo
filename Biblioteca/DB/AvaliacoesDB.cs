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
    public class AvaliacoesDB
    {
        public List<Avaliacoes> Listar(int codigo = 0)
        {
            try
            {
                List<Avaliacoes> list_avaliacao = new List<Avaliacoes>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT A.TXALUNO, CA.idcursoaluno, CV.dtavaliacao, ISNULL(CV.nrnota1, 0) AS nrnota1, ISNULL(CV.nrnota2, 0) AS nrnota2, ISNULL(CV.nrnota3, 0) AS nrnota3, ISNULL(CV.nrnota4, 0) AS nrnota4, ISNULL(CV.nrnota5, 0) AS nrnota5, Cv.txobservacao, CA.txavaliacao FROM Cursos_Alunos CA LEFT JOIN Alunos A ON A.idaluno = CA.idaluno LEFT JOIN Cursos_Avaliacoes CV ON CV.idcursoaluno = CA.idcursoaluno WHERE CA.idcurso = @codigo ORDER BY A.TXALUNO");
                query.SetParameter("codigo", codigo);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_avaliacao.Add(new Avaliacoes(Convert.ToInt32(reader["idcursoaluno"]), Convert.ToString(reader["txaluno"]), Convert.ToString(reader["dtavaliacao"]), Convert.ToInt32(reader["nrnota1"]), Convert.ToInt32(reader["nrnota2"]), Convert.ToInt32(reader["nrnota3"]), Convert.ToInt32(reader["nrnota4"]), Convert.ToInt32(reader["nrnota5"]), Convert.ToString(reader["txobservacao"]), Convert.ToString(reader["txavaliacao"])));
                }
                reader.Close();
                session.Close();

                return list_avaliacao;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        
    }
}

