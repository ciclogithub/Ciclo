using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Web;
using Biblioteca.Funcoes;

namespace Biblioteca.DB
{
    public class UsuariosDB
    {

        public void GravaAluno(int usuario = 0, int aluno = 0)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Usuarios_Alunos (idusuario, idaluno) values (@usuario, @aluno)");
                query.SetParameter("usuario", usuario);
                query.SetParameter("aluno", aluno);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

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

        public Usuarios BuscarCompleto(int codigo = 0)
        {
            try
            {
                Usuarios usuarios = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT *, ISNULL(idcidade,0) AS cidade FROM Usuarios WHERE idusuario = @codigo");
                quey.SetParameter("codigo", codigo);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    usuarios = new Usuarios(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["txusuario"]), Convert.ToString(reader["txemail"]), Convert.ToString(reader["txcpf"]), Convert.ToString(reader["txempresa"]), Convert.ToInt32(reader["cidade"]));
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

        public int BuscarCodigoAluno(int usuario = 0)
        {
            try
            {
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Usuarios_Alunos where idusuario = @id");                
                query.SetParameter("id", usuario);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    result = Convert.ToInt32(reader["idaluno"]);
                }
                reader.Close();
                session.Close();

                return result;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public int BuscarAlunoPorCpf(string cpf)
        {
            try
            {
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select top 1 idaluno from Alunos where txcpf = @cpf");
                query.SetParameter("cpf", cpf);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    result = Convert.ToInt32(reader["idaluno"]);
                }
                reader.Close();
                session.Close();

                return result;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public int BuscarAlunoPorEmail(string email)
        {
            try
            {
                int result = 0;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select top 1 idaluno from Alunos_Emails where txemail = @email");
                query.SetParameter("email", email);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    result = Convert.ToInt32(reader["idaluno"]);
                }
                reader.Close();
                session.Close();

                return result;
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

        public bool ExisteCPF(string cpf)
        {
            try
            {
                bool usuario = false;

                DBSession session = new DBSession();
                Query query = session.CreateQuery("SELECT idusuario FROM Usuarios WHERE txcpf = @cpf");
                query.SetParameter("cpf", cpf);
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
                Query query = session.CreateQuery("INSERT INTO Usuarios (txusuario, txemail, txsenha, txcpf) output INSERTED.idusuario VALUES (@usuario, @email, @senha, @cpf) ");
                query.SetParameter("usuario", variavel.txusuario);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("senha", variavel.txsenhaaluno);
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
                Query query = session.CreateQuery("UPDATE Usuarios SET txusuario = @usuario, txcpf = @cpf, idcidade = @cidade, txempresa = @empresa WHERE idusuario = @id");
                query.SetParameter("id", variavel.idusuario);
                query.SetParameter("usuario", variavel.txusuario);
                query.SetParameter("cpf", variavel.txcpf);
                query.SetParameter("cidade", variavel.idcidade);
                query.SetParameter("empresa", variavel.txempresa);
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
                query.SetParameter("id", variavel.idusuario);
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
                query.SetParameter("id", variavel.idusuario);
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

        public List<Mercados> ListarMercados(int id = 0)
        {
            try
            {
                List<Mercados> list_mercados = new List<Mercados>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select m.idmercado, m.txmercado from Usuarios_Mercados am left join mercados m on m.idmercado = am.idmercado where am.idusuario = @id order by m.txmercado");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_mercados.Add(new Mercados(Convert.ToInt32(reader["idmercado"]), Convert.ToString(reader["txmercado"]), 0));
                }
                reader.Close();
                session.Close();

                return list_mercados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Telefones> ListarTelefones(int id = 0)
        {
            try
            {
                List<Telefones> list_telefone = new List<Telefones>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Usuarios_Telefones where idusuario = @id order by txtelefone");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_telefone.Add(new Telefones(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["txtelefone"]), Convert.ToInt32(reader["flwhatsapp"])));
                }
                reader.Close();
                session.Close();

                return list_telefone;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Redes> ListarRedesSociais(int id = 0)
        {
            try
            {
                List<Redes> list_redes = new List<Redes>();

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select AR.*, RS.txicone from Usuarios_RedesSociais AR Inner Join RedesSociais RS on RS.idredesocial = AR.idredesocial where AR.idusuario = @id order by RS.txredesocial");
                query.SetParameter("id", id);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_redes.Add(new Redes(Convert.ToInt32(reader["idusuario"]), Convert.ToString(reader["txredesocial"]), Convert.ToString(reader["txicone"]), Convert.ToInt32(reader["idredesocial"])));
                }
                reader.Close();
                session.Close();

                return list_redes;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RemoverMercados(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Usuarios_Mercados WHERE idusuario = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void RemoverTelefones(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Usuarios_Telefones WHERE idusuario = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void RemoverRedesSociais(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Usuarios_RedesSociais WHERE idusuario = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarMercado(int id, int mercado)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Usuarios_Mercados (idusuario, idmercado) VALUES (@usuario, @mercado)");
                query.SetParameter("usuario", id);
                query.SetParameter("mercado", mercado);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarTelefone(int id, string telefone, int whatsapp)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Usuarios_Telefones (idusuario, txtelefone, flwhatsapp) VALUES (@usuario, @telefone, @whatsapp)");
                query.SetParameter("usuario", id);
                query.SetParameter("telefone", telefone);
                query.SetParameter("whatsapp", whatsapp);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void SalvarRedeSocial(int id, string rede, int codigo)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Usuarios_RedesSociais (idusuario, idredesocial, txredesocial) VALUES (@usuario, @codigo, @rede)");
                query.SetParameter("usuario", id);
                query.SetParameter("codigo", codigo);
                query.SetParameter("rede", rede);
                query.ExecuteUpdate();
                session.Close();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

    }        
}
