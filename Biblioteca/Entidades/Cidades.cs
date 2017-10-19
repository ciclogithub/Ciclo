using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Cidades
    {
        public int idcidade { get; set; }
        public int idestado { get; set; }
        public string txcidade { get; set; }
        
        public Cidades()
        {
            this.idcidade = 0;
            this.idestado = 0;
            this.txcidade = "";
        }

        public Cidades(int id, int estado, string cidade)
        {
            this.idcidade = id;
            this.idestado = estado;
            this.txcidade = cidade;
        }

    }
}
