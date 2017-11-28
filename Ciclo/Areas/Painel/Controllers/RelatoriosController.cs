using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ciclo.Areas.Painel.Models;

namespace Ciclo.Areas.Painel.Controllers{

    public class RelatoriosController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            return View(new RelatoriosView());
        }

        [Autenticacao]
        public JsonResult Graficos(FormCollection collection)
        {
            int ident = Convert.ToInt32(collection["tprelatorio"]);
            List<Graficos> graf = new List<Graficos>();

            switch (ident)
            {
                case 1:
                    graf = new GraficosDB().ListarAluno(collection);
                    break;
                case 2:
                    graf = new GraficosDB().ListarCategoria(collection);
                    break;
                case 3:
                    graf = new GraficosDB().ListarClassificacaoAluno(collection);
                    break;
                case 4:
                    graf = new GraficosDB().ListarClassificacaoCurso(collection);
                    break;
                case 5:
                    graf = new GraficosDB().ListarCurso(collection);
                    break;
                case 6:
                    graf = new GraficosDB().ListarMercado(collection);
                    break;
                case 7:
                    graf = new GraficosDB().ListarLocal(collection);
                    break;
                case 8:
                    graf = new GraficosDB().ListarInstrutor(collection);
                    break;
                case 9:
                    graf = new GraficosDB().ListarTema(collection);
                    break;
                case 10:
                    graf = new GraficosDB().ListarEmpresa(collection);
                    break;
            }

            return Json(graf);
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult Relatorio(FormCollection collection)
        {
            int tipo = Convert.ToInt32(collection["tprelatorio"]);
            List<Relatorios> list = new List<Relatorios>();
            ViewBag.form = collection;

            switch (tipo)
            {
                case 1:
                    list = new RelatoriosDB().ListarAluno(collection);
                    break;
                case 2:
                    list = new RelatoriosDB().ListarCategoria(collection);
                    break;
                case 3:
                    list = new RelatoriosDB().ListarClassificacaoAluno(collection);
                    break;
                case 4:
                    list = new RelatoriosDB().ListarClassificacaoCurso(collection);
                    break;
                case 5:
                    list = new RelatoriosDB().ListarCurso(collection);
                    break;
                case 6:
                    list = new RelatoriosDB().ListarMercado(collection);
                    break;
                case 7:
                    list = new RelatoriosDB().ListarLocal(collection);
                    break;
                case 8:
                    list = new RelatoriosDB().ListarInstrutor(collection);
                    break;
                case 9:
                    list = new RelatoriosDB().ListarTema(collection);
                    break;
                case 10:
                    list = new RelatoriosDB().ListarEmpresa(collection);
                    break;
            }

            return View(list);
        }

    }
}