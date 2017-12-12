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
    public class Altera_SenhaDB
    {
        public void Salvar(Altera_Senha variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Log_Altera_Senha (idperfil, idusuario, dtsolicitacao, txcodigo, flativo) VALUES (@perfil, @usuario, @data, @codigo, @ativo)");
                query.SetParameter("perfil", variavel.idperfil);
                query.SetParameter("usuario", variavel.idusuario);
                query.SetParameter("data", variavel.data);
                query.SetParameter("codigo", variavel.codigo);
                query.SetParameter("ativo", variavel.ativo);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Altera_Senha> Buscar(string codigo)
        {
            try
            {
                List<Altera_Senha> senha = new List<Altera_Senha>();
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT * from Log_Altera_Senha where txcodigo = @codigo");
                query.SetParameter("codigo", codigo);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    senha.Add(new Altera_Senha(Convert.ToInt32(reader["idperfil"]), Convert.ToInt32(reader["idusuario"]), Convert.ToDateTime(reader["dtsolicitacao"]), Convert.ToString(reader["txcodigo"]), Convert.ToByte(reader["flativo"])));
                }
                reader.Close();
                session.Close();

                return senha;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
