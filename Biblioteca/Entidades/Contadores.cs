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
        public int cont_empresas { get; set; }
        public int avaliacao { get; set; }
        public int cont_alunos_ciclo { get; set; }
        public int cont_alunos_compra { get; set; }

        public Contadores()
        {
            this.cont_instrutor = 0;
            this.cont_curso = 0;
            this.cont_aluno = 0;
            this.cont_empresas = 0;
            this.avaliacao = 0;
            this.cont_alunos_ciclo = 0;
            this.cont_alunos_compra = 0;
        }

        public Contadores(int instrutor, int cursos, int alunos, int avaliacaoes, int empresas, int ciclo, int compra)
        {
            this.cont_instrutor = instrutor;
            this.cont_curso = cursos;
            this.cont_aluno = alunos;
            this.cont_empresas = empresas;
            this.avaliacao = avaliacaoes;
            this.cont_alunos_ciclo = ciclo;
            this.cont_alunos_compra = compra;
        }

    }
}
