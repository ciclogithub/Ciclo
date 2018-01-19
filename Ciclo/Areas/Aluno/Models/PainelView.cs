using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Aluno.Models
{
    public class PainelView
    {
        public Boolean perfil { get; set; }
        public Boolean mercado { get; set; }
        public Boolean cursos { get; set; }
        public Boolean interesse { get; set; }

        public PainelView(int id)
        {
            this.perfil = new PainelAlunoDB().BuscarPerfil(id);
            this.mercado = new PainelAlunoDB().BuscarMercado(id);
            this.cursos = new PainelAlunoDB().BuscarCursos(id);
            this.interesse = new PainelAlunoDB().BuscarInteresse(id);
        }
        
    }
}