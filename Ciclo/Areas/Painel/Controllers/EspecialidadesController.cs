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
    public class EspecialidadesController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string especialidade = "")
        {
            List<Especialidades> list = new List<Especialidades>();
            ViewBag.especialidade = especialidade;

            if (especialidade == "")
                list = new EspecialidadesDB().Listar();
            else
                list = new EspecialidadesDB().Listar(especialidade);

            return View(list);
        }

        [Autenticacao]
        public JsonResult Excluir(string ident)
        {
            var arr = ident.Split(',');
            foreach (var i in arr)
            {
                new EspecialidadesDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            Especialidades especialidade = new Especialidades();
            if (id != 0)
            {
                especialidade = new EspecialidadesDB().Buscar(id);
            }

            return PartialView(especialidade);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string nome = "", string sigla = "")
        {
            EspecialidadesDB db = new EspecialidadesDB();

            if (id == 0)
            {
                db.Salvar(new Especialidades(id, nome, sigla));
                Especialidades especialidade = db.Buscar(id);
            }
            else
            {
                Especialidades especialidade = db.Buscar(id);
                especialidade.txespecialidade = nome;
                especialidade.txsigla = sigla;

                db.Alterar(especialidade);
            }

            return Json(new Retorno());
        }
    }
}