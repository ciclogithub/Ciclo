using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Cursos_Valores
    {
        public int idvalorcurso { get; set; }
        public int idcurso { get; set; }
        public double nrvalor { get; set; }
        public string dtvalor { get; set; }
        public string valor { get; set; }

        public Cursos_Valores()
        {
            this.idvalorcurso = 0;
            this.idcurso = 0;
            this.nrvalor = 0;
            this.dtvalor = "";
        }

        public Cursos_Valores(int idvalor, int idcurso, double valor, string data)
        {
            this.idvalorcurso = idvalor;
            this.idcurso = idcurso;
            this.nrvalor = valor;
            this.dtvalor = data;
            this.valor = valor.ToString("N2");
        }

        public void Salvar()
        {
            new Cursos_ValoresDB().Salvar(this);
        }

        public void Alterar()
        {
            new Cursos_ValoresDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new Cursos_ValoresDB().Excluir(ident);
        }
    }
}
