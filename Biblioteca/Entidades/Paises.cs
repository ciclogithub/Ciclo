using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Paises
    {
        public int idpais { get; set; }
        public string txpais { get; set; }
        
        public Paises()
        {
            this.idpais = 0;
            this.txpais = "";
        }

        public Paises(int id, string pais)
        {
            this.idpais = id;
            this.txpais = pais;
        }

    }
}
