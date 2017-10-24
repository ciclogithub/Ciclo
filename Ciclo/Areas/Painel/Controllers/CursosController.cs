using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class CursosController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string curso = "")
        {
            List<Cursos> list = new List<Cursos>();
            ViewBag.curso = curso;

            if (curso == "")
                list = new CursosDB().Listar();
            else
                list = new CursosDB().Listar(curso);

            return View(list);
        }

        [Autenticacao]
        public JsonResult Excluir(string ident)
        {
            var arr = ident.Split(',');
            foreach (var i in arr)
            {
                new CursosDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            Cursos curso = new Cursos();
            if (id != 0)
            {
                curso = new CursosDB().Buscar(id);
            }

            ViewBag.temas = new TemasDB().Listar();
            ViewBag.categorias = new CategoriasDB().Listar();
            ViewBag.locais = new LocaisDB().Listar();

            return PartialView(curso);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(HttpPostedFileBase txfoto, int id = 0, string nome_curso = "", int tema = 0, int categoria = 0, int codlocal = 0, string local = "", string minimo = "", string maximo = "", string cargahoraria = "", string descricao = "", bool gratuito = true)
        {
            CursosDB db = new CursosDB();
            int ident = 0;
            string fileName = "";

            if (id == 0)
            {
                ident = db.Salvar(new Cursos(id, nome_curso, tema, categoria, codlocal, local, minimo, maximo, cargahoraria, descricao, gratuito, ""));
                Cursos curso = db.Buscar(id);
            }
            else
            {
                ident = id;
                Cursos curso = db.Buscar(id);
                curso.txcurso = nome_curso;
                curso.idtema = tema;
                curso.idcategoria = categoria;
                curso.idlocal = codlocal;
                curso.txlocal = local;
                curso.nrminimo = minimo;
                curso.nrmaximo = maximo;
                curso.txcargahoraria = cargahoraria;
                curso.txdescritivo = descricao;

                db.Alterar(curso);
            }

            if (txfoto != null)
            {
                if (txfoto.ContentLength > 0)
                {
                    if (IsImage(txfoto))
                    {
                        fileName = Path.GetExtension(txfoto.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/Cursos"), ident + fileName);
                        txfoto.SaveAs(path);

                        Cursos curso_foto = db.Buscar(ident);
                        curso_foto.txfoto = ident + fileName;
                        db.AlterarFoto(curso_foto);
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

        [Autenticacao]
        public ActionResult Instrutores(int id = 0)
        {
            Cursos curso = new Cursos();
            if (id != 0)
            {
                curso = new CursosDB().Buscar(id);
            }

            ViewBag.instrutores = new InstrutoresDB().ListarTodos(id);

            return PartialView(curso);
        }

        [Autenticacao]
        public JsonResult IncluirInstrutores(int id = 0, string instrutores = "")
        {
            CursosDB db = new CursosDB();
            db.RemoverInstrutores(id);

            var arrI = instrutores.Split(',');
            foreach (var i in arrI)
            {
                new CursosDB().SalvarInstrutor(id, Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Alunos(int id = 0)
        {
            Cursos curso = new Cursos();
            if (id != 0)
            {
                curso = new CursosDB().Buscar(id);
            }

            ViewBag.alunos = new AlunosDB().ListarTodos(id);

            return PartialView(curso);
        }

        [Autenticacao]
        public JsonResult IncluirAlunos(int id = 0, string alunos = "")
        {
            CursosDB db = new CursosDB();
            db.RemoverAlunos(id);

            var arrI = alunos.Split(',');
            foreach (var i in arrI)
            {
                new CursosDB().SalvarAlunos(id, Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }
    }
}