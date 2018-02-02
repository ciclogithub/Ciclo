using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class CursoView
    {

        public Curso_Site cursos = new Curso_Site();
        public List<Cursos_Site> outros = new List<Cursos_Site>();

        public CursoView(int id)
        {

            if (id != 0)
            {
                this.cursos = new Curso_SiteDB().Buscar(id);
                outros = new Cursos_SiteDB().ListardoOrganizador(id, 6, cursos.idorganizador);
            }
        }
        
    }
}