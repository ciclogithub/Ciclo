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
        public List<Graficos> ListarAluno(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT 'Alunos' as serie,  YEAR(CD.dtcurso) as categoria, COUNT(A.IDALUNO) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += "GROUP BY YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso)";
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

        public List<Graficos> ListarCategoria(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT CT.TXCATEGORIA as serie,  YEAR(CD.dtcurso) as categoria, COUNT(A.IDALUNO) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += "GROUP BY CT.TXCATEGORIA, YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso), CT.TXCATEGORIA";
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

        public List<Graficos> ListarClassificacaoAluno(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry += "SELECT TBL.txcor as serie,TBL.ano as categoria, COUNT(TBL.idaluno) as VALOR FROM( ";
                qry += "SELECT DISTINCT co2.txcor, YEAR(CD.dtcurso) as ano, A.IDALUNO ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += ") AS TBL ";
                qry += "GROUP BY TBL.txcor, TBL.ano ";
                qry += "ORDER BY TBL.ano, TBL.txcor ";
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

        public List<Graficos> ListarClassificacaoCurso(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT co1.txcor as serie,  YEAR(CD.dtcurso) as categoria, COUNT(A.IDALUNO) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += "GROUP BY CO1.TXCOR, YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso), CO1.TXCOR";
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

        public List<Graficos> ListarCurso(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT 'ALUNOS' as serie,  YEAR(CD.dtcurso) as categoria, COUNT(A.IDALUNO) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += "GROUP BY YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso)";
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

        public List<Graficos> ListarMercado(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry += "SELECT TBL.TXMERCADO as serie,TBL.ano as categoria, COUNT(TBL.idaluno) as VALOR FROM( ";
                qry += "SELECT DISTINCT M.TXMERCADO, YEAR(CD.dtcurso) as ano, A.IDALUNO ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN MERCADOS M ON M.IDMERCADO = AM.IDMERCADO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += ") AS TBL ";
                qry += "GROUP BY TBL.TXMERCADO, TBL.ano ";
                qry += "ORDER BY TBL.ano, TBL.TXMERCADO ";
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

        public List<Graficos> ListarLocal(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
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
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
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

        public List<Graficos> ListarInstrutor(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT I.TXINSTRUTOR as serie,  YEAR(CD.dtcurso) as categoria, COUNT(A.IDALUNO) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += "GROUP BY I.TXINSTRUTOR, YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso), I.TXINSTRUTOR ";
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

        public List<Graficos> ListarTema(FormCollection collection)
        {
            try
            {
                List<Graficos> grafico = new List<Graficos>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT T.TXTEMA as serie, YEAR(CD.dtcurso) as categoria, COUNT(L.idlocal) as VALOR ";
                qry += "FROM CURSOS C ";
                qry += "LEFT JOIN CURSOS_DATAS CD ON CD.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN CURSOS_ALUNOS CA ON CA.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN ALUNOS A ON A.IDALUNO = CA.IDALUNO AND A.IDORGANIZADOR = " + cookie.Value + " ";
                qry += "LEFT JOIN CORES CO1 ON CO1.IDCOR = C.IDCOR LEFT JOIN CORES CO2 ON CO2.IDCOR = A.IDCOR ";
                qry += "LEFT JOIN TEMAS T ON T.IDTEMA = C.IDTEMA LEFT JOIN CURSOS_INSTRUTORES CI ON CI.IDCURSO = C.IDCURSO ";
                qry += "LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR ";
                qry += "LEFT JOIN ALUNOS_MERCADOS AM ON AM.IDALUNO = A.IDALUNO ";
                qry += "LEFT JOIN LOCAIS L ON L.IDLOCAL = C.IDLOCAL ";
                qry += "LEFT JOIN CIDADES C1 ON C1.IDCIDADE = L.IDCIDADE ";
                qry += "LEFT JOIN CIDADES C2 ON C2.IDCIDADE = A.IDCIDADE ";
                qry += "LEFT JOIN ESTADOS E1 ON E1.IDESTADO = C1.IDESTADO ";
                qry += "LEFT JOIN ESTADOS E2 ON E2.IDESTADO = C2.IDESTADO ";
                qry += "LEFT JOIN CATEGORIAS CT ON CT.IDCATEGORIA = C.IDCATEGORIA ";
                qry += "WHERE C.IDORGANIZADOR = " + cookie.Value + " ";
                if (collection["tempinstrutor"] != "") { qry += "AND CI.IDINSTRUTOR IN (" + collection["tempinstrutor"] + ") "; }
                if (collection["temptema"] != "") { qry += "AND C.IDTEMA IN (" + collection["temptema"] + ") "; }
                if (collection["tempempresa"] != "") { qry += "AND A.IDEMPRESA IN (" + collection["tempempresa"] + ") "; }
                if (collection["templocal"] != "") { qry += "AND C.IDLOCAL IN (" + collection["templocal"] + ") "; }
                if (collection["tempcorcurso"] != "") { qry += "AND C.IDCOR IN (" + collection["tempcorcurso"] + ") "; }
                if (collection["tempcoraluno"] != "") { qry += "AND A.IDCOR IN (" + collection["tempcoraluno"] + ") "; }
                if (collection["tempcategoria"] != "") { qry += "AND CT.IDCATEGORIA IN (" + collection["tempcategoria"] + ") "; }
                if (collection["tempcurso"] != "") { qry += "AND C.IDCURSO IN (" + collection["tempcurso"] + ") "; }
                if (collection["tempaluno"] != "") { qry += "AND A.IDALUNO IN (" + collection["tempaluno"] + ") "; }
                if (collection["tempmercado"] != "") { qry += "AND AM.IDMERCADO IN (" + collection["tempmercado"] + ") "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] == "")) { qry += "AND CD.DTCURSO >= '" + collection["dtini"] + "' "; }
                if ((collection["dtini"] == "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO <= '" + collection["dtfim"] + "' "; }
                if ((collection["dtini"] != "") && (collection["dtfim"] != "")) { qry += "AND CD.DTCURSO BETWEEN '" + collection["dtini"] + "' AND '" + collection["dtfim"] + "' "; }
                if (collection["dtmes"] != "") { qry += "AND MONTH(CD.DTCURSO) = " + collection["dtmes"] + " "; }
                qry += "GROUP BY T.TXTEMA, YEAR(CD.dtcurso) ";
                qry += "ORDER BY YEAR(CD.dtcurso), T.TXTEMA";
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
    }
}

