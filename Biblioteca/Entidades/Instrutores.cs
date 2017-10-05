using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Instrutores
    {
        public int idinstrutor { get; set; }
        public string txinstrutor { get; set; }
        public string txemail { get; set; }
        public string txtelefone { get; set; }
        public string txdescritivo { get; set; }

        public Instrutores()
        {
            this.idinstrutor = 0;
            this.txinstrutor = "";
            this.txemail = "";
            this.txtelefone = "";
            this.txdescritivo = "";
        }

        public Instrutores(int id, string instrutor, string email, string telefone, string descritivo)
        {
            this.idinstrutor = id;
            this.txinstrutor = instrutor;
            this.txemail = email;
            this.txtelefone = telefone;
            this.txdescritivo = descritivo;
        }

        public void Salvar()
        {
            new InstrutoresDB().Salvar(this);
        }

        public void Alterar()
        {
            new InstrutoresDB().Alterar(this);
        }

        public void Excluir()
        {
            new InstrutoresDB().Excluir(this);
        }
    }
}
