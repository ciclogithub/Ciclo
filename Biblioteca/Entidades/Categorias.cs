using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Categorias
    {
        public int idcategoria { get; set; }
        public string txcategoria { get; set; }

        public Categorias()
        {
            this.idcategoria = 0;
            this.txcategoria = "";
        }

        public Categorias(int id, string categoria)
        {
            this.idcategoria = id;
            this.txcategoria = categoria;
        }

    }
}
