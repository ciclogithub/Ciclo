using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Aluno.Models
{
    public class PerfilView
    {
        public List<Paises> paises { get; set; }
        public List<Mercados> mercados { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public List<RedesSociais> redes { get; set; }
        public Usuarios usuarios = new Usuarios();

        public PerfilView(int id)
        {
            this.paises = new PaisesDB().Listar();
            this.mercados = new MercadosDB().Listar();
            this.especialidades = new EspecialidadesDB().Listar();
            this.redes = new RedesSociaisDB().Listar();
            if (id != 0)
            {
                this.usuarios = new UsuariosDB().BuscarCompleto(id);
            }
        }
        
    }
}