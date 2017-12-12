using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using Biblioteca.Funcoes;
using Biblioteca.DB;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Funcoes
{
    public class Inclusao
    {
        public Retorno Cadastro(Organizadores organizadoresview)
        {
            Retorno retorno = new Retorno();
            if (organizadoresview != null)
            {
                organizadoresview.txemail = organizadoresview.txemail.TrimStart().TrimEnd();

                if (new OrganizadoresDB().ExisteEmail(organizadoresview.txemail) && organizadoresview.idorganizador == 0)
                {
                    retorno.erro = true;
                    retorno.mensagem = "E-mail já cadastrado";
                }
                else
                {
                    Organizadores organizadores = new Organizadores();
                    organizadoresview.txsenha = MD5Hash.CalculaHash(organizadoresview.txsenha);
                    if (organizadoresview.idorganizador == 0)
                    {
                        organizadores = organizadoresview.Retornar();
                        int idorganizador = organizadores.Salvar();

                        if (Convert.ToBoolean(organizadoresview.flinstrutor))
                        {
                            Instrutores instrutor = new Instrutores(organizadoresview);
                            instrutor.idorganizador = idorganizador;
                            int idinstrutor = instrutor.Salvar();
                        }
                    }
                    else
                    {
                        organizadores = organizadoresview.Atualizar(new OrganizadoresDB().Buscar(organizadoresview.idorganizador));
                        organizadores.Alterar();
                    }

                    retorno.erro = false;
                    retorno.mensagem = "Redirecionando...";

                    var id = new OrganizadoresDB().Email(organizadoresview.txemail).idorganizador;

                    retorno.id = id;

                    HttpCookie cookie = new HttpCookie("ciclo_usuario");
                    cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                    if (cookie == null)
                        cookie = new HttpCookie("ciclo_usuario");
                    cookie.Value = Convert.ToString(id);
                    HttpContext.Current.Response.Cookies.Add(cookie);

                    HttpCookie cookie2 = new HttpCookie("ciclo_perfil");
                    cookie2 = HttpContext.Current.Request.Cookies["ciclo_perfil"];
                    if (cookie2 == null)
                        cookie2 = new HttpCookie("ciclo_perfil");
                    cookie2.Value = "1";
                    HttpContext.Current.Response.Cookies.Add(cookie2);
                }
            }
            else
            {
                retorno.erro = true;
                retorno.mensagem = "Conteúdo vazio";
            }
            return retorno;

        }

        public Retorno CadastroAluno(Usuarios usuariosview)
        {
            Retorno retorno = new Retorno();
            if (usuariosview != null)
            {
                usuariosview.txemail = usuariosview.txemail.TrimStart().TrimEnd();

                if (new UsuariosDB().ExisteEmail(usuariosview.txemail) && usuariosview.idusuario == 0)
                {
                    retorno.erro = true;
                    retorno.mensagem = "E-mail já cadastrado";
                }
                else
                {
                    if (new UsuariosDB().ExisteCPF(usuariosview.txcpf) && usuariosview.idusuario == 0)
                    {
                        retorno.erro = true;
                        retorno.mensagem = "CPF já cadastrado";
                    }
                    else
                    {

                        Usuarios usuarios = new Usuarios();
                        usuariosview.txsenhaaluno = MD5Hash.CalculaHash(usuariosview.txsenhaaluno);
                        if (usuariosview.idusuario == 0)
                        {
                            usuarios = usuariosview.Retornar();
                            int idorganizador = usuarios.Salvar();
                        }
                        else
                        {
                            usuarios = usuariosview.Atualizar(new UsuariosDB().Buscar(usuariosview.idusuario));
                            usuarios.Alterar();
                        }

                        retorno.erro = false;
                        retorno.mensagem = "Redirecionando...";

                        var id = new UsuariosDB().Email(usuariosview.txemail).idusuario;

                        retorno.id = id;

                        HttpCookie cookie = new HttpCookie("ciclo_usuario");
                        cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
                        if (cookie == null)
                            cookie = new HttpCookie("ciclo_usuario");
                        cookie.Value = Convert.ToString(id);
                        HttpContext.Current.Response.Cookies.Add(cookie);

                        HttpCookie cookie2 = new HttpCookie("ciclo_perfil");
                        cookie2 = HttpContext.Current.Request.Cookies["ciclo_perfil"];
                        if (cookie2 == null)
                            cookie2 = new HttpCookie("ciclo_perfil");
                        cookie2.Value = "2";
                        HttpContext.Current.Response.Cookies.Add(cookie2);
                    }
                }
            }
            else
            {
                retorno.erro = true;
                retorno.mensagem = "Conteúdo vazio";
            }
            return retorno;

        }

        public Retorno EsqueceuSenha(string email, int perfil)
        {
            Retorno retorno = new Retorno();
            String codigo = RandomString(20);
            Altera_Senha senha = new Altera_Senha();

            if (email.Length > 2)
            {

                if (perfil == 1)
                {
                    Organizadores organizadores = new OrganizadoresDB().Email(email);
                    if (organizadores == null)
                    {
                        retorno.retorno = 0;
                        retorno.mensagem = "Dados incorretos.";
                    }
                    else
                    {
                        Email mail = new Email();
                        mail.destinatario = organizadores.txemail;
                        mail.assunto = "Solicitação de alteração de senha - www.treinaauto.com.br";
                        mail.mensagem = "<a href='http://www.treinaauto.com.br'><img src='http://www.treinaauto.com.br/images/logo.png' height='100'></a><br><br>Prezado(a) " + organizadores.txorganizador + ",<br><br>Para criar uma nova senha, clique ou copie o link: <a href='http://www.treinaauto.com.br/AlteraSenha?q=" + codigo + "'>http://www.treinaauto.com.br/AlteraSenha?q=" + codigo + "</a>.<br><br>Este link é válido por 24 horas.<br>Caso não tenha solicitado este e-mail, favor desconsiderar.<br><br>Att,<br><br>Treinaauto<br>contato@treinaauto.com.br";
                        string ret = mail.EnviaMensagem(mail);

                        senha.idperfil = 1;
                        senha.idusuario = organizadores.idorganizador;
                        senha.data = DateTime.Now;
                        senha.codigo = codigo;
                        senha.ativo = 1;
                        senha.Salvar();

                        retorno.mensagem = ret;
                    }
                }

                if (perfil == 2)
                {
                    Usuarios usuarios = new UsuariosDB().Email(email);
                    if (usuarios == null)
                    {
                        retorno.retorno = 0;
                        retorno.mensagem = "Dados incorretos.";
                    }
                    else
                    {
                        retorno.retorno = 1;
                        retorno.id = usuarios.idusuario;

                        Email mail = new Email();
                        mail.destinatario = usuarios.txemail;
                        mail.assunto = "Solicitação de alteração de senha - www.treinaauto.com.br";
                        mail.mensagem = "<a href='http://www.treinaauto.com.br'><img src='http://www.treinaauto.com.br/images/logo.png' height='100'></a><br><br>Prezado(a) " + usuarios.txusuario + ",<br><br>Para criar uma nova senha, clique ou copie o link: <a href='http://www.treinaauto.com.br/AlteraSenha?q=" + codigo + "'>http://www.treinaauto.com.br/AlteraSenha?q=" + codigo + "</a>.<br><br>Este link é válido por 24 horas.<br>Caso não tenha solicitado este e-mail, favor desconsiderar.<br><br>Att,<br><br>Treinaauto<br>contato@treinaauto.com.br";
                        string ret = mail.EnviaMensagem(mail);

                        senha.idperfil = 1;
                        senha.idusuario = usuarios.idusuario;
                        senha.data = DateTime.Now;
                        senha.codigo = codigo;
                        senha.ativo = 1;
                        senha.Salvar();

                        retorno.mensagem = ret;
                    }
                }

            }
            else
            {
                retorno.retorno = 0;
                retorno.mensagem = "Dados incorretos.";
            }

            return retorno;

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    } 
}
