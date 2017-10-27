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

                    HttpCookie cookie = new HttpCookie("ciclo_instrutores");
                    cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];
                    if (cookie == null)
                        cookie = new HttpCookie("ciclo_instrutores");
                    cookie.Value = Convert.ToString(id);
                    HttpContext.Current.Response.Cookies.Add(cookie);

                }
            }
            else
            {
                retorno.erro = true;
                retorno.mensagem = "Conteúdo vazio";
            }
            return retorno;

        }
    }
}
