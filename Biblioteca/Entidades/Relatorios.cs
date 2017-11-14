using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Biblioteca.Entidades
{
    public class Relatorios
    {
        public int tipo { get; set; }
        public string curso { get; set; }
        public string data { get; set; }
        public string aluno { get; set; }
        public string empresa { get; set; }
        public string corcurso { get; set; }
        public string coraluno { get; set; }
        public string tema { get; set; }
        public string instrutor { get; set; }
        public string especialidade { get; set; }
        public string cidadecurso { get; set; }
        public string estadocurso { get; set; }
        public string cidadealuno { get; set; }
        public string estadoaluno { get; set; }
        public string local { get; set; }
        public string categoria { get; set; }
        public string identificador { get; set; }
        public int qtaluno { get; set; }
        public string emails { get; set; }
        public string telefones { get; set; }
        public double avaliacao { get; set; }
        public double avaliacaogeral { get; set; }

        public Relatorios()
        {
            this.tipo = 0;
            this.curso = "";
            this.data = "";
            this.aluno = "";
            this.empresa = "";
            this.corcurso = "";
            this.coraluno = "";
            this.tema = "";
            this.instrutor = "";
            this.especialidade = "";
            this.cidadecurso = "";
            this.estadocurso = "";
            this.cidadealuno = "";
            this.estadoaluno = "";
            this.local = "";
            this.categoria = "";
            this.identificador = "";
            this.qtaluno = 0;
            this.emails = "";
            this.telefones = "";
            this.avaliacao = 0;
            this.avaliacaogeral = 0;
        }

        public Relatorios(int tipo, string curso, string data, string aluno, string empresa, string corcurso, string coraluno, string tema, string instrutor, string especialidade, string cidadecurso, string estadocurso, string cidadealuno, string estadoaluno, string local, string categoria, string identificador, int qtaluno, string emails, string telefones, double avaliacao, double avaliacaogeral)
        {
            this.tipo = tipo;
            this.curso = curso;
            this.data = data;
            this.aluno = aluno;
            this.empresa = empresa;
            this.corcurso = corcurso;
            this.coraluno = coraluno;
            this.tema = tema;
            this.instrutor = instrutor;
            this.especialidade = especialidade;
            this.cidadecurso = cidadecurso;
            this.estadocurso = estadocurso;
            this.cidadealuno = cidadealuno;
            this.estadoaluno = estadoaluno;
            this.local = local;
            this.categoria = categoria;
            this.identificador = identificador;
            this.qtaluno = qtaluno;
            this.emails = emails;
            this.telefones = telefones;
            this.avaliacao = avaliacao;
            this.avaliacaogeral = avaliacaogeral;
        }

    }
}
