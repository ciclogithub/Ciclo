using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Cursos_Site
    {
        public int total { get; set; }
        public int idcurso { get; set; }
        public string txcurso { get; set; }
        public string txcategoria { get; set; }
        public string txtema { get; set; }
        public string instrutores { get; set; }
        public string txfoto { get; set; }

        public Cursos_Site()
        {
            this.total = 0;
            this.idcurso = 0;
            this.txcurso = "";
            this.txcategoria = "";
            this.txtema = "";
            this.instrutores = "";
            this.txfoto = "";
        }

        public Cursos_Site(int total, int idcurso, string txcurso, string txcategoria, string txtema, string instrutores, string txfoto)
        {
            this.total = total;
            this.idcurso = idcurso;
            this.txcurso = txcurso;
            this.txcategoria = txcategoria;
            this.txtema = txtema;
            this.instrutores = instrutores;
            this.txfoto = txfoto;
        }

        
    }
}
