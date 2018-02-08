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
    public class Cursos_ListaDB
    {
        public List<Cursos> Listar(int id = 0)
        {
            try
            {
                List<Cursos> list_cursos = new List<Cursos>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select c.idcurso, c.txcurso, isnull(c.idcategoria,0) as idcategoria, isnull(c.flgratuito,0) as flgratuito, isnull(c.idcor,0) as idcor from cursos c where c.idorganizador = @organizador and (((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso desc) >= GETDATE()) or((select top 1 dtcurso from Cursos_Datas cc where cc.idcurso = c.idcurso order by cc.dtcurso desc) is null)) order by (select max(dtcurso) from Cursos_Datas where idcurso = c.idcurso), c.txcurso");
                query.SetParameter("organizador", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {

                    list_cursos.Add(new Cursos(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["idcategoria"]), Convert.ToBoolean(reader["flgratuito"]), Convert.ToInt32(reader["idcor"]), 0));
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
