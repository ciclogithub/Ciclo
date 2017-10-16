using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Contadores
    {
        public int cont_instrutor { get; set; }
        public int cont_aluno { get; set; }
        public int cont_curso { get; set; }
        public int avaliacao { get; set; }

        public Contadores()
        {
            this.cont_instrutor = 0;
            this.cont_curso = 0;
            this.cont_aluno = 0;
            this.avaliacao = 0;
        }

        public Contadores(int instrutor, int cursos, int alunos, int avaliacaoes)
        {
            this.cont_instrutor = instrutor;
            this.cont_curso = cursos;
            this.cont_aluno = alunos;
            this.avaliacao = avaliacaoes;
        }

    }
}
