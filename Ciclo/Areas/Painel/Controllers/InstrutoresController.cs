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
        public JsonResult IncluirConcluir(HttpPostedFileBase txfoto, int id = 0, string nome = "", string email = "", string telefone = "", string descricao = "")
        {
            InstrutoresDB db = new InstrutoresDB();
            int ident = 0;
            string fileName = "";

            if (id == 0)
            {
                ident = db.Salvar(new Instrutores(id, nome, email, telefone, descricao, ""));
                Instrutores instrutor = db.Buscar(ident);
            }
            else
            {
                ident = id;
                Instrutores instrutor = db.Buscar(id);
                instrutor.txinstrutor = nome;
                instrutor.txemail = email;
                instrutor.txtelefone = telefone;
                instrutor.txdescritivo = descricao;

                db.Alterar(instrutor);
            }

            if (txfoto != null)
            {
                if (txfoto.ContentLength > 0)
                {
                    if (IsImage(txfoto))
                    {
                        fileName = Path.GetExtension(txfoto.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/Instrutores"), ident + fileName);
                        txfoto.SaveAs(path);

                        Instrutores instrutor_foto = db.Buscar(ident);
                        instrutor_foto.txfoto = ident + fileName;
                        db.AlterarFoto(instrutor_foto);
                    }
                }
            }

            return Json(new Retorno());
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}