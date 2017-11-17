using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class RedesSociais
    {
        public int idredesocial { get; set; }
        public string txredesocial { get; set; }
        public string txicone { get; set; }

        public RedesSociais()
        {
            this.idredesocial = 0;
            this.txredesocial = "";
            this.txicone = "";
        }

        public RedesSociais(int id, string rede, string icone)
        {
            this.idredesocial = id;
            this.txredesocial = rede;
            this.txicone = icone;
        }

    }
}
