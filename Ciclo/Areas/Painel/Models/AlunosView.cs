using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Painel.Models
{
    public class AlunosView
    {
        public List<Estados> estados { get; set; }
        public List<Cores> cores { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public List<RedesSociais> redes { get; set; }
        public List<Empresas> empresas { get; set; }
        public Alunos alunos = new Alunos();

        public AlunosView(int id)
        {
            this.estados = new EstadosDB().Listar();
            this.cores = new CoresDB().Listar();
            this.especialidades = new EspecialidadesDB().Listar();
            this.redes = new RedesSociaisDB().Listar();
            //this.empresas = new EmpresasDB().Listar();
            if (id != 0)
            {
                this.alunos = new AlunosDB().Buscar(id);
            }
        }
        
    }
}