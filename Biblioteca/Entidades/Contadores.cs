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
        public int cont_alunos_email { get; set; }
        public int cont_empresas_compra { get; set; }
        public int cont_empresas_ciclo { get; set; }
        public int cont_empresas_email { get; set; }
        public int cont_temas { get; set; }
        public int cont_locais { get; set; }
        public int cont_avaliacao_enviada { get; set; }
        public int cont_avaliacao_respondida { get; set; }
        public int cont_inscricoes { get; set; }

        public Contadores()
        {
            this.cont_instrutor = 0;
            this.cont_curso = 0;
            this.cont_aluno = 0;
            this.cont_empresas = 0;
            this.avaliacao = 0;
            this.cont_alunos_ciclo = 0;
            this.cont_alunos_compra = 0;
            this.cont_alunos_email = 0;
            this.cont_empresas_compra = 0;
            this.cont_empresas_ciclo = 0;
            this.cont_empresas_email = 0;
            this.cont_temas = 0;
            this.cont_locais = 0;
            this.cont_avaliacao_enviada = 0;
            this.cont_avaliacao_respondida = 0;
            this.cont_inscricoes = 0;
        }

        public Contadores(int instrutor, int cursos, int alunos, int avaliacaoes, int empresas, int ciclo, int compra, int emails, int empciclo, int empcompra, int empemails, int temas, int locais, int avalenviada, int avalrespondida, int inscricoes)
        {
            this.cont_instrutor = instrutor;
            this.cont_curso = cursos;
            this.cont_aluno = alunos;
            this.cont_empresas = empresas;
            this.avaliacao = avaliacaoes;
            this.cont_alunos_ciclo = ciclo;
            this.cont_alunos_compra = compra;
            this.cont_alunos_email = emails;
            this.cont_empresas_compra = empciclo;
            this.cont_empresas_ciclo = empcompra;
            this.cont_empresas_email = empemails;
            this.cont_temas = temas;
            this.cont_locais = locais;
            this.cont_avaliacao_enviada = avalenviada;
            this.cont_avaliacao_respondida = avalrespondida;
            this.cont_inscricoes = inscricoes;
        }

    }
}
