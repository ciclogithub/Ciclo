using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Cursos_Alunos
    {
        public int curso_aluno { get; set; }
        public string aluno { get; set; }
        public string emails { get; set; }
        public string curso { get; set; }
        public string data { get; set; }
        public string telefones { get; set; }

        public Cursos_Alunos()
        {
            this.curso_aluno = 0;
            this.aluno = "";
            this.emails = "";
            this.curso = "";
            this.data = "";
            this.telefones = "";
        }

        public Cursos_Alunos(int curso_aluno, string aluno, string emails, string curso, string data, string telefones)
        {
            this.curso_aluno = curso_aluno;
            this.aluno = aluno;
            this.emails = emails;
            this.curso = curso;
            this.data = data;
            this.telefones = telefones;
        }

    }
}