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
    public class Curso_SiteDB
    {
        public Curso_Site Buscar(int id)
        {
            try
            {
                Curso_Site cursos = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("select c.idcurso, c.txcurso, ca.txcategoria, o.txorganizador, isnull(c.nrminimo,0) as minimo, isnull(c.nrmaximo,0) as maximo, c.txcargahoraria, c.flgratuito, c.txlocal as localdesc, c.txdescritivo, c.txfoto, l.txlocal, l.txlogradouro, ci.txcidade, es.txsigla, p.txpais from Cursos c left join Categorias ca on ca.idcategoria = c.idcategoria left join Organizadores o on o.idorganizador = c.idorganizador left join Locais l on l.idlocal = c.idlocal left join Cidades ci on ci.idcidade = l.idcidade left join Estados es on es.idestado = ci.idestado left join Paises p on p.idpais = es.idpais where c.idcurso = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    cursos = new Curso_Site(Convert.ToInt32(reader["idcurso"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txcategoria"]), Convert.ToString(reader["txorganizador"]), Convert.ToInt32(reader["minimo"]), Convert.ToInt32(reader["maximo"]), Convert.ToString(reader["txcargahoraria"]), Convert.ToByte(reader["flgratuito"]), Convert.ToString(reader["localdesc"]), Convert.ToString(reader["txdescritivo"]), Convert.ToString(reader["txfoto"]), Convert.ToString(reader["txlocal"]), Convert.ToString(reader["txlogradouro"]), Convert.ToString(reader["txcidade"]), Convert.ToString(reader["txsigla"]), Convert.ToString(reader["txpais"]));
                }
                reader.Close();
                session.Close();

                return cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
