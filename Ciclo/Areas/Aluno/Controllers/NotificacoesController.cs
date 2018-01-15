using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.DB;
using Ciclo.Areas.Aluno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Aluno.Controllers
{
    public class NotificacoesController : Controller
    {
        // GET: Aluno/Notificacoes
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];
            return View(new NotificacoesView(Convert.ToInt32(cookie.Value)));
        }

        [Autenticacao]
        public JsonResult Gravar(string categoria = "", string localidade = "", string especialidade = "")
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            Notificacoes id = new NotificacoesDB().Buscar(Convert.ToInt32(cookie.Value));

            NotificacoesDB db = new NotificacoesDB();

            if (id == null)
            {
                db.Salvar(new Notificacoes(Convert.ToInt32(cookie.Value), especialidade, categoria, localidade));
                Notificacoes notificacao = db.Buscar(Convert.ToInt32(cookie.Value));
            }
            else
            {
                Notificacoes notificacao = db.Buscar(id.idusuario);
                notificacao.idespecialidade = especialidade;
                notificacao.idcategoria = categoria;
                notificacao.idlocalidade = localidade;

                db.Alterar(notificacao);
            }

            return Json(new Retorno());
        }
    }
}