using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Ciclo.Models
{
    public class CicloView
    {
        public List<Painel_Ciclo> usuarios { get; set; }
        public List<Painel_Ciclo> organizadores { get; set; }

        public CicloView(string ini, string fim)
        {
            this.usuarios = new Painel_CicloDB().ListarUsuarios(ini, fim);
            this.organizadores = new Painel_CicloDB().ListarOrganizadores(ini, fim);
        }
        
    }
}