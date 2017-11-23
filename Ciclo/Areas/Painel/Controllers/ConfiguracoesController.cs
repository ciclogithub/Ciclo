using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class ConfiguracoesController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            Organizadores organizador = new Organizadores();
            organizador = new OrganizadoresDB().Buscar(Convert.ToInt32(cookie.Value));
            
            return View(organizador);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string organizador = "", string telefone = "", string descricao = "")
        {
            OrganizadoresDB db = new OrganizadoresDB();

            Organizadores organizadores = db.Buscar(id);
            organizadores.txorganizador = organizador;
            organizadores.txtelefone = telefone;
            organizadores.txdescritivo = descricao;

            db.Atualizar(organizadores);

            return Json(new Retorno());
        }

        [Autenticacao]
        public JsonResult AlterarSenha(int id = 0, string senha = "", string novasenha = "")
        {

            string retorno = "";

            OrganizadoresDB db = new OrganizadoresDB();
            Organizadores organizadores = db.Buscar(id);
            organizadores.txsenha = MD5Hash.CalculaHash(senha);
            organizadores.txnovasenha = MD5Hash.CalculaHash(novasenha); ;

            bool painel = new PainelDB().Buscar(id, organizadores.txsenha);

            if (!painel)
            {
                retorno = "Dados incorretos";
                retorno = "{\"retorno\": \"" + retorno + "\"}";
            }
            else
            {
                db.AlteraSenha(organizadores);
                retorno = "OK";
                retorno = "{\"retorno\": \"" + retorno + "\"}";
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}