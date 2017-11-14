using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Biblioteca.Entidades
{
    public class Graficos
    {
        public int valor { get; set; }
        public string categoria { get; set; }
        public string serie { get; set; }


        public Graficos()
        {
            this.valor = 0;
            this.categoria = "";
            this.serie = "";
        }

        public Graficos(int valor, string categoria, string serie)
        {
            this.valor = valor;
            this.categoria = categoria;
            this.serie = serie;
        }

    }
}
