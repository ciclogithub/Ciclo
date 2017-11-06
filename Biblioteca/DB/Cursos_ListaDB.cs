﻿using System;
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
        public List<Cursos_Lista> Listar(int id = 0)
        {
            try
            {
                List<Cursos_Lista> list_cursos = new List<Cursos_Lista>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select top 10 c.txcurso, (select count(ci.idinstrutor) from Cursos_Instrutores ci where ci.idcurso = c.idcurso) as instrutores, (select count(ca.idaluno) from Cursos_Alunos ca where ca.idcurso = c.idcurso) as Alunos, isnull(c.nrmaximo,0) as nrmaximo, c.idcurso, isnull(c.idcor,0) as idcor from cursos c INNER JOIN Cursos_Datas cd on cd.idcurso = c.idcurso AND cd.dtcurso >= getdate()  where c.idorganizador = @organizador order by c.txcurso");
                query.SetParameter("organizador", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_cursos.Add(new Cursos_Lista(Convert.ToString(reader["txcurso"]), Convert.ToInt32(reader["instrutores"]), Convert.ToInt32(reader["Alunos"]), Convert.ToInt32(reader["nrmaximo"]), Convert.ToInt32(reader["idcurso"]), Convert.ToInt32(reader["idcor"])));
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
