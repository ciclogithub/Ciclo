using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DB
{

    public class ContadoresDB
    {
        public Contadores Listar(int organizador)
        {
            try
            {
                Contadores contador = null;

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select (select count(*) from Instrutores where idorganizador = @organizador) instrutor, (select count(*) from cursos where idorganizador = @organizador) cursos, (select count(*) from alunos where idorganizador = @organizador) alunos, ISNULL((select (SUM((cast(cv.nrnota1 + cv.nrnota2 + cv.nrnota3 + cv.nrnota4 + cv.nrnota5 as float) / 5)) / count(*)) from cursos c inner join cursos_alunos ca on ca.idcurso = c.idcurso inner join cursos_avaliacoes cv on cv.idcursoaluno = ca.idcursoaluno where c.idorganizador = @organizador),0) avaliacaoes");
                query.SetParameter("organizador", organizador);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    contador = new Contadores(Convert.ToInt32(reader["instrutor"]), Convert.ToInt32(reader["cursos"]), Convert.ToInt32(reader["alunos"]), Convert.ToInt32(reader["avaliacaoes"]));
                }
                reader.Close();
                session.Close();

                return contador;

            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
