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
    public class RelatoriosDB
    {

        public List<Relatorios> Listar()
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT C.TXCURSO, CD.DTCURSO, A.TXALUNO, A.TXEMPRESA, CO1.TXCOR AS CORCURSO, CO2.TXCOR AS CORALUNO, T.TXTEMA, I.TXINSTRUTOR, E.TXESPECIALIDADE, C1.TXCIDADE AS CIDADECURSO, C2.TXCIDADE AS CIDADEALUNO, E1.TXESTADO AS ESTADOCURSO, E2.TXESTADO AS ESTADOALUNO, L.TXLOCAL, CT.TXCATEGORIA FROM CURSOS C LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = @idorganizador LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA WHERE C.IDORGANIZADOR = @idorganizador ORDER BY C.TXCURSO");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(Convert.ToString(reader["TXCURSO"]), Convert.ToString(reader["DTCURSO"]), Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["TXEMPRESA"]), Convert.ToString(reader["CORCURSO"]), Convert.ToString(reader["CORALUNO"]), Convert.ToString(reader["TXTEMA"]), Convert.ToString(reader["TXINSTRUTOR"]), Convert.ToString(reader["TXESPECIALIDADE"]), Convert.ToString(reader["CIDADECURSO"]), Convert.ToString(reader["ESTADOCURSO"]), Convert.ToString(reader["CIDADEALUNO"]), Convert.ToString(reader["ESTADOALUNO"]), Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"])));
                }
                reader.Close();
                session.Close();

                return list_relat;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
       
    }
}
