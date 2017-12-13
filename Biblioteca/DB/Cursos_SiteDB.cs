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
        public List<Cursos_Site> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Cursos_Site> list_cursos = new List<Cursos_Site>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, c.idcurso, c.txcurso, ct.txcategoria, e.txespecialidade, SUBSTRING((SELECT ', ' + i.txinstrutor + '|' + cast(i.idinstrutor as varchar) FROM Cursos_Instrutores ci inner join instrutores i on i.idinstrutor = ci.idinstrutor WHERE ci.idcurso = c.idcurso FOR XML PATH('')), 3, 999) as instrutores, c.txfoto, ci.txcidade, es.txsigla from cursos c left join categorias ct on ct.idcategoria = c.idcategoria left join especialidades e on e.idespecialidade = c.idespecialidade left join Locais l on l.idlocal = c.idlocal left join Cidades ci on ci.idcidade = l.idcidade left join Estados es on es.idestado = ci.idestado INNER JOIN Cursos_Datas cd on cd.idcurso = c.idcurso AND cd.dtcurso >= getdate() where 1 = 1 ORDER by cd.dtcurso OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos_Site(Convert.ToInt32(reader["total"]), Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txcategoria"]), Convert.ToString(reader["txespecialidade"]), Convert.ToString(reader["instrutores"]), Convert.ToString(reader["txfoto"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txsigla"])));
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
