using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class TemasController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string filtro = "", int pagina = 1)
        {
            List<Temas> list = new List<Temas>();
            ViewBag.filtro = filtro;

            if (filtro == "")
                list = new TemasDB().Listar(pagina, 10);
            else
                list = new TemasDB().Listar(filtro, pagina, 10);

            if (list.Count > 0)
            {
                ViewBag.total = list.First().total;
                ViewBag.pagina = pagina;
            }
            else
            {
                ViewBag.total = 0;
                ViewBag.pagina = 0;
            }

            return View(list);
        }

        [Autenticacao]
        public JsonResult Excluir(string ident)
        {
            var arr = ident.Split(',');
            foreach (var i in arr)
            {
                new TemasDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            Temas tema = new Temas();
            if (id != 0)
            {
                tema = new TemasDB().Buscar(id);
            }

            return PartialView(tema);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string nome = "", string subtitulo = "", string descricao = "")
        {
            TemasDB db = new TemasDB();

            if (id == 0)
            {
                db.Salvar(new Temas(id, nome, subtitulo, descricao, 0));
                Temas tema = db.Buscar(id);
            }
            else
            {
                Temas tema = db.Buscar(id);
                tema.txtema = nome;
                tema.txsubtitulo = subtitulo;
                tema.txdescritivo = descricao;

                db.Alterar(tema);
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public int VerificaTema(int id = 0, string nome = "")
        {
            int retorno = new TemasDB().VerificaTema(id, nome);
            return retorno;
        }
    }
}