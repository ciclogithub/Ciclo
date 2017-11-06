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
        public ActionResult Index(string aluno = "", int pagina = 1)
        {
            List<Alunos> list = new List<Alunos>();
            ViewBag.aluno = aluno;

            if (aluno == "")
                list = new AlunosDB().Listar(pagina, 10);
            else
                list = new AlunosDB().Listar(aluno, pagina, 10);

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
            Alunos aluno = new Alunos();
            if (id != 0)
            {
                aluno = new AlunosDB().Buscar(id);
            }

            ViewBag.estados = new EstadosDB().Listar();
            ViewBag.especialidades = new EspecialidadesDB().Listar();
            ViewBag.cores = new CoresDB().Listar();

            return PartialView(aluno);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string nome = "", string cpf = "", string email = "", string telefone = "", int especialidade = 0, int cidade = 0, int cor = 0, string empresa = "", string obs = "")
        {
            AlunosDB db = new AlunosDB();
            int ident = 0;

            if (id == 0)
            {
                ident = db.Salvar(new Alunos(id, nome, cpf, especialidade, cidade, cor, empresa, 0, obs));
                Alunos aluno = db.Buscar(id);
            }
            else
            {
                ident = id;
                Alunos aluno = db.Buscar(id);
                aluno.txaluno = nome;
                aluno.txcpf = cpf;
                aluno.idespecialidade = especialidade;
                aluno.idcidade = cidade;
                aluno.idcor = cor;
                aluno.txempresa = empresa;
                aluno.txobs = obs;

                db.Alterar(aluno);

                db.RemoverEmails(ident);
                db.RemoverTelefones(ident);
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
                    new AlunosDB().SalvarTelefone(ident, i);
                }

            }

            return Json(new Retorno());
        }
    }
}