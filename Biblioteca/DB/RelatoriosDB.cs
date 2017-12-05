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
    public class RelatoriosDB
    {

        public List<Relatorios> ListarAluno(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT distinct CO2.TXCOR AS CORALUNO, A.TXALUNO, E.TXEMPRESA, C2.TXCIDADE AS CIDADEALUNO, ";
                qry += "E2.TXESTADO AS ESTADOALUNO, C.TXCURSO, L.TXLOCAL, CT.TXCATEGORIA, T.TXTEMA, CO1.TXCOR AS CORCURSO, C.TXIDENTIFICADOR, ";
                qry += "ISNULL((SELECT ISNULL(NRNOTA1, 0) + ISNULL(NRNOTA2, 0) + ISNULL(NRNOTA3, 0) + ISNULL(NRNOTA4, 0) + ISNULL(NRNOTA5, 0) FROM CURSOS_AVALIACOES WHERE IDCURSOALUNO = CA.IDCURSOALUNO),0) AS AVALIACAO, ";
                qry += "SUBSTRING((SELECT ', ' + M.TXMERCADO FROM ALUNOS_MERCADOS AM LEFT JOIN MERCADOS M ON M.IDMERCADO = AM.IDMERCADO WHERE AM.IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS MERCADOS, ";
                qry += "SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, ";
                qry += "SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES, ";
                qry += "SUBSTRING((SELECT ', ' + I.TXINSTRUTOR FROM CURSOS_INSTRUTORES CI LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR WHERE CI.IDCURSO = C.IDCURSO FOR XML PATH('')),3,999) AS INSTRUTORES ";
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
                qry += "LEFT JOIN EMPRESAS E ON E.IDEMPRESA = A.IDEMPRESA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",","','") + "')) "; }
                qry += "ORDER BY A.TXALUNO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(1, Convert.ToString(reader["TXCURSO"]), "", Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["TXEMPRESA"]), Convert.ToString(reader["CORCURSO"]), Convert.ToString(reader["CORALUNO"]), Convert.ToString(reader["TXTEMA"]), Convert.ToString(reader["INSTRUTORES"]), Convert.ToString(reader["MERCADOS"]), "", "", Convert.ToString(reader["CIDADEALUNO"]), Convert.ToString(reader["ESTADOALUNO"]), Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"]), Convert.ToString(reader["TXIDENTIFICADOR"]), 0, Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"]), Convert.ToDouble(reader["AVALIACAO"]),0));
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

        public List<Relatorios> ListarCategoria(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT T.TXTEMA, C.TXIDENTIFICADOR, C.TXCURSO, ";
                qry += "(SELECT COUNT(IDALUNO) FROM CURSOS_ALUNOS WHERE IDCURSO = C.IDCURSO) AS ALUNOS, L.TXLOCAL, ";
                qry += "    CO1.TXCOR AS CORCURSO, CT.TXCATEGORIA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY CT.TXCATEGORIA, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(2, Convert.ToString(reader["TXCURSO"]), "", "", "", Convert.ToString(reader["CORCURSO"]), "", Convert.ToString(reader["TXTEMA"]), "", "", "", "", "", "", Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"]), Convert.ToString(reader["TXIDENTIFICADOR"]), Convert.ToInt32(reader["ALUNOS"]),"","",0,0));
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

        public List<Relatorios> ListarClassificacaoAluno(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT CO2.TXCOR AS CORALUNO, A.TXALUNO, E.TXEMPRESA, C2.TXCIDADE AS CIDADEALUNO, E2.TXESTADO AS ESTADOALUNO, ";
                qry += "SUBSTRING((SELECT ', ' + M.TXMERCADO FROM ALUNOS_MERCADOS AM LEFT JOIN MERCADOS M ON M.IDMERCADO = AM.IDMERCADO WHERE AM.IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS MERCADOS, ";
                qry += "SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, ";
                qry += "SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES ";
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
                qry += "LEFT JOIN EMPRESAS E ON E.IDEMPRESA = A.IDEMPRESA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY CO2.TXCOR, A.TXALUNO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(3, "", "", Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["TXEMPRESA"]), "", Convert.ToString(reader["CORALUNO"]), "", "", Convert.ToString(reader["MERCADOS"]), "", "", Convert.ToString(reader["CIDADEALUNO"]), Convert.ToString(reader["ESTADOALUNO"]), "", "", "", 0, Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"]), 0,0));
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

        public List<Relatorios> ListarClassificacaoCurso(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT T.TXTEMA, C.TXIDENTIFICADOR, C.TXCURSO, ";
                qry += "(SELECT COUNT(IDALUNO) FROM CURSOS_ALUNOS WHERE IDCURSO = C.IDCURSO) AS ALUNOS, L.TXLOCAL, ";
                qry += "    CO1.TXCOR AS CORCURSO, CT.TXCATEGORIA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY CO1.TXCOR, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(4, Convert.ToString(reader["TXCURSO"]), "", "", "", Convert.ToString(reader["CORCURSO"]), "", Convert.ToString(reader["TXTEMA"]), "", "", "", "", "", "", Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"]), Convert.ToString(reader["TXIDENTIFICADOR"]), Convert.ToInt32(reader["ALUNOS"]), "", "", 0,0));
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

        public List<Relatorios> ListarCurso(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();
                qry = "SELECT distinct C.TXCURSO, CO1.TXCOR AS CORCURSO, C.TXIDENTIFICADOR, L.TXLOCAL, CT.TXCATEGORIA, A.TXALUNO, E.TXEMPRESA, CO2.TXCOR AS CORALUNO, T.TXTEMA, ";
                qry += "SUBSTRING((SELECT ', ' + M.TXMERCADO FROM ALUNOS_MERCADOS AM LEFT JOIN MERCADOS M ON M.IDMERCADO = AM.IDMERCADO WHERE AM.IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS MERCADOS, ";
                qry += "C1.TXCIDADE AS CIDADECURSO, C2.TXCIDADE AS CIDADEALUNO, E1.TXESTADO AS ESTADOCURSO, E2.TXESTADO AS ESTADOALUNO, ";
                qry += "ISNULL((SELECT ISNULL(NRNOTA1, 0) + ISNULL(NRNOTA2, 0) + ISNULL(NRNOTA3, 0) + ISNULL(NRNOTA4, 0) + ISNULL(NRNOTA5, 0) FROM CURSOS_AVALIACOES WHERE IDCURSOALUNO = CA.IDCURSOALUNO),0) AS AVALIACAO, ";
                qry += "ISNULL((SELECT ISNULL(SUM(CAST(ISNULL(CAV.NRNOTA1, 0) + ISNULL(CAV.NRNOTA2, 0) + ISNULL(CAV.NRNOTA3, 0) + ISNULL(CAV.NRNOTA4, 0) + ISNULL(CAV.NRNOTA5, 0) AS FLOAT) / 5) / COUNT(CAV.IDCURSOALUNO),0) FROM CURSOS_AVALIACOES CAV WHERE CAV.IDCURSOALUNO IN(SELECT IDCURSOALUNO FROM Cursos_Alunos CA WHERE CA.IDCURSO = C.IDCURSO)),0) AS AVALIACAOGERAL, ";
                qry += "SUBSTRING((SELECT ', ' + I.TXINSTRUTOR FROM CURSOS_INSTRUTORES CI LEFT JOIN INSTRUTORES I ON I.IDINSTRUTOR = CI.IDINSTRUTOR WHERE CI.IDCURSO = C.IDCURSO FOR XML PATH('')),3,999) AS INSTRUTORES ";
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
                qry += "LEFT JOIN EMPRESAS E ON E.IDEMPRESA = A.IDEMPRESA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY C.TXCURSO, A.TXALUNO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(5, Convert.ToString(reader["TXCURSO"]), "", Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["TXEMPRESA"]), Convert.ToString(reader["CORCURSO"]), Convert.ToString(reader["CORALUNO"]), Convert.ToString(reader["TXTEMA"]), Convert.ToString(reader["INSTRUTORES"]), Convert.ToString(reader["MERCADOS"]), Convert.ToString(reader["CIDADECURSO"]), Convert.ToString(reader["ESTADOCURSO"]), Convert.ToString(reader["CIDADEALUNO"]), Convert.ToString(reader["ESTADOALUNO"]), Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"]), Convert.ToString(reader["TXIDENTIFICADOR"]), 0, "", "", Convert.ToDouble(reader["AVALIACAO"]), Convert.ToDouble(reader["AVALIACAOGERAL"])));
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

        public List<Relatorios> ListarMercado(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT M.TXMERCADO, CO2.TXCOR AS CORALUNO, A.TXALUNO, E.TXEMPRESA, C2.TXCIDADE AS CIDADEALUNO, E2.TXESTADO AS ESTADOALUNO, ";
                qry += "SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, ";
                qry += "SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES ";
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
                qry += "LEFT JOIN EMPRESAS E ON E.IDEMPRESA = A.IDEMPRESA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY M.TXMERCADO, A.TXALUNO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(6, "", "", Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["TXEMPRESA"]), "", Convert.ToString(reader["CORALUNO"]), "", "", Convert.ToString(reader["TXMERCADO"]), "", "", Convert.ToString(reader["CIDADEALUNO"]), Convert.ToString(reader["ESTADOALUNO"]), "", "", "", 0, Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"]), 0,0));
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

        public List<Relatorios> ListarLocal(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT T.TXTEMA, C.TXIDENTIFICADOR, C.TXCURSO, ";
                qry += "(SELECT COUNT(IDALUNO) FROM CURSOS_ALUNOS WHERE IDCURSO = C.IDCURSO) AS ALUNOS, L.TXLOCAL, ";
                qry += "    CO1.TXCOR AS CORCURSO, CT.TXCATEGORIA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY L.TXLOCAL, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(7, Convert.ToString(reader["TXCURSO"]), "", "", "", Convert.ToString(reader["CORCURSO"]), "", Convert.ToString(reader["TXTEMA"]), "", "", "", "", "", "", Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"]), Convert.ToString(reader["TXIDENTIFICADOR"]), Convert.ToInt32(reader["ALUNOS"]), "", "", 0,0));
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

        public List<Relatorios> ListarInstrutor(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "select DISTINCT I.TXINSTRUTOR, I.TXEMAIL, I.TXTELEFONE, CO1.TXCOR AS CORCURSO, C.TXCURSO, T.TXTEMA, L.TXLOCAL, CT.TXCATEGORIA, C.TXIDENTIFICADOR, ";
                qry += "ISNULL((SELECT SUM(CAST(ISNULL(CAV.nrnota2,0) AS float)) / COUNT(CAV.idcursoaluno) FROM Cursos_Avaliacoes CAV WHERE CAV.idcursoaluno IN (SELECT idcursoaluno FROM Cursos_Alunos WHERE idcurso IN (SELECT CIN.IDCURSO FROM Cursos_Instrutores CIN WHERE CIN.idinstrutor = CI.idinstrutor AND CIN.idcurso = C.IDCURSO ))),0) AS AVALIACAO, ";
                qry += "(SELECT COUNT(IDALUNO) FROM CURSOS_ALUNOS WHERE IDCURSO = C.IDCURSO) AS ALUNOS ";
                qry += "FROM CURSOS C  ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY I.TXINSTRUTOR, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(8, Convert.ToString(reader["TXCURSO"]), "", "", "", Convert.ToString(reader["CORCURSO"]), "", Convert.ToString(reader["TXTEMA"]), Convert.ToString(reader["TXINSTRUTOR"]), "", "", "", "", "", Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"]), Convert.ToString(reader["TXIDENTIFICADOR"]), Convert.ToInt32(reader["ALUNOS"]), Convert.ToString(reader["TXEMAIL"]), Convert.ToString(reader["TXTELEFONE"]), Convert.ToDouble(reader["AVALIACAO"]), 0));
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

        public List<Relatorios> ListarTema(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT T.TXTEMA, C.TXIDENTIFICADOR, C.TXCURSO,  ";
                qry += "(SELECT COUNT(IDALUNO) FROM CURSOS_ALUNOS WHERE IDCURSO = C.IDCURSO) AS ALUNOS, L.TXLOCAL, ";
                qry += "    CO1.TXCOR AS CORCURSO, CT.TXCATEGORIA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY T.TXTEMA, C.TXCURSO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(9, Convert.ToString(reader["TXCURSO"]), "", "", "", Convert.ToString(reader["CORCURSO"]), "", Convert.ToString(reader["TXTEMA"]), "", "", "", "", "", "", Convert.ToString(reader["TXLOCAL"]), Convert.ToString(reader["TXCATEGORIA"]), Convert.ToString(reader["TXIDENTIFICADOR"]), Convert.ToInt32(reader["ALUNOS"]), "", "", 0,0));
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

        public List<Relatorios> ListarLocalidadeAluno(FormCollection collection)
        {
            try
            {
                List<Relatorios> list_relat = new List<Relatorios>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                string qry = "";
                DBSession session = new DBSession();

                qry = "SELECT DISTINCT CONCAT(C2.TXCIDADE,'/',E2.TXSIGLA) AS CIDADEALUNO, CO2.TXCOR, A.TXALUNO, E.TXEMPRESA, C2.TXCIDADE AS CIDADEALUNO, E2.TXESTADO AS ESTADOALUNO, ";
                qry += "SUBSTRING((SELECT ', ' + M.TXMERCADO FROM ALUNOS_MERCADOS AM LEFT JOIN MERCADOS M ON M.IDMERCADO = AM.IDMERCADO WHERE AM.IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS MERCADOS, ";
                qry += "SUBSTRING((SELECT ', ' + TXEMAIL FROM ALUNOS_EMAILS WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS EMAILS, ";
                qry += "SUBSTRING((SELECT ', ' + TXTELEFONE FROM ALUNOS_TELEFONES WHERE IDALUNO = A.IDALUNO FOR XML PATH('')),3,999) AS TELEFONES ";
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
                qry += "LEFT JOIN EMPRESAS E ON E.IDEMPRESA = A.IDEMPRESA ";
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
                if (collection["templocalidade"] != "") { qry += "AND (CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_', A.IDCIDADE) IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '_', C2.IDESTADO, '_') IN ('" + collection["templocalidade"].Replace(",", "','") + "') OR CONCAT(E2.IDPAIS, '__') IN ('" + collection["templocalidade"].Replace(",", "','") + "')) "; }
                qry += "ORDER BY CONCAT(C2.TXCIDADE,'/',E2.TXSIGLA), A.TXALUNO";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_relat.Add(new Relatorios(10, "", "", Convert.ToString(reader["TXALUNO"]), Convert.ToString(reader["TXEMPRESA"]), "", Convert.ToString(reader["TXCOR"]), "", "", Convert.ToString(reader["MERCADOS"]), "", "", Convert.ToString(reader["CIDADEALUNO"]), "", "", "", "", 0, Convert.ToString(reader["EMAILS"]), Convert.ToString(reader["TELEFONES"]), 0, 0));
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
