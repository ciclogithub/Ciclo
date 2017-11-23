using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Instrutores
    {
        public int idinstrutor { get; set; }
        public int idorganizador { get; set; }
        public string txinstrutor { get; set; }
        public string txemail { get; set; }
        public string txtelefone { get; set; }
        public string txdescritivo { get; set; }
        public string txfoto { get; set; }
        public int total { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

        public Instrutores()
        {
            this.idinstrutor = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txinstrutor = "";
            this.txemail = "";
            this.txtelefone = "";
            this.txdescritivo = "";
            this.txfoto = "";
            this.total = 0;
        }

        public Instrutores(Organizadores organizadoresview)
        {
            this.idinstrutor = 0;
            this.idorganizador = 0;
            this.txinstrutor = organizadoresview.txorganizador;
            this.txemail = organizadoresview.txemail;
            this.txtelefone = organizadoresview.txtelefone;
            this.txdescritivo = organizadoresview.txdescritivo;
            this.txfoto = "";
        }

        public Instrutores(int id, string instrutor, string email, string telefone, string descritivo, string foto, int total)
        {
            this.idinstrutor = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txinstrutor = instrutor;
            this.txemail = email;
            this.txtelefone = telefone;
            this.txdescritivo = descritivo;
            this.txfoto = foto;
            this.total = total;
        }

        public int Salvar()
        {
            int ident = new InstrutoresDB().Salvar(this);
            return ident;
        }

        public void Alterar()
        {
            new InstrutoresDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new InstrutoresDB().Excluir(ident);
        }

        public void AlterarFoto()
        {
            new InstrutoresDB().AlterarFoto(this);
        }
    }
}
