using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Mercados
    {
        public int idmercado { get; set; }
        public string txmercado { get; set; }
        public int total { get; set; }

        public Mercados()
        {
            this.idmercado = 0;
            this.txmercado = "";
            this.total = 0;
        }

        public Mercados(int id, string mercado, int total)
        {
            this.idmercado = id;
            this.txmercado = mercado;
            this.total = total;
        }

        public void Salvar()
        {
            new MercadosDB().Salvar(this);
        }

        public void Alterar()
        {
            new MercadosDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new MercadosDB().Excluir(ident);
        }
    }
}
