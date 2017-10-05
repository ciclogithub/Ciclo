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
    }
}
