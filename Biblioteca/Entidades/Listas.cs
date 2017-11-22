using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Biblioteca.Entidades
{
    public class Listas
    {
        public string nome { get; set; }
        public string cpf_cnpj { get; set; }
        public string emails { get; set; }
        public string telefones { get; set; }

        public Listas()
        {
            this.nome = "";
            this.cpf_cnpj = "";
            this.emails = "";
            this.telefones = "";
        }

        public Listas(int ident, string nome, string cpf_cnpj, string emails, string telefones)
        {
            this.nome = nome;
            this.cpf_cnpj = cpf_cnpj;
            this.emails = emails;
            this.telefones = telefones;
        }

    }
}
