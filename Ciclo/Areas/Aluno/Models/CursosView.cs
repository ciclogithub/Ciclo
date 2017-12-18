using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Aluno.Models
{
    public class CursosView
    {
        public List<Meus_Cursos> cursos_ativos { get; set; }
        public List<Meus_Cursos> cursos_finalizados { get; set; }
        public List<Meus_Cursos> inscricoes_pendentes { get; set; }
        public List<Meus_Cursos> inscricoes_recusada { get; set; }

        public CursosView(int id)
        {
            this.cursos_ativos = new Meus_CursosDB().ListarCursosAtivos(id);
            this.cursos_finalizados = new Meus_CursosDB().ListarCursosFinalizados(id);
            this.inscricoes_pendentes = new Meus_CursosDB().ListarInscricoesPendentes(id);
            this.inscricoes_recusada = new Meus_CursosDB().ListarInscricoesRecusadas(id);
        }
        
    }
}