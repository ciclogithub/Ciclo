using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Cores
    {
        public int idcor { get; set; }
        public string txcor { get; set; }
        
        public Cores()
        {
            this.idcor = 0;
            this.txcor = "";
        }

        public Cores(int id, string cor)
        {
            this.idcor = id;
            this.txcor = cor;
        }

    }
}
