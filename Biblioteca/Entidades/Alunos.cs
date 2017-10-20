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
        public List<Emails> txemail { get; set; }
        public List<Telefones> txtelefone { get; set; }

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
            this.txemail = new AlunosDB().ListarEmails(id); ;
            this.txtelefone = new AlunosDB().ListarTelefones(id); ;
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

        public void RemoverEmails(int id = 0)
        {
            new AlunosDB().RemoverEmails(id);
        }

        public void RemoverTelefones(int id = 0)
        {
            new AlunosDB().RemoverTelefones(id);
        }

        public void SalvarEmail(int id = 0, string email = "")
        {
            new AlunosDB().SalvarEmail(id, email);
        }

        public void SalvarTelefone(int id = 0, string telefone = "")
        {
            new AlunosDB().SalvarTelefone(id, telefone);
        }
    }

    public class Telefones
    {
        public int idaluno { get; set; }
        public string txtelefone { get; set; }

        public Telefones(int id, string telefone)
        {
            this.idaluno = id;
            this.txtelefone = telefone;
        }
    }

    public class Emails
    {
        public int idaluno { get; set; }
        public string txemail { get; set; }

        public Emails(int id, string email)
        {
            this.idaluno = id;
            this.txemail = email;
        }
    }
}
