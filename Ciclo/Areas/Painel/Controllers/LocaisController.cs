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
    public class LocaisController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string local = "")
        {
            List<Locais> list = new List<Locais>();
            ViewBag.local = local;

            if (local == "")
                list = new LocaisDB().Listar();
            else
                list = new LocaisDB().Listar(local);

            return View(list);
        }

        [Autenticacao]
        public JsonResult Excluir(string ident)
        {
            var arr = ident.Split(',');
            foreach (var i in arr)
            {
                new LocaisDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            Locais local = new Locais();
            if (id != 0)
            {
                local = new LocaisDB().Buscar(id);
            }

            ViewBag.estados = new EstadosDB().Listar();

            return PartialView(local);
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ListaCidades(int id = 0)
        {
            List<Cidades> cidades = new CidadesDB().Listar(id);
            return Json(cidades);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, int cidade = 0, string nome = "", string logradouro = "")
        {
            LocaisDB db = new LocaisDB();

            if (id == 0)
            {
                db.Salvar(new Locais(id, cidade, nome, logradouro));
                Locais local = db.Buscar(id);
            }
            else
            {
                Locais local = db.Buscar(id);
                local.idcidade = cidade;
                local.txlocal = nome;
                local.txlogradouro = logradouro;


                db.Alterar(local);
            }

            return Json(new Retorno());
        }
    }
}