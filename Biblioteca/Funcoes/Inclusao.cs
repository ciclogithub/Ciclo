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

                if (new UsuariosDB().ExisteEmail(usuariosview.txemail) && usuariosview.idaluno == 0)
                {
                    retorno.erro = true;
                    retorno.mensagem = "E-mail já cadastrado";
                }
                else
                {
                    if (new UsuariosDB().ExisteCPF(usuariosview.txcpf) && usuariosview.idaluno == 0)
                    {
                        retorno.erro = true;
                        retorno.mensagem = "CPF já cadastrado";
                    }
                    else
                    {

                        Usuarios usuarios = new Usuarios();
                        usuariosview.txsenha = MD5Hash.CalculaHash(usuariosview.txsenha);
                        if (usuariosview.idaluno == 0)
                        {
                            usuarios = usuariosview.Retornar();
                            int idorganizador = usuarios.Salvar();
                        }
                        else
                        {
                            usuarios = usuariosview.Atualizar(new UsuariosDB().Buscar(usuariosview.idaluno));
                            usuarios.Alterar();
                        }

                        retorno.erro = false;
                        retorno.mensagem = "Redirecionando...";

                        var id = new UsuariosDB().Email(usuariosview.txemail).idaluno;

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

        public Retorno EsqueceuSenha(string esqueceu)
        {
            Retorno retorno = new Retorno();

            if (esqueceu.Length > 2)
            {
                //Alunos aluno = new AlunosDB().Email(esqueceu);

                //if (aluno == null)
                //{
                //    retorno.retorno = 0;
                //    retorno.mensagem = "Dados incorretos.";
                //}
                //else
                //{
                //    retorno.retorno = 1;
                //    retorno.id = aluno.codigo;
                    //new Envio_email()
                    //{
                    //    para = aluno.email,
                    //    assunto = "Dados de acesso - www.treinaauto.com.br",
                    //    texto = "Segue os dados de acceso conforme solicitado.<BR>Usuário: " + aluno.email + "<BR>Senha: " + aluno.senha
                    //}.Salvar();
            //        retorno.mensagem = "Dados enviados com sucesso.";
            //    }
            //}
            //else
            //{
            //    retorno.retorno = 0;
            //    retorno.mensagem = "Dados incorretos.";
            }

            return retorno;

        }
    }
}
