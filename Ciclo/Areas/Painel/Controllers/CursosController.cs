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
        public JsonResult IncluirConcluir(HttpPostedFileBase txfoto, int id = 0, string nome_curso = "", int tema = 0, int categoria = 0, int codlocal = 0, string local = "", string minimo = "", string maximo = "", string cargahoraria = "", string descricao = "", bool gratuito = false)
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
                curso.flgratuito = gratuito;

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
        [HttpPost]
        public ActionResult ListaTemas()
        {
            List<Temas> temas = new TemasDB().Listar();
            return Json(temas);
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ListaLocais()
        {
            List<Locais> locais = new LocaisDB().Listar();
            return Json(locais);
        }

        // INSTRUTORES

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
        [HttpPost]
        public ActionResult ListaInstrutores(string instrutor = "", string lista = "")
        {
            return Json(new InstrutoresDB().ListarTodos(instrutor, lista));
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

        // ALUNOS
        
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
        [HttpPost]
        public ActionResult ListaAlunos(string aluno = "", string lista = "")
        {
            return Json(new AlunosDB().ListarTodos(aluno, lista));
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

        // DATAS

        [Autenticacao]
        public ActionResult Datas(int id = 0)
        {
            Cursos curso = new Cursos();
            if (id != 0)
            {
                curso = new CursosDB().Buscar(id);
            }

            return PartialView(curso);
        }

        [Autenticacao]
        public JsonResult IncluirDatas(int iddata = 0, int idcurso = 0, string data = "", string horaini = "", string horafim = "", string descricao = "")
        {
            Cursos_DatasDB db = new Cursos_DatasDB();

            if (iddata == 0)
            {
                db.Salvar(new Cursos_Datas(iddata, idcurso, data, horaini, horafim, descricao));
                Cursos_Datas cursodata = db.Buscar(iddata);
            }
            else
            {
                Cursos_Datas cursodata = db.Buscar(iddata);
                cursodata.iddatacurso = iddata;
                cursodata.idcurso = idcurso;
                cursodata.dtcurso = data;
                cursodata.hrinicial = horaini;
                cursodata.hrfinal = horafim;
                cursodata.txobs = descricao;
                
                db.Alterar(cursodata);
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ListaDatas(int id = 0)
        {
            List<Cursos_Datas> cursodata = new Cursos_DatasDB().Listar(id);
            return Json(cursodata);
        }

        [Autenticacao]
        public JsonResult ExcluirData(int ident)
        {
            new Cursos_DatasDB().Excluir(ident);
            return Json(new Retorno());
        }

        [Autenticacao]
        public JsonResult AlterarData(int ident)
        {            
            return Json(new Cursos_DatasDB().Buscar(ident));
        }

        // VALORES

        [Autenticacao]
        public ActionResult Valores(int id = 0)
        {
            Cursos curso = new Cursos();
            if (id != 0)
            {
                curso = new CursosDB().Buscar(id);
            }

            return PartialView(curso);
        }

        [Autenticacao]
        public JsonResult IncluirValores(int idvalor = 0, int idcurso = 0, double valor = 0, string data = "")
        {
            Cursos_ValoresDB db = new Cursos_ValoresDB();

            if (idvalor == 0)
            {
                db.Salvar(new Cursos_Valores(idvalor, idcurso, valor, data));
                Cursos_Valores cursovalor = db.Buscar(idvalor);
            }
            else
            {
                Cursos_Valores cursovalor = db.Buscar(idvalor);
                cursovalor.idvalorcurso = idvalor;
                cursovalor.idcurso = idcurso;
                cursovalor.nrvalor = valor;
                cursovalor.dtvalor = data;

                db.Alterar(cursovalor);
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ListaValores(int id = 0)
        {
            List<Cursos_Valores> cursovalor = new Cursos_ValoresDB().Listar(id);
            return Json(cursovalor);
        }

        [Autenticacao]
        public JsonResult ExcluirValor(int ident)
        {
            new Cursos_ValoresDB().Excluir(ident);
            return Json(new Retorno());
        }

        [Autenticacao]
        public JsonResult AlterarValor(int ident)
        {
            return Json(new Cursos_ValoresDB().Buscar(ident));
        }
    }
}
