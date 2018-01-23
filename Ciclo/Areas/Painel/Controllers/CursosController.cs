using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.Funcoes;
using Ciclo.Areas.Painel.Models;
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
        public ActionResult Index(string filtro = "", int pagina = 1)
        {
            List<Cursos> list = new List<Cursos>();
            ViewBag.filtro = filtro;

            if (filtro == "")
                list = new CursosDB().Listar(pagina, 10);
            else
                list = new CursosDB().Listar(filtro, pagina, 10);

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
                new CursosDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            return PartialView(new CursosView(id));
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(HttpPostedFileBase txfoto, int id = 0, string nome_curso = "", int tema = 0, int categoria = 0, int codlocal = 0, string local = "", string minimo = "", string maximo = "", string cargahoraria = "", string descricao = "", bool gratuito = false, int cor = 0, string identificador = "", string mercados = "", string especialidades = "")
        {
            CursosDB db = new CursosDB();
            int ident = 0;
            string fileName = "";
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];

            if (id == 0)
            {
                ident = db.Salvar(new Cursos(id, nome_curso, tema, categoria, codlocal, local, minimo, maximo, cargahoraria, descricao, gratuito, "", cor, identificador, 0, Convert.ToInt32(cookie.Value)));
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
                curso.idcor = cor;
                curso.txidentificador = identificador;
                curso.idorganizador = Convert.ToInt32(cookie.Value);

                db.Alterar(curso);

                db.RemoverMercados(ident);
                db.RemoverEspecialidades(ident);
            }

            var arrM = mercados.Split(',');
            foreach (var i in arrM)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new CursosDB().SalvarMercado(ident, Convert.ToInt32(arrTemp[0]));
                }
            }

            var arrE = especialidades.Split(',');
            foreach (var i in arrE)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new CursosDB().SalvarEspecialidades(ident, Convert.ToInt32(arrTemp[0]));
                }
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

            if (instrutores != "")
            {
                var arrI = instrutores.Split(',');
                foreach (var i in arrI)
                {
                    new CursosDB().SalvarInstrutor(id, Convert.ToInt32(i));
                }
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

            if (alunos != "")
            {
                var arrI = alunos.Split(',');
                foreach (var i in arrI)
                {
                    new CursosDB().SalvarAlunos(id, Convert.ToInt32(i));
                }
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

        // AVALIAÇÕES

        [Autenticacao]
        public ActionResult Avaliacao(int id = 0)
        {
            Cursos curso = new Cursos();
            if (id != 0)
            {
                curso = new CursosDB().Buscar(id);
            }

            ViewBag.avaliacoes = new AvaliacoesDB().Listar(id);

            return PartialView(curso);
        }

        [Autenticacao]
        public JsonResult AvaliacaoConcluir(string ident)
        {
            var arr = ident.Split(',');
            foreach (var i in arr)
            {
                new CursosDB().InserirAvaliacao(Convert.ToInt32(i), RandomString(15) + i);
            }

            return Json(new Retorno());
        }

        // LISTA DE ALUNOS

        [Autenticacao]
        public ActionResult Lista(int id = 0)
        {
            Cursos curso = new Cursos();
            if (id != 0)
            {
                curso = new CursosDB().Buscar(id);
            }

            ViewBag.alunos = new CursosAlunosDB().ListarDoCurso(id);

            return PartialView(curso);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Autenticacao]
        public int VerificaCurso(int id = 0, string nome = "", string identificador = "")
        {
            int retorno = 0;
            if (identificador != "") { retorno = new CursosDB().VerificaCursoIdent(id, identificador); }
            if (retorno == 0) { retorno = new CursosDB().VerificaCurso(id, nome); }
            return retorno;
        }

        [Autenticacao]
        public int VerificaCursoExcluir(string id = "")
        {
            int retorno = new CursosDB().VerificaCursoExcluir(id);
            return retorno;
        }

        public JsonResult SugestaoEspecialidade(string sugestao)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];
            Organizadores org = new OrganizadoresDB().Buscar(Convert.ToInt32(cookie.Value));

            Email mail = new Email();
            mail.destinatario = "contato@treinaauto.com.br";
            mail.assunto = "Sugestão de especialidade - www.treinaauto.com.br";
            mail.mensagem = "<a href='http://www.treinaauto.com.br'><img src='http://www.treinaauto.com.br/images/logo.png' height='100'></a><br><br>O usuário " + org.txorganizador + " enviou uma sugestão de especialidade.<br>Sugestão: "+sugestao+".<br><br>Att,<br><br>Treinaauto<br>contato@treinaauto.com.br";
            string ret = mail.EnviaMensagem(mail);
            return Json(ret);
        }
    }
}
