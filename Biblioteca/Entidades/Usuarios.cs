using Biblioteca.DB;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Usuarios
    {
        public int idusuario { get; set; }
        public string txusuario { get; set; }
        public string txemail { get; set; }
        public string txsenhaaluno { get; set; }
        public string txcpf { get; set; }
        public string txnovasenha { get; set; }
        public string txempresa { get; set; }
        public List<Telefones> txtelefone { get; set; }
        public List<Redes> txredes { get; set; }
        public List<Mercados> txmercado { get; set; }
        public int idcidade { get; set; }
        public int idestado { get; set; }
        public int idpais { get; set; }

        public Usuarios()
        {
            this.idusuario = 0;
            this.txusuario = "";
            this.txemail = "";
            this.txsenhaaluno = "";
            this.txcpf = "";
            this.txnovasenha = "";
            this.txempresa = "";
            this.idcidade = 0;
            this.idestado = 0;
            this.idpais = 0;
        }

        public Usuarios(int id, string usuario, string email, string senha, string cpf)
        {
            this.idusuario = id;
            this.txusuario = usuario;
            this.txemail = email;
            this.txsenhaaluno = MD5Hash.CalculaHash(senha);
            this.txcpf = cpf;
        }

        public Usuarios(int id, string usuario, string email, string cpf, string empresa, int cidade)
        {
            this.idusuario = id;
            this.txusuario = usuario;
            this.txemail = email;
            this.txcpf = cpf;
            this.txempresa = empresa;
            this.idcidade = cidade;
            if(cidade > 0)
            {
                this.idestado = new EstadosDB().Buscar(cidade);
                this.idpais = new PaisesDB().Buscar(this.idestado);
            }
            else
            {
                this.idestado = 0;
                this.idpais = 26;
            }
            this.txtelefone = new UsuariosDB().ListarTelefones(id);
            this.txredes = new UsuariosDB().ListarRedesSociais(id);
            this.txmercado = new UsuariosDB().ListarMercados(id);
        }

        public Usuarios(string usuario)
        {
            this.txusuario = usuario;
        }

        public int Salvar()
        {
            int ident = new UsuariosDB().Salvar(this);
            return ident;
        }

        public void Alterar()
        {
            new UsuariosDB().Alterar(this);
        }

        public void AlteraSenha()
        {
            new UsuariosDB().AlteraSenha(this);
        }        

        public void Excluir()
        {
            new UsuariosDB().Excluir(this);
        }

        public void GravaAluno(int usuario = 0, int aluno = 0)
        {
            new UsuariosDB().GravaAluno(usuario, aluno);
        }

        public Usuarios Retornar()
        {
            Usuarios usuario = new Usuarios();
            usuario.idusuario = this.idusuario;
            usuario.txusuario = this.txusuario;
            usuario.txemail = this.txemail;
            usuario.txsenhaaluno = this.txsenhaaluno;
            usuario.txcpf = this.txcpf;

            return usuario;
        }

        public Usuarios Atualizar(Usuarios usuarios)
        {
            if (this.txusuario == null)
                this.txusuario = "";

            if (this.txemail == null)
                this.txemail = "";

            if (this.txsenhaaluno == null)
                this.txsenhaaluno = "";

            if (this.txcpf == null)
                this.txcpf = "";

            usuarios.txusuario = this.txusuario;
            usuarios.txemail = this.txemail;
            usuarios.txsenhaaluno = this.txsenhaaluno;
            usuarios.txcpf = this.txcpf;

            return usuarios;
        }
    }
}
