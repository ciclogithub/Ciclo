using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Certificados
    {
        public int idcertificado { get; set; }
        public int idusuario { get; set; }
        public string txcertificadora { get; set; }
        public string txcurso { get; set; }
        public DateTime dtinicio { get; set; }
        public DateTime dtfim { get; set; }
        public int total { get; set; }
        public string txinstrutor { get; set; }
        public int nrnota { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

        public Certificados()
        {
            this.idcertificado = 0;
            this.idusuario = Convert.ToInt32(cookie.Value);
            this.txcertificadora = "";
            this.txcurso = "";
            this.dtinicio = DateTime.Now;
            this.dtfim = DateTime.Now;
            this.total = 0;
            this.txinstrutor = "";
            this.nrnota = 0;
        }

        public Certificados(int id, string certificadora, string curso, DateTime datainicio, DateTime datafim, int total, string instrutor, int nota)
        {
            this.idcertificado = id;
            this.idusuario = Convert.ToInt32(cookie.Value);
            this.txcertificadora = certificadora;
            this.txcurso = curso;
            this.dtinicio = datainicio;
            this.dtfim = datafim;
            this.total = total;
            this.txinstrutor = instrutor;
            this.nrnota = nota;
        }

        public void Salvar()
        {
            new CertificadosDB().Salvar(this);
        }

        public void Alterar()
        {
            new CertificadosDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new CertificadosDB().Excluir(ident);
        }
    }
}
