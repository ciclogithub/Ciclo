using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers{

    public class ListasController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            return View();
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult Relatorio(FormCollection collection)
        {
            int tipo = Convert.ToInt32(collection["tprelatorio"]);
            List<Listas> list = new List<Listas>();
            ViewBag.form = collection;

            switch (tipo)
            {
                case 1:
                    list = new ListasDB().ListarAlunosSemProdutos();
                    break;
                case 2:
                    list = new ListasDB().ListarAlunosComProdutos();
                    break;
                case 3:
                    list = new ListasDB().ListarAlunosSemDados();
                    break;
                case 4:
                    list = new ListasDB().ListarAlunosNaoCadastrados();
                    break;
                case 5:
                    list = new ListasDB().ListarEmpresasSemProdutos();
                    break;
                case 6:
                    list = new ListasDB().ListarEmpresasComProdutos();
                    break;
                case 7:
                    list = new ListasDB().ListarEmpresasSemDados();
                    break;
                case 8:
                    list = new ListasDB().ListarEmpresasNaoCadastradas();
                    break;
            }

            return View(list);
        }
    }
}