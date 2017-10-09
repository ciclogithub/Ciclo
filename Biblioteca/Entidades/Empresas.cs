using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Empresas
    {
        public int idempresa { get; set; }
        public string txempresa { get; set; }
        public string txemail { get; set; }
        public string txsenha { get; set; }
        public string txtelefone { get; set; }
        public string txdescritivo { get; set; }

        public Empresas(Empresas empresas)
        {
            this.idempresa = empresas.idempresa;
            this.txempresa = empresas.txempresa;
            this.txemail = empresas.txemail;
            this.txsenha = empresas.txsenha;
            this.txtelefone = empresas.txtelefone;
            this.txdescritivo = empresas.txdescritivo;
        }

        public Empresas()
        {
            this.idempresa = 0;
            this.txempresa = "";
            this.txemail = "";
            this.txsenha = "";
            this.txtelefone = "";
            this.txdescritivo = "";
        }

        public Empresas(int id, string empresa, string email, string senha, string telefone, string descritivo)
        {
            this.idempresa = id;
            this.txempresa = empresa;
            this.txemail = email;
            this.txsenha = senha;
            this.txtelefone = telefone;
            this.txdescritivo = descritivo;
        }

        public void Salvar()
        {
            new EmpresasDB().Salvar(this);
        }

        public void Alterar()
        {
            new EmpresasDB().Alterar(this);
        }

        public void Excluir()
        {
            new EmpresasDB().Excluir(this);
        }

        public Empresas Retornar()
        {
            Empresas empresa = new Empresas();
            empresa.idempresa = this.idempresa;
            empresa.txempresa = this.txempresa;
            empresa.txemail = this.txemail;
            empresa.txsenha = this.txsenha;
            empresa.txtelefone = this.txtelefone;
            empresa.txdescritivo = this.txdescritivo;

            return empresa;
        }

        public Empresas Atualizar(Empresas empresas)
        {
            if (this.txempresa == null)
                this.txempresa = "";

            if (this.txemail == null)
                this.txemail = "";

            if (this.txsenha == null)
                this.txsenha = "";

            if (this.txtelefone == null)
                this.txtelefone = "";

            if (this.txdescritivo == null)
                this.txdescritivo = "";

            empresas.txempresa = this.txempresa;
            empresas.txemail = this.txemail;
            empresas.txsenha = this.txsenha;
            empresas.txtelefone = this.txtelefone;
            empresas.txdescritivo = this.txdescritivo;

            return empresas;
        }
    }
}
