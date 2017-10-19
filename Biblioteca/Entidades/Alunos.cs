using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Alunos
    {
        public int idaluno { get; set; }
        public string txaluno { get; set; }
        public string txcpf { get; set; }
        
        public Alunos()
        {
            this.idaluno = 0;
            this.txaluno = "";
            this.txcpf = "";
        }

        public Alunos(int id, string aluno, string cpf)
        {
            this.idaluno = id;
            this.txaluno = aluno;
            this.txcpf = cpf;
        }

        public void Salvar()
        {
            new AlunosDB().Salvar(this);
        }

        public void Alterar()
        {
            new AlunosDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new AlunosDB().Excluir(ident);
        }
    }
}
