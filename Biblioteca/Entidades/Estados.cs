using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Estados
    {
        public int idestado { get; set; }
        public string txestado { get; set; }
        public string txsigla { get; set; }
        
        public Estados()
        {
            this.idestado = 0;
            this.txestado = "";
            this.txsigla = "";
        }

        public Estados(int id, string estado, string sigla)
        {
            this.idestado = id;
            this.txestado = estado;
            this.txsigla = sigla;
        }

    }
}
