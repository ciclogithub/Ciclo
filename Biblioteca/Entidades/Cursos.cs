using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Cursos
    {
        public int idcurso { get; set; }
        public string txcurso { get; set; }
        public int idtema { get; set; }
        public int idcategoria { get; set; }
        public int idlocal { get; set; }
        public int nrminimo { get; set; }
        public int nrmaximo { get; set; }
        public int txcargahoraria { get; set; }
        public bool flgratuito { get; set; }
        public string txlocal { get; set; }
        public string txdescritivo { get; set; }
        public string txfoto { get; set; }
        public Temas nome_tema { get; set; }
        public Categorias nome_categoria { get; set; }
        public Locais nome_local { get; set; }

        public Cursos()
        {
            this.idcurso = 0;
            this.txcurso = "";
            this.idtema = 0;
            this.idcategoria = 0;
            this.idlocal = 0;
            this.nrminimo = 0;
            this.nrmaximo = 0;
            this.txcargahoraria = 0;
            this.flgratuito = true;
            this.txlocal = "";
            this.txdescritivo = "";
            this.txfoto = "";
            this.nome_tema = null;
            this.nome_categoria = null;
            this.nome_local = null;
        }

        public Cursos(int id, string curso, int tema, int categoria, int codlocal, string local, int minimo, int maximo, int cargahoraria, string descricao)
        {
            this.idcurso = id;
            this.txcurso = curso;
            this.idtema = tema;
            this.idcategoria = categoria;
            this.idlocal = codlocal;
            this.nrminimo = minimo;
            this.nrmaximo = maximo;
            this.txcargahoraria = cargahoraria;
            this.flgratuito = true;
            this.txlocal = local;
            this.txdescritivo = descricao;
            this.txfoto = "";
            this.nome_tema = new TemasDB().Buscar(tema);
            this.nome_categoria = new CategoriasDB().Buscar(categoria);
            this.nome_local = new LocaisDB().Buscar(codlocal);
        }

        public int Salvar()
        {
            int ident = new CursosDB().Salvar(this);
            return ident;
        }

        public void Alterar()
        {
            new CursosDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new CursosDB().Excluir(ident);
        }

        public void AlterarFoto()
        {
            new CursosDB().AlterarFoto(this);
        }
    }
}
