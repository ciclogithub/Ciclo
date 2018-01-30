using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ciclo.Areas.Painel.Models;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace Ciclo.Areas.Painel.Controllers
{
    public class AlunosController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string filtro = "", int pagina = 1)
        {
            List<Alunos> list = new List<Alunos>();
            ViewBag.filtro = filtro;

            if (filtro == "")
                list = new AlunosDB().Listar(pagina, 10);
            else
                list = new AlunosDB().Listar(filtro, pagina, 10);

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
                new AlunosDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            return PartialView(new AlunosView(id));
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string nome = "", string cpf = "", string email = "", string telefone = "", int cidade = 0, int cor = 0, int empresa = 0, string obs = "", string redes = "", string mercados = "")
        {
            AlunosDB db = new AlunosDB();
            int ident = 0;

            if (id == 0)
            {
                ident = db.Salvar(new Alunos(id, nome, cpf, cidade, cor, empresa, 0, obs, 0));
                Alunos aluno = db.Buscar(id);
            }
            else
            {
                ident = id;
                Alunos aluno = db.Buscar(id);
                aluno.txaluno = nome;
                aluno.txcpf = cpf;
                aluno.idcidade = cidade;
                aluno.idcor = cor;
                aluno.txobs = obs;
                aluno.idempresa = empresa;

                db.Alterar(aluno);

                db.RemoverEmails(ident);
                db.RemoverTelefones(ident);
                db.RemoverRedesSociais(ident);
                db.RemoverMercados(ident);
            }

            var arrM = mercados.Split(',');
            foreach (var i in arrM)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new AlunosDB().SalvarMercado(ident, Convert.ToInt32(arrTemp[0]));
                }
            }

            var arrE = email.Split(',');
            foreach (var i in arrE)
            {
                if (i != "")
                {
                    new AlunosDB().SalvarEmail(ident, i);
                }
            }

            var arrT = telefone.Split(',');
            foreach (var i in arrT)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new AlunosDB().SalvarTelefone(ident, arrTemp[1], Convert.ToInt32(arrTemp[0]));
                }

            }

            var arrR = redes.Split(',');
            foreach (var i in arrR)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new AlunosDB().SalvarRedeSocial(ident, arrTemp[1], Convert.ToInt32(arrTemp[0]));
                }

            }

            return Json(new Retorno());
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ListaEspecialidades()
        {
            List<Especialidades> especialidades = new EspecialidadesDB().Listar();
            return Json(especialidades);
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult ListaEmpresas()
        {
            List<Empresas> empresas = new EmpresasDB().ListarRel();
            return Json(empresas);
        }

        [Autenticacao]
        public int VerificaAluno(int id = 0, string nome = "", string cpf = "")
        {
            int retorno = 0;
            if (cpf != "") { retorno = new AlunosDB().VerificaAlunoCPF(id, cpf); }
            if (retorno == 0) { retorno = new AlunosDB().VerificaAluno(id, nome); }
            return retorno;
        }

        [Autenticacao]
        public int VerificaAlunoExcluir(string id = "")
        {
            int retorno = new AlunosDB().VerificaAlunoExcluir(id);
            return retorno;
        }

        [Autenticacao]
        public ActionResult Importar()
        {
            return PartialView();
        }

        [Autenticacao]
        public JsonResult ImportarConcluir(HttpPostedFileBase txArquivo)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];
            if (txArquivo != null)
            {
                if (txArquivo.ContentLength > 0)
                {
                    string fileName = Path.GetExtension(txArquivo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/importacao"), Convert.ToInt32(cookie.Value) + fileName);
                    txArquivo.SaveAs(path);

                    OleDbConnection conexao = null;

                    if (fileName == ".xls")
                    {
                        conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                    }
                    else if (fileName == ".xlsx")
                    {
                        conexao = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
                    }

                    OleDbDataAdapter adapter = new OleDbDataAdapter("select * from Alunos", conexao); /*[Sheet1$]*/
                    DataSet ds = new DataSet();
                    try
                    {
                        conexao.Open();
                        adapter.Fill(ds);
                        foreach (DataRow linha in ds.Tables[0].Rows)
                        {
                            Console.WriteLine(linha["nome"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ocorreu um erro ao ler o arquivo");
                    }
                    finally
                    {
                        conexao.Close();

                    }
                }
            }

            return Json(new Retorno());
        }

    }
}