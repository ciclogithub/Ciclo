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
                Query query = session.CreateQuery("select (select count(*) from Organizadores_Instrutores where idorganizador = @organizador) instrutor, (select count(*) from cursos) cursos, (select count(*) from alunos) alunos, (0) avaliacaoes");
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
