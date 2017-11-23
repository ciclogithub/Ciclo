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
        public int idaluno { get; set; }
        public string txaluno { get; set; }
        public string txemail { get; set; }
        public string txsenha { get; set; }
        public string txcpf { get; set; }
        public string txnovasenha { get; set; }

        public Usuarios()
        {
            this.idaluno = 0;
            this.txaluno = "";
            this.txemail = "";
            this.txsenha = "";
            this.txcpf = "";
            this.txnovasenha = "";
        }

        public Usuarios(int id, string aluno, string email, string senha, string cpf)
        {
            this.idaluno = id;
            this.txaluno = aluno;
            this.txemail = email;
            this.txsenha = MD5Hash.CalculaHash(senha);
            this.txcpf = cpf;
        }

        public Usuarios(string usuario)
        {
            this.txaluno = usuario;
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

        public Usuarios Retornar()
        {
            Usuarios usuario = new Usuarios();
            usuario.idaluno = this.idaluno;
            usuario.txaluno = this.txaluno;
            usuario.txemail = this.txemail;
            usuario.txsenha = this.txsenha;
            usuario.txcpf = this.txcpf;

            return usuario;
        }

        public Usuarios Atualizar(Usuarios usuarios)
        {
            if (this.txaluno == null)
                this.txaluno = "";

            if (this.txemail == null)
                this.txemail = "";

            if (this.txsenha == null)
                this.txsenha = "";

            if (this.txcpf == null)
                this.txcpf = "";

            usuarios.txaluno = this.txaluno;
            usuarios.txemail = this.txemail;
            usuarios.txsenha = this.txsenha;
            usuarios.txcpf = this.txcpf;

            return usuarios;
        }
    }
}
