using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Empresas
    {
        public int idempresa { get; set; }
        public int idorganizador { get; set; }
        public string txempresa { get; set; }
        public string txcnpj { get; set; }
        public string txcodigo { get; set; }
        public string nrcep { get; set; }
        public int idcidade { get; set; }
        public string txnumero { get; set; }
        public string txlogradouro { get; set; }
        public string txcomplemento { get; set; }
        public List<EmailsEmpresas> txemail { get; set; }
        public List<TelefonesEmpresas> txtelefone { get; set; }
        public int total { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

        public Empresas()
        {
            this.idempresa = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txempresa = "";
            this.txcnpj = "";
            this.txcodigo = "";
            this.nrcep = "";
            this.idcidade = 0;
            this.txnumero = "";
            this.txlogradouro = "";
            this.txcomplemento = "";
            this.total = 0;
        }

        public Empresas(int id, string empresa, string cnpj, string codigo, string cep, int cidade, string numero, string logradouro, string complemento, int total)
        {
            this.idempresa = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txempresa = empresa;
            this.txcnpj = cnpj;
            this.txemail = new EmpresasDB().ListarEmails(id); 
            this.txtelefone = new EmpresasDB().ListarTelefones(id);
            this.txcodigo = codigo;
            this.nrcep = cep;
            this.idcidade = cidade;
            this.txnumero = numero;
            this.txlogradouro = logradouro;
            this.txcomplemento = complemento;
            this.total = total;
        }

        public Empresas(int id, string empresa)
        {
            this.idempresa = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txempresa = empresa;
        }

        public int Salvar()
        {
            int ident = new EmpresasDB().Salvar(this);
            return ident;
        }

        public void Alterar()
        {
            new EmpresasDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new EmpresasDB().Excluir(ident);
        }

        public void RemoverEmails(int id = 0)
        {
            new EmpresasDB().RemoverEmails(id);
        }

        public void RemoverTelefones(int id = 0)
        {
            new EmpresasDB().RemoverTelefones(id);
        }

        public void SalvarEmail(int id = 0, string email = "")
        {
            new EmpresasDB().SalvarEmail(id, email);
        }

        public void SalvarTelefone(int id = 0, string telefone = "", int whatsapp = 0)
        {
            new EmpresasDB().SalvarTelefone(id, telefone, whatsapp);
        }
    }

    public class TelefonesEmpresas
    {
        public int idempresa { get; set; }
        public string txtelefone { get; set; }
        public int flwhatsapp { get; set; }

        public TelefonesEmpresas(int id, string telefone, int whatsapp)
        {
            this.idempresa = id;
            this.txtelefone = telefone;
            this.flwhatsapp = whatsapp;
        }
    }

    public class EmailsEmpresas
    {
        public int idempresa { get; set; }
        public string txemail { get; set; }

        public EmailsEmpresas(int id, string email)
        {
            this.idempresa = id;
            this.txemail = email;
        }
    }
}
