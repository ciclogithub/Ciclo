using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Organizadores
    {
        public int idorganizador { get; set; }
        public string txorganizador { get; set; }
        public string txemail { get; set; }
        public string txsenha { get; set; }
        public string txtelefone { get; set; }
        public string txdescritivo { get; set; }
        public Byte flinstrutor { get; set; }

        public Organizadores(Organizadores organizadores)
        {
            this.idorganizador = organizadores.idorganizador;
            this.txorganizador = organizadores.txorganizador;
            this.txemail = organizadores.txemail;
            this.txsenha = organizadores.txsenha;
            this.txtelefone = organizadores.txtelefone;
            this.txdescritivo = organizadores.txdescritivo;
            this.flinstrutor = organizadores.flinstrutor;
        }

        public Organizadores()
        {
            this.idorganizador = 0;
            this.txorganizador = "";
            this.txemail = "";
            this.txsenha = "";
            this.txtelefone = "";
            this.txdescritivo = "";
            this.flinstrutor = 0;
        }

        public Organizadores(int id, string organizador, string email, string senha, string telefone, string descritivo)
        {
            this.idorganizador = id;
            this.txorganizador = organizador;
            this.txemail = email;
            this.txsenha = senha;
            this.txtelefone = telefone;
            this.txdescritivo = descritivo;
        }

        public void Salvar()
        {
            new OrganizadoresDB().Salvar(this);
        }

        public void Alterar()
        {
            new OrganizadoresDB().Alterar(this);
        }

        public void Excluir()
        {
            new OrganizadoresDB().Excluir(this);
        }

        public Organizadores Retornar()
        {
            Organizadores empresa = new Organizadores();
            empresa.idorganizador = this.idorganizador;
            empresa.txorganizador = this.txorganizador;
            empresa.txemail = this.txemail;
            empresa.txsenha = this.txsenha;
            empresa.txtelefone = this.txtelefone;
            empresa.txdescritivo = this.txdescritivo;

            return empresa;
        }

        public Organizadores Atualizar(Organizadores organizadores)
        {
            if (this.txorganizador == null)
                this.txorganizador = "";

            if (this.txemail == null)
                this.txemail = "";

            if (this.txsenha == null)
                this.txsenha = "";

            if (this.txtelefone == null)
                this.txtelefone = "";

            if (this.txdescritivo == null)
                this.txdescritivo = "";

            organizadores.txorganizador = this.txorganizador;
            organizadores.txemail = this.txemail;
            organizadores.txsenha = this.txsenha;
            organizadores.txtelefone = this.txtelefone;
            organizadores.txdescritivo = this.txdescritivo;

            return organizadores;
        }
    }
}
