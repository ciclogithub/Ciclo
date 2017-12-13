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

        public void Alterar(Altera_Senha variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Log_Altera_Senha set flativo = 0 where txcodigo = @codigo");
                query.SetParameter("codigo", variavel.codigo);
                query.ExecuteUpdate();
                session.Close();

                if (variavel.idperfil == 2)
                {
                    DBSession session2 = new DBSession();
                    Query query2 = session2.CreateQuery("UPDATE Usuarios set txsenha = @senha where idusuario = @usuario");
                    query2.SetParameter("senha", variavel.senha);
                    query2.SetParameter("usuario", variavel.idusuario);
                    query2.ExecuteUpdate();
                    session2.Close();
                }
                else
                {
                    DBSession session2 = new DBSession();
                    Query query2 = session2.CreateQuery("UPDATE Organizadores set txsenha = @senha where idorganizador = @usuario");
                    query2.SetParameter("senha", variavel.senha);
                    query2.SetParameter("usuario", variavel.idusuario);
                    query2.ExecuteUpdate();
                    session2.Close();
                }
                
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public Altera_Senha Buscar(string codigo)
        {
            try
            {
                Altera_Senha senha = new Altera_Senha();
                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT * from Log_Altera_Senha where txcodigo = @codigo");
                query.SetParameter("codigo", codigo);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    senha.ativo = Convert.ToByte(reader["flativo"]);
                    senha.codigo = Convert.ToString(reader["txcodigo"]);
                    senha.data = Convert.ToDateTime(reader["dtsolicitacao"]);
                    senha.idperfil = Convert.ToInt32(reader["idperfil"]);
                    senha.idusuario = Convert.ToInt32(reader["idusuario"]);
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
