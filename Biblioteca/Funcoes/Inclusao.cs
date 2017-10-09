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
        public Retorno Cadastro(Empresas empresaview)
        {
            Retorno retorno = new Retorno();
            if (empresaview != null)
            {
                empresaview.txemail = empresaview.txemail.TrimStart().TrimEnd();

                if (new EmpresasDB().ExisteEmail(empresaview.txemail) && empresaview.idempresa == 0)
                {
                    retorno.erro = true;
                    retorno.mensagem = "E-mail já cadastrado";
                }
                else
                {
                    Empresas empresa = new Empresas();
                    if (empresaview.idempresa == 0)
                    {
                        empresa = empresaview.Retornar();
                        empresa.Salvar();
                    }
                    else
                    {
                        empresa = empresaview.Atualizar(new EmpresasDB().Buscar(empresaview.idempresa));
                        empresa.Alterar();
                    }

                    retorno.erro = false;
                    retorno.mensagem = "Redirecionando...";

                    var id = new EmpresasDB().Email(empresaview.txemail).idempresa;

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
