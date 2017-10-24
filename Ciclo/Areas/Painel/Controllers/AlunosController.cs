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
    public class AlunosController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string aluno = "")
        {
            List<Alunos> list = new List<Alunos>();
            ViewBag.aluno = aluno;

            if (aluno == "")
                list = new AlunosDB().Listar();
            else
                list = new AlunosDB().Listar(aluno);

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
            Alunos aluno = new Alunos();
            if (id != 0)
            {
                aluno = new AlunosDB().Buscar(id);
            }

            return PartialView(aluno);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string nome = "", string cpf = "", string email = "", string telefone = "")
        {
            AlunosDB db = new AlunosDB();
            int ident = 0;

            if (id == 0)
            {
                ident = db.Salvar(new Alunos(id, nome, cpf));
                Alunos aluno = db.Buscar(id);
            }
            else
            {
                ident = id;
                Alunos aluno = db.Buscar(id);
                aluno.txaluno = nome;
                aluno.txcpf = cpf;

                db.Alterar(aluno);

                db.RemoverEmails(ident);
                db.RemoverTelefones(ident);
            }
 
            var arrE = email.Split(',');
            foreach (var i in arrE)
            {
                new AlunosDB().SalvarEmail(ident, i);
            }

            var arrT = telefone.Split(',');
            foreach (var i in arrT)
            {
                new AlunosDB().SalvarTelefone(ident, i);
            }

            return Json(new Retorno());
        }
    }
}