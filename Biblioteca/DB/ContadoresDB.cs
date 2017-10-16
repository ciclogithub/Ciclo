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
        public Contadores Listar()
        {
            try
            {
                Contadores contador = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("select (select count(*) from Instrutores) instrutor, (select count(*) from cursos) cursos, (select count(*) from alunos) alunos, (0) avaliacaoes");
                IDataReader reader = quey.ExecuteQuery();

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
