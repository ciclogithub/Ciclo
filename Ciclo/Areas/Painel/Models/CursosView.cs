using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Painel.Models
{
    public class CursosView
    {
        public List<Temas> temas { get; set; }
        public List<Categorias> categorias { get; set; }
        public List<Mercados> mercados { get; set; }
        public List<Locais> locais { get; set; }
        public List<Cores> cores { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public Cursos cursos = new Cursos();

        public CursosView(int id)
        {
            this.temas = new TemasDB().Listar();
            this.categorias = new CategoriasDB().Listar();
            this.locais = new LocaisDB().Listar();
            this.cores = new CoresDB().Listar();
            this.especialidades = new EspecialidadesDB().Listar();
            this.mercados = new MercadosDB().Listar();
            if (id != 0)
            {
                this.cursos = new CursosDB().Buscar(id);
            }
        }
        
    }
}