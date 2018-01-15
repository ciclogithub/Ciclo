using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Aluno.Models
{
    public class NotificacoesView
    {
        public List<Categorias> categorias { get; set; }
        public List<Mercados> mercados { get; set; }
        public List<Paises> paises { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public List<Especialidades> especialidade_usuario { get; set; }

        public NotificacoesView(int id = 0)
        {
            this.categorias = new CategoriasDB().Listar();
            this.mercados = new MercadosDB().Listar();
            this.paises = new PaisesDB().Listar();
            this.especialidades = new EspecialidadesDB().Listar();
            this.especialidade_usuario = new EspecialidadesDB().BuscarNotificacao(id);
        }
        
    }
}