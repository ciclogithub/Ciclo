using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class AvaliacaoController : Controller
    {
        // GET: Avaliacao
        public ActionResult Index()
        {
            string code = Request.QueryString["c"];
            int curso_aluno = 0;
            if (code != null)
            {
                curso_aluno = new AvaliacoesDB().Buscar(code);
            }

            if (curso_aluno == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new CursosAlunosDB().Buscar(curso_aluno));
            }

        }

        public JsonResult IncluirConcluir(int id = 0, int nota1 = 0, int nota2 = 0, int nota3 = 0, int nota4 = 0, int nota5 = 0, string obs = "")
        {
            AvaliacoesDB db = new AvaliacoesDB();
            db.Salvar(new Avaliacoes(id, "", "", nota1, nota2, nota3, nota4, nota5, obs, ""));

            return Json(new Retorno());
        }
    }
}