using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Ciclo.Areas.Painel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class InstrutoresController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string instrutor = "")
        {
            List<Instrutores> list = new List<Instrutores>();
            ViewBag.instrutor = instrutor;

            if (instrutor == "")
                list = new InstrutoresDB().Listar();
            else
                list = new InstrutoresDB().Listar(instrutor);

            return View(list);
        }

        [Autenticacao]
        public JsonResult Excluir(string ident)
        {
            var arr = ident.Split(',');
            foreach (var i in arr)
            {
                new InstrutoresDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            Instrutores instrutor = new Instrutores();
            if (id != 0)
            {
                instrutor = new InstrutoresDB().Buscar(id);
            }

            return PartialView(instrutor);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string nome = "", string email = "", string telefone = "", string descricao = "", string txfoto = "")
        {
            InstrutoresDB db = new InstrutoresDB();

            if (id == 0)
            {
                db.Salvar(new Instrutores(id, nome, email, telefone, descricao));
                Instrutores instrutor = db.Buscar(id);
            }
            else
            {
                Instrutores instrutor = db.Buscar(id);
                instrutor.txinstrutor = nome;
                instrutor.txemail = email;
                instrutor.txtelefone = telefone;
                instrutor.txdescritivo = descricao;

                db.Alterar(instrutor);
            }

            //if (txfoto.File.ContentLength > 0)
            //{
            //    var fileName = Path.GetExtension(txfoto.File.FileName);
            //    var path = Path.Combine(Server.MapPath("~/Images/Instrutores"), id + "." + fileName);
            //    txfoto.File.SaveAs(path);
            //}

            return Json(new Retorno());
        }
    }
}