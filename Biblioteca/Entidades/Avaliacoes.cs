using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Avaliacoes
    {
        public int idcursoaluno { get; set; }
        public string aluno { get; set; }
        public string data { get; set; }
        public int nota1 { get; set; }
        public int nota2 { get; set; }
        public int nota3 { get; set; }
        public int nota4 { get; set; }
        public int nota5 { get; set; }
        public string obs { get; set; }
        public string avaliacao { get; set; }

        public Avaliacoes()
        {
            this.idcursoaluno = 0;
            this.aluno = "";
            this.data = "";
            this.nota1 = 0;
            this.nota2 = 0;
            this.nota3 = 0;
            this.nota4 = 0;
            this.nota5 = 0;
            this.obs = "";
            this.avaliacao = "";
        }

        public Avaliacoes(int id, string aluno, string data, int nota1, int nota2, int nota3, int nota4, int nota5, string obs, string avaliacao)
        {
            this.idcursoaluno = id;
            this.aluno = aluno;
            this.data = data;
            this.nota1 = nota1;
            this.nota2 = nota2;
            this.nota3 = nota3;
            this.nota4 = nota4;
            this.nota5 = nota5;
            this.obs = obs;
            this.avaliacao = avaliacao;
        }
    }
}
