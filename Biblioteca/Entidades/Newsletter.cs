using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Newsletter
    {
        public int idnewsletter { get; set; }
        public string txnome { get; set; }
        public string txcurso { get; set; }
        public string txemail { get; set; }
        public string txwhatsapp { get; set; }
        public string txcidade { get; set; }
        public string txestado { get; set; }

        public Newsletter()
        {
            this.idnewsletter = 0;
            this.txnome = "";
            this.txcurso = "";
            this.txemail = "";
            this.txwhatsapp = "";
            this.txcidade = "";
            this.txestado = "";
        }

        public Newsletter(int id, string nome, string curso, string email, string whatsapp, string cidade, string estado)
        {
            this.idnewsletter = 0;
            this.txnome = nome;
            this.txcurso = curso;
            this.txemail = email;
            this.txwhatsapp = whatsapp;
            this.txcidade = cidade;
            this.txestado = estado;
        }

        public void Salvar()
        {
            new NewsletterDB().Salvar(this);
        }

    }
}
