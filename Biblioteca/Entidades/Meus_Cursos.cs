﻿using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Meus_Cursos
    {
        public int idcurso { get; set; }
        public string txcurso { get; set; }
        public string txlocal { get; set; }
        public Nullable<DateTime> dtcurso { get; set; }
        public Nullable<DateTime> dtsolicitacao { get; set; }
        public Nullable<DateTime> dtstatus { get; set; }
        public string txmotivo { get; set; }
        public Nullable<int> avaliacao { get; set; }

        public Meus_Cursos()
        {
            this.idcurso = 0;
            this.txcurso = "";
            this.txlocal = "";
            this.dtcurso = null;
            this.dtsolicitacao = null;
            this.dtstatus = null;
            this.txmotivo = "";
            this.avaliacao = null;
        }

        public Meus_Cursos(int id, string curso, string local, Nullable<DateTime> dtcurso, Nullable<DateTime> dtsolicitacao, Nullable<DateTime> dtstatus, string motivo, Nullable<int> avaliacao)
        {
            this.idcurso = id;
            this.txcurso = curso;
            this.txlocal = local;
            this.dtcurso = dtcurso;
            this.dtsolicitacao = dtsolicitacao;
            this.dtstatus = dtstatus;
            this.txmotivo = motivo;
            this.avaliacao = avaliacao;
        }

    }
}
