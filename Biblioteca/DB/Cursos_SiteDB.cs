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
    public class Cursos_SiteDB
    {
        public List<Cursos_Site> Listar()
        {
            try
            {
                List<Cursos_Site> list_cursos = new List<Cursos_Site>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select TOP 3 c.txdescritivo, c.idcurso, c.txcurso, ct.txcategoria, SUBSTRING((SELECT ', ' + i.txinstrutor + '|' + cast(i.idinstrutor as varchar) FROM Cursos_Instrutores ci inner join instrutores i on i.idinstrutor = ci.idinstrutor WHERE ci.idcurso = c.idcurso FOR XML PATH('')), 3, 999) as instrutores, c.txfoto, ci.txcidade, es.txsigla from cursos c left join categorias ct on ct.idcategoria = c.idcategoria left join Locais l on l.idlocal = c.idlocal left join Cidades ci on ci.idcidade = l.idcidade left join Estados es on es.idestado = ci.idestado where 1 = 1 and(((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) >= GETDATE()) or((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) is null)) ORDER by c.idcurso desc");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos_Site(0, Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txcategoria"]), Convert.ToString(reader["instrutores"]), Convert.ToString(reader["txfoto"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txsigla"]), Convert.ToString(reader["txdescritivo"])));
                }
                reader.Close();
                session.Close();

                return list_cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Cursos_Site> Ultimos()
        {
            try
            {
                List<Cursos_Site> list_cursos = new List<Cursos_Site>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select TOP 5 c.txdescritivo, c.idcurso, c.txcurso, ct.txcategoria, SUBSTRING((SELECT ', ' + i.txinstrutor + '|' + cast(i.idinstrutor as varchar) FROM Cursos_Instrutores ci inner join instrutores i on i.idinstrutor = ci.idinstrutor WHERE ci.idcurso = c.idcurso FOR XML PATH('')), 3, 999) as instrutores, c.txfoto, ci.txcidade, es.txsigla from cursos c left join categorias ct on ct.idcategoria = c.idcategoria left join Locais l on l.idlocal = c.idlocal left join Cidades ci on ci.idcidade = l.idcidade left join Estados es on es.idestado = ci.idestado LEFT JOIN Cursos_Datas cd on cd.idcurso = c.idcurso where 1 = 1 and(((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) >= GETDATE()) or((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) is null)) order by idcurso desc");
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos_Site(0, Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txcategoria"]), Convert.ToString(reader["instrutores"]), Convert.ToString(reader["txfoto"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txsigla"]), Convert.ToString(reader["txdescritivo"])));
                }
                reader.Close();
                session.Close();

                return list_cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Cursos_Site> ListarPesquisa(string curso = "", string cidade = "", string data = "", int page = 1, int regs = 12)
        {
            try
            {
                List<Cursos_Site> list_cursos = new List<Cursos_Site>();
                string qry = "";
                var d = data.Split('-');
                DBSession session = new DBSession();
                qry = "select c.txdescritivo, c.idcurso, c.txcurso, ct.txcategoria, SUBSTRING((SELECT ', ' + i.txinstrutor + '|' + cast(i.idinstrutor as varchar) FROM Cursos_Instrutores ci inner join instrutores i on i.idinstrutor = ci.idinstrutor WHERE ci.idcurso = c.idcurso FOR XML PATH('')), 3, 999) as instrutores, c.txfoto, ci.txcidade, es.txsigla ";
                qry += "from cursos c ";
                qry += "left join categorias ct on ct.idcategoria = c.idcategoria ";
                qry += "left join Locais l on l.idlocal = c.idlocal ";
                qry += "left join Cidades ci on ci.idcidade = l.idcidade ";
                qry += "left join Estados es on es.idestado = ci.idestado ";
                qry += "LEFT JOIN Cursos_Datas cd on cd.idcurso = c.idcurso ";
                qry += "where 1 = 1 ";

                if (curso != "") { qry += " and c.txcurso like '%" + curso.Replace(" ","%") + "%' "; }
                if (cidade != "") { qry += " and ci.txcidade like '%" + cidade.Replace(" ", "%")  + "%' "; }
                if (data == "")
                {
                    //qry += " AND cd.dtcurso >= getdate() ";
                    qry += " and(((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) >= GETDATE()) or((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) is null)) ";
                }
                else
                {
                    //qry += " AND cd.dtcurso between '" + d[0].Trim() + "' and '" + d[1].Trim() + "' ";
                    qry += " and(((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) '" + d[0].Trim() + "' and '" + d[1].Trim() + "') or((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso) is null)) ";
                }
                qry += "ORDER by cd.dtcurso ";
                qry += "OFFSET " + regs + " * (" + page + " - 1) ROWS FETCH NEXT " + regs + " ROWS ONLY";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos_Site(0, Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txcategoria"]), Convert.ToString(reader["instrutores"]), Convert.ToString(reader["txfoto"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txsigla"]), Convert.ToString(reader["txdescritivo"])));
                }
                reader.Close();
                session.Close();

                return list_cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
