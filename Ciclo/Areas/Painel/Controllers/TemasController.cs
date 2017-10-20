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
        public ActionResult Index(string tema = "")
        {
            List<Temas> list = new List<Temas>();
            ViewBag.tema = tema;

            if (tema == "")
                list = new TemasDB().Listar();
            else
                list = new TemasDB().Listar(tema);

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
                db.Salvar(new Temas(id, nome, subtitulo, descricao));
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
    }
}