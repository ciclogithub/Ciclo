using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Painel_Ciclo
    {
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime data { get; set; }
        public string telefone { get; set; }
        public int cursos { get; set; }
        public int instrutores { get; set; }
        public int temas { get; set; }
        public int empresas { get; set; }
        public int locais { get; set; }
        public int alunos { get; set; }

        public Painel_Ciclo()
        {
            this.nome = "";
            this.email = "";
            this.data = DateTime.Now;
            this.telefone = "";
            this.cursos = 0;
            this.instrutores = 0;
            this.temas = 0;
            this.empresas = 0;
            this.locais = 0;
            this.alunos = 0;
        }

        public Painel_Ciclo(string nome, string email, DateTime data, string telefone)
        {
            this.nome = nome;
            this.email = email;
            this.data = data;
            this.telefone = telefone;
        }

        public Painel_Ciclo(string nome, string email, DateTime data, string telefone, int cursos, int instrutores, int temas, int locais, int empresas, int alunos)
        {
            this.nome = nome;
            this.email = email;
            this.data = data;
            this.telefone = telefone;
            this.cursos = cursos;
            this.instrutores = instrutores;
            this.temas = temas;
            this.empresas = empresas;
            this.locais = locais;
            this.alunos = alunos;
        }

    }
}
