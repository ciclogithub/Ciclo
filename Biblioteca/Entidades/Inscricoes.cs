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
        public Nullable<DateTime> dtinscricao { get; set; }
        public int idstatus { get; set; }
        public string curso { get; set; }
        public string usuario { get; set; }
        public int total { get; set; }
        public int idorganizador { get; set; }
        public Nullable<DateTime> dtstatus{ get; set; }
        public string motivo { get; set; }

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
            this.idorganizador = 0;
            this.dtstatus = null;
            this.motivo = "";
        }

        public Inscricoes(int id, int usuario, Nullable<DateTime> data, int curso, int status, Nullable<DateTime> dtstatus, string motivo)
        {
            this.idinscricao = id;
            this.idusuario = usuario;
            this.dtinscricao = data;
            this.idcurso = curso;
            this.idstatus = status;
            this.idorganizador = new CursosDB().Buscar(curso).idorganizador;
            this.dtstatus = dtstatus;
            this.motivo = motivo;
        }

        public Inscricoes(int id, Nullable<DateTime> data, string curso, string usuario, int total, Nullable<DateTime> dtstatus, string motivo)
        {
            this.idinscricao = id;
            this.dtinscricao = data;
            this.curso = curso;
            this.usuario = usuario;
            this.total = total;
            this.dtstatus = dtstatus;
            this.motivo = motivo;
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
            mail.mensagem = "<a href='http://www.treinaauto.com.br'><img src='http://www.treinaauto.com.br/images/logo.png' height='100'></a><br><br>Prezado(a) " + o.txorganizador + ",<br><br>O aluno " + u.txusuario.ToUpper() + " solicitou a inscrição no curso " + c.txcurso.ToUpper() + " em " + data + ".<br>Acesse seu painel no site <a href='http://www.treinaauto.com.br'>www.treinaauto.com.br</a> para confirmar a inscrição.<br><br>Att,<br><br>Treinaauto<br><a href='mailto:contato@treinaauto.com.br'>contato@treinaauto.com.br</a>";
            string ret = mail.EnviaMensagem(mail);
        }

        public void EmailConfirmacao(int curso, int usuario)
        {
            Cursos c = new CursosDB().Buscar(curso);
            Organizadores o = new OrganizadoresDB().Buscar(c.idorganizador);
            Usuarios u = new UsuariosDB().Buscar(usuario);

            Email mail = new Email();
            mail.destinatario = u.txemail;
            mail.assunto = "Informações sobre inscrição no curso " + c.txcurso + " - www.treinaauto.com.br";
            mail.mensagem = "<a href='http://www.treinaauto.com.br'><img src='http://www.treinaauto.com.br/images/logo.png' height='100'></a><br><br>Prezado(a) " + u.txusuario + ",<br><br>O organizador está ciente de sua solicitação de inscrição no curso " + c.txcurso.ToUpper() + ".<br>Aguarde por mais informações para confirmar sua inscrição.<br>Para mais informações sobre o curso, acesse <a href='http://www.treinaauto.com.br/curso?c="+curso+"'>www.treinaauto.com.br/curso?c="+curso+ "</a>.<br><br>Att,<br><br>Treinaauto<br><a href='mailto:contato@treinaauto.com.br'>contato@treinaauto.com.br</a>";
            string ret = mail.EnviaMensagem(mail);
        }

        public void EmailRecusa(int curso, int usuario, string motivo)
        {
            Cursos c = new CursosDB().Buscar(curso);
            Organizadores o = new OrganizadoresDB().Buscar(c.idorganizador);
            Usuarios u = new UsuariosDB().Buscar(usuario);

            Email mail = new Email();
            mail.destinatario = u.txemail;
            mail.assunto = "Cancelamento de inscrição no curso " + c.txcurso + " - www.treinaauto.com.br";
            mail.mensagem = "<a href='http://www.treinaauto.com.br'><img src='http://www.treinaauto.com.br/images/logo.png' height='100'></a><br><br>" + motivo + "<br><br>Treinaauto<br><a href='mailto:contato@treinaauto.com.br'>contato@treinaauto.com.br</a>";
            string ret = mail.EnviaMensagem(mail);
        }

    }
}
