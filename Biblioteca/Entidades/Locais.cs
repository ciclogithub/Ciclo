using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Locais
    {
        public int idlocal { get; set; }
        public int idorganizador { get; set; }
        public int idcidade { get; set; }
        public string txlocal { get; set; }
        public string txlogradouro { get; set; }
        public string txcidade { get; set; }
        public string txestado { get; set; }
        public int total { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

        public Locais()
        {
            this.idlocal = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.idcidade = 0;
            this.txlocal = "";
            this.txlogradouro = "";
            this.txcidade = "";
            this.txestado = "";
            this.total = 0;
        }

        public Locais(int id, int cidade, string local, string logradouro)
        {
            this.idlocal = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.idcidade = cidade;
            this.txlocal = local;
            this.txlogradouro = logradouro;
        }

        public Locais(int id, int cidade, string local, string logradouro, string nome_cidade, string nome_estado, int total)
        {
            this.idlocal = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.idcidade = cidade;
            this.txlocal = local;
            this.txlogradouro = logradouro;
            this.txcidade = nome_cidade;
            this.txestado = nome_estado;
            this.total = total;
        }

        public void Salvar()
        {
            new LocaisDB().Salvar(this);
        }

        public void Alterar()
        {
            new LocaisDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new LocaisDB().Excluir(ident);
        }
    }
}
