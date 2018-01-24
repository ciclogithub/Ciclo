using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Biblioteca.Funcoes;

namespace Biblioteca.DB
{
    public class Painel_CicloDB
    {
       
        public List<Painel_Ciclo> ListarUsuarios(string ini, string fim)
        {
            try
            {
                List<Painel_Ciclo> list_aluno = new List<Painel_Ciclo>();

                DBSession session = new DBSession();
                string qry = "";

                qry = "select u.txusuario, u.txemail, u.dtcadastro, (SUBSTRING((select ', ' + txtelefone from Usuarios_Telefones where idusuario = u.idusuario FOR XML PATH('')), 3, 9999)) as telefones from usuarios u where u.dtcadastro between '" + ini + " 00:00:00' and '" + fim + " 23:59:59' order by u.dtcadastro";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Painel_Ciclo(Convert.ToString(reader["txusuario"]), Convert.ToString(reader["txemail"]), Convert.ToDateTime(reader["dtcadastro"]), Convert.ToString(reader["telefones"])));
                }
                reader.Close();
                session.Close();

                return list_aluno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Painel_Ciclo> ListarOrganizadores(string ini, string fim)
        {
            try
            {
                List<Painel_Ciclo> list_aluno = new List<Painel_Ciclo>();

                DBSession session = new DBSession();
                string qry = "";

                qry = "select O.txorganizador, O.txemail, O.txtelefone, O.dtcadastro, (select COUNT(*) from Cursos where idorganizador = O.idorganizador) as cursos, (select COUNT(*) from Instrutores where idorganizador = O.idorganizador) as instrutores, (select COUNT(*) from Temas where idorganizador = O.idorganizador) as temas, (select COUNT(*) from Locais where idorganizador = O.idorganizador) as locais, (select COUNT(*) from Empresas where idorganizador = O.idorganizador) as empresas,(select COUNT(*) from Alunos where idorganizador = O.idorganizador) as alunos from Organizadores O where o.dtcadastro between '" + ini + " 00:00:00' and '" + fim + " 23:59:59' order by O.dtcadastro";
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_aluno.Add(new Painel_Ciclo(Convert.ToString(reader["txorganizador"]), Convert.ToString(reader["txemail"]), Convert.ToDateTime(reader["dtcadastro"]), Convert.ToString(reader["txtelefone"]), Convert.ToInt32(reader["cursos"]), Convert.ToInt32(reader["instrutores"]), Convert.ToInt32(reader["temas"]), Convert.ToInt32(reader["locais"]), Convert.ToInt32(reader["empresas"]), Convert.ToInt32(reader["alunos"])));
                }
                reader.Close();
                session.Close();

                return list_aluno;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}

