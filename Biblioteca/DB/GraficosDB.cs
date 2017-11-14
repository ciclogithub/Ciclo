using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.DB
{
    public class GraficosDB
    {
        public List<Graficos> ListarAluno(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Graficos> ListarCategoria(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Graficos> ListarClassificacaoAluno(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Graficos> ListarClassificacaoCurso(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Graficos> ListarCurso(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "GROUP BY L.txlocal, YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso), L.TXLOCAL";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Graficos> ListarEspecialidade(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Graficos> ListarLocal(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL as serie, YEAR(CD.dtcurso) as categoria, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "GROUP BY L.txlocal, YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso), L.TXLOCAL ";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                { 
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["categoria"]), Convert.ToString(reader["serie"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
}

        public List<Graficos> ListarInstrutor(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Graficos> ListarTema(int id)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT L.TXLOCAL, YEAR(CD.dtcurso) as ANOS, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ESPECIALIDADES E ON E.IDESPECIALIDADE = A.IDESPECIALIDADE ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    grafico.Add(new Graficos(Convert.ToInt32(reader["VALOR"]), Convert.ToString(reader["ANOS"]), Convert.ToString(reader["TXLOCAL"])));
                }
                reader.Close();
                session.Close();

                return grafico;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

