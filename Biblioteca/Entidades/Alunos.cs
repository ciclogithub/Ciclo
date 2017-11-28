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
        public int idcidade { get; set; }
        public int idcor { get; set; }
        public List<Emails> txemail { get; set; }
        public List<Telefones> txtelefone { get; set; }
        public int idestado { get; set; }
        public string txcor { get; set; }
        public int total { get; set; }
        public string txobs { get; set; }
        public List<Redes> txredes { get; set; }
        public int diagweb { get; set; }
        public int idempresa { get; set; }
        public List<Mercados> txmercado { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

        public Alunos()
        {
            this.idaluno = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txaluno = "";
            this.txcpf = "";
            this.idcidade = 0;
            this.idcor = 0;
            this.idestado = 0;
            this.txcor = "";
            this.total = 0;
            this.txobs = "";
            this.diagweb = 0;
            this.idempresa = 0;
        }

        public Alunos(int id, string aluno, string cpf, int cidade, int cor, int idempresa, int total, string obs, int diagweb)
        {
            this.idaluno = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txaluno = aluno;
            this.txcpf = cpf;
            this.txemail = new AlunosDB().ListarEmails(id); 
            this.txtelefone = new AlunosDB().ListarTelefones(id);
            this.idcidade = cidade;
            this.idcor = cor;
            this.idestado = new EstadosDB().Buscar(cidade);
            this.txcor = new CoresDB().Buscar(cor);
            this.total = total;
            this.txobs = obs;
            this.txredes = new AlunosDB().ListarRedesSociais(id);
            this.diagweb = diagweb;
            this.idempresa = idempresa;
            this.txmercado = new AlunosDB().ListarMercados(id);
        }

        public Alunos(int id, string aluno)
        {
            this.idaluno = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txaluno = aluno;
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

        public void RemoverMercados(int id = 0)
        {
            new AlunosDB().RemoverMercados(id);
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

        public void SalvarTelefone(int id = 0, string telefone = "", int whatsapp = 0)
        {
            new AlunosDB().SalvarTelefone(id, telefone, whatsapp);
        }
    }

    public class Redes
    {
        public int idaluno { get; set; }
        public string txredesocial { get; set; }
        public string txicone { get; set; }
        public int idredesocial { get; set; }

        public Redes(int id, string rede, string icone, int idrede)
        {
            this.idaluno = id;
            this.txredesocial = rede;
            this.txicone = icone;
            this.idredesocial = idrede;
        }
    }

    public class Telefones
    {
        public int idaluno { get; set; }
        public string txtelefone { get; set; }
        public int flwhatsapp { get; set; }

        public Telefones(int id, string telefone, int whatsapp)
        {
            this.idaluno = id;
            this.txtelefone = telefone;
            this.flwhatsapp = whatsapp;
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
