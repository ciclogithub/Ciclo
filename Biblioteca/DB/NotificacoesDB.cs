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
    public class NotificacoesDB
    {
        public void Salvar(Notificacoes variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Usuarios_Notificacoes (idusuario, idespecialidade, idcategoria, idlocalidade) VALUES (@id, @especialidade, @categoria, @localidade)");
                query.SetParameter("id", variavel.idusuario);
                query.SetParameter("especialidade", variavel.idespecialidade);
                query.SetParameter("categoria", variavel.idcategoria);
                query.SetParameter("localidade", variavel.idlocalidade);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Notificacoes variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Usuarios_Notificacoes SET idespecialidade = @especialidade, idcategoria = @categoria, idlocalidade = @localidade WHERE idusuario = @id");
                query.SetParameter("id", variavel.idusuario);
                query.SetParameter("especialidade", variavel.idespecialidade);
                query.SetParameter("categoria", variavel.idcategoria);
                query.SetParameter("localidade", variavel.idlocalidade);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public Notificacoes Buscar(int id)
        {
            try
            {
                Notificacoes notificacao = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Usuarios_Notificacoes WHERE idusuario = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    notificacao = new Notificacoes(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["idespecialidade"]), Convert.ToString(reader["idcategoria"]), Convert.ToString(reader["idlocalidade"]));
                }
                reader.Close();
                session.Close();

                return notificacao;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        
    }
}
