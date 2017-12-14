using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Curso_Site
    {
        public int idcurso { get; set; }
        public string txcurso { get; set; }
        public string txcategoria { get; set; }
        public string txorganizador { get; set; }
        public int nrminimo { get; set; }
        public int nrmaximo { get; set; }
        public string txcargahoraria { get; set; }
        public Byte flgratuito { get; set; }
        public string txlocaldesc { get; set; }
        public string txdescritivo { get; set; }
        public string txfoto { get; set; }
        public string txlocal { get; set; }
        public string txlogradouro { get; set; }
        public string txcidade { get; set; }
        public string txestado { get; set; }
        public string txpais { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public List<Cursos_Datas> datas { get; set; }
        public List<Mercados> mercados { get; set; }
        public List<Instrutores> instrutores { get; set; }

        public Curso_Site()
        {
            this.idcurso = 0;
            this.txcurso = "";
            this.txcategoria = "";
            this.txorganizador = "";
            this.nrminimo = 0;
            this.nrmaximo = 0;
            this.txcargahoraria = "";
            this.flgratuito = 0;
            this.txlocaldesc = "";
            this.txdescritivo = "";
            this.txfoto = "";
            this.txlocal = "";
            this.txlogradouro = "";
            this.txcidade = "";
            this.txestado = "";
            this.txpais = "";
        }

        public Curso_Site(int id, string curso, string categoria, string organizador, int minimo, int maximo, string cargahoraria, byte gratuito, string localdesc, string descritivo, string foto, string local, string logradouro, string cidade, string estado, string pais)
        {
            this.idcurso = id;
            this.txcurso = curso;
            this.txcategoria = categoria;
            this.txorganizador = organizador;
            this.nrminimo = minimo;
            this.nrmaximo = maximo;
            this.txcargahoraria = cargahoraria;
            this.flgratuito = gratuito;
            this.txlocaldesc = localdesc;
            this.txdescritivo = descritivo;
            this.txfoto = foto;
            this.txlocal = local;
            this.txlogradouro = logradouro;
            this.txcidade = cidade;
            this.txestado = estado;
            this.txpais = pais;
            this.especialidades = new CursosDB().ListarEspecialidades(id);
            this.datas = new Cursos_DatasDB().Listar(id);
            this.mercados = new CursosDB().ListarMercados(id);
            this.instrutores = new InstrutoresDB().ListarDoCurso(id);
        }

        
    }
}
