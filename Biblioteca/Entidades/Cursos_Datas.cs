using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Cursos_Datas
    {
        public int iddatacurso { get; set; }
        public int idcurso { get; set; }
        public string dtcurso { get; set; }
        public string hrinicial { get; set; }
        public string hrfinal { get; set; }
        public string txobs { get; set; }

        public Cursos_Datas()
        {
            this.iddatacurso = 0;
            this.idcurso = 0;
            this.dtcurso = "";
            this.hrinicial = "";
            this.hrfinal = "";
            this.txobs = "";
        }

        public Cursos_Datas(int iddata, int idcurso, string data, string horaini, string horafim, string obs)
        {
            this.iddatacurso = iddata;
            this.idcurso = idcurso;
            this.dtcurso = data;
            this.hrinicial = horaini;
            this.hrfinal = horafim;
            this.txobs = obs;
        }

        public void Salvar()
        {
            new Cursos_DatasDB().Salvar(this);
        }

        public void Alterar()
        {
            new Cursos_DatasDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new Cursos_DatasDB().Excluir(ident);
        }
    }
}
