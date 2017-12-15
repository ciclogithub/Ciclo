using Biblioteca.DB;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Inscricoes
    {
        public int idinscricao { get; set; }
        public int idusuario { get; set; }
        public int idcurso { get; set; }
        public DateTime dtinscricao { get; set; }
        public int idstatus { get; set; }
        public string curso { get; set; }
        public string usuario { get; set; }
        public int total { get; set; }

        public Inscricoes()
        {
            this.idinscricao = 0;
            this.idusuario = 0;
            this.dtinscricao = DateTime.Now;
            this.idcurso = 0;
            this.idstatus = 0;
            this.curso = "";
            this.usuario = "";
            this.total = 0;
        }

        public Inscricoes(int id, int usuario, DateTime data, int curso, int status)
        {
            this.idinscricao = id;
            this.idusuario = usuario;
            this.dtinscricao = data;
            this.idcurso = curso;
            this.idstatus = status;
        }

        public Inscricoes(int id, DateTime data, string curso, string usuario, int total)
        {
            this.idinscricao = id;
            this.dtinscricao = data;
            this.curso = curso;
            this.usuario = usuario;
            this.total = total;
        }

        public void Salvar()
        {
            new InscricoesDB().Salvar(this);
        }

        public void Email(int curso, int usuario, DateTime data)
        {
            Cursos c = new CursosDB().Buscar(curso);
            Organizadores o = new OrganizadoresDB().Buscar(c.idorganizador);
            Usuarios u = new UsuariosDB().Buscar(usuario);

            Email mail = new Email();
            mail.destinatario = o.txemail;
            mail.assunto = "Solicitação de inscrição no curso " + c.txcurso + " - www.treinaauto.com.br";
            mail.mensagem = "<a href='http://www.treinaauto.com.br'><img src='http://www.treinaauto.com.br/images/logo.png' height='100'></a><br><br>Prezado(a) " + o.txorganizador + ",<br><br>O aluno " + u.txusuario.ToUpper() + " solicitou a inscrição no curso " + c.txcurso.ToUpper() + " em " + data + ".<br>Acesse seu painel no site <a href='http://www.treinaauto.com.br'>www.treinaauto.com.br</a> para confirmar a inscrição.<br><br>Att,<br><br>Treinaauto<br>contato@treinaauto.com.br";
            string ret = mail.EnviaMensagem(mail);
        }

    }
}
