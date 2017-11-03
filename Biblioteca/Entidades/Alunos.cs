using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Alunos
    {
        public int idaluno { get; set; }
        public int idorganizador { get; set; }
        public string txaluno { get; set; }
        public string txcpf { get; set; }
        public int idespecialidade { get; set; }
        public int idcidade { get; set; }
        public int idcor { get; set; }
        public List<Emails> txemail { get; set; }
        public List<Telefones> txtelefone { get; set; }
        public int idestado { get; set; }
        public string txcor { get; set; }
        public string txempresa { get; set; }
        public int total { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

        public Alunos()
        {
            this.idaluno = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txaluno = "";
            this.txcpf = "";
            this.idespecialidade = 0;
            this.idcidade = 0;
            this.idcor = 0;
            this.idestado = 0;
            this.txcor = "";
            this.txempresa = "";
            this.total = 0;
        }

        public Alunos(int id, string aluno, string cpf, int especialidade, int cidade, int cor, string empresa, int total)
        {
            this.idaluno = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txaluno = aluno;
            this.txcpf = cpf;
            this.txemail = new AlunosDB().ListarEmails(id); ;
            this.txtelefone = new AlunosDB().ListarTelefones(id);
            this.idespecialidade = especialidade;
            this.idcidade = cidade;
            this.idcor = cor;
            this.idestado = new EstadosDB().Buscar(cidade);
            this.txcor = new CoresDB().Buscar(cor);
            this.txempresa = empresa;
            this.total = total;
        }

        public int Salvar()
        {
            int ident = new AlunosDB().Salvar(this);
            return ident;
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
