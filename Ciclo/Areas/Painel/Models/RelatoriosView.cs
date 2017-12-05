using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Painel.Models
{
    public class RelatoriosView
    {
        public List<Temas> temas { get; set; }
        public List<Categorias> categorias { get; set; }
        public List<Cores> cores { get; set; }
        public List<Cursos> cursos { get; set; }
        public List<Alunos> alunos { get; set; }
        public List<Mercados> mercados { get; set; }
        public List<Instrutores> instrutores { get; set; }
        public List<Locais> locais { get; set; }
        public List<Empresas> empresas { get; set; }
        public List<Paises> paises { get; set; }

        public RelatoriosView()
        {
            this.temas = new TemasDB().Listar();
            this.categorias = new CategoriasDB().Listar();
            this.locais = new LocaisDB().Listar();
            this.instrutores = new InstrutoresDB().Listar();
            this.alunos = new AlunosDB().ListarRel();
            this.mercados = new MercadosDB().Listar();
            this.cursos = new CursosDB().ListarRel();
            this.cores = new CoresDB().Listar();
            this.empresas = new EmpresasDB().ListarRel();
            this.paises = new PaisesDB().Listar();
        }
        
    }
}