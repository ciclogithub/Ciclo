using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Web;

namespace Biblioteca.DB
{
    public class UsuariosDB
    {

        public Usuarios Buscar(string email, string senha)
        {
            try
            {
                Usuarios usuarios = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Usuarios WHERE txemail = @email AND txsenha = @senha");
                quey.SetParameter("email", email);
                quey.SetParameter("senha", senha);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    usuarios = new Usuarios(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["txusuario"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]), Convert.ToString(reader["txcpf"]));
                }
                reader.Close();
                session.Close();

                return usuarios;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Usuarios Email(string email)
        {
            try
            {
                Usuarios usuarios = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Usuarios WHERE txemail = @email");
                quey.SetParameter("email", email);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    usuarios = new Usuarios(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["txusuario"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]), Convert.ToString(reader["txcpf"]));
                }
                reader.Close();
                session.Close();

                return usuarios;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Usuarios Buscar(int codigo = 0)
        {
            try
            {
                Usuarios usuarios = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Usuarios WHERE idusuario = @codigo");
                quey.SetParameter("codigo", codigo);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    usuarios = new Usuarios(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["txusuario"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txsenha"]), Convert.ToString(reader["txcpf"]));
                }
                reader.Close();
                session.Close();

                return usuarios;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public bool ExisteEmail(string email)
        {
            try
            {
                bool usuario = false;

                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idusuario FROM Usuarios WHERE txemail = @email");
                query.SetParameter("email", email);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    usuario = true;
                }
                reader.Close();
                session.Close();

                return usuario;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public int Salvar(Usuarios variavel)
        {
            try
            {

                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Usuarios (txusuario, txemail, txsenha, txcpf) output INSERTED.idusuario VALUES (@aluno, @email, @senha, @cpf) ");
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("senha", variavel.txsenha);
                query.SetParameter("cpf", variavel.txcpf);
                int ident = query.ExecuteScalar();
                session.Close();
                return ident;

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Usuarios variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Usuarios SET txusuario = @aluno, txemail = @email, txsenha = @senha,  txcpf = @cpf WHERE idusuario = @id");
                query.SetParameter("id", variavel.idaluno);
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("senha", variavel.txsenha);
                query.SetParameter("cpf", variavel.txcpf);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void AlteraSenha(Usuarios variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Usuarios SET txsenha = @senha WHERE idusuario = @id");
                query.SetParameter("id", variavel.idaluno);
                query.SetParameter("senha", variavel.txnovasenha);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Excluir(Usuarios variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Usuarios WHERE idusuario = @id");
                query.SetParameter("id", variavel.idaluno);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public Usuarios Nome(int codigo)
        {
            try
            {
                Usuarios usuarios = null;

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select txusuario from usuarios where idusuario = @id");
                query.SetParameter("id", codigo);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    usuarios = new Usuarios(Convert.ToString(reader["txusuario"]));
                }
                reader.Close();
                session.Close();

                return usuarios;

            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
