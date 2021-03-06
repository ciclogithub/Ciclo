﻿using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ciclo.Areas.Painel.Models;
using System.IO;
using Biblioteca.Funcoes;

namespace Ciclo.Areas.Painel.Controllers
{
    public class EmpresasController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string filtro = "", int pagina = 1)
        {
            List<Empresas> list = new List<Empresas>();
            ViewBag.filtro = filtro;

            if (filtro == "")
                list = new EmpresasDB().Listar(pagina, 10);
            else
                list = new EmpresasDB().Listar(filtro, pagina, 10);

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
                new EmpresasDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            Empresas empresa = new Empresas();
            if (id != 0)
            {
                empresa = new EmpresasDB().Buscar(id);
            }

            ViewBag.paises = new PaisesDB().Listar();

            return PartialView(empresa);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string codigo = "", string empresa = "", string email = "", string telefone = "", string cnpj = "", string cep = "", int cidade = 0, string bairro = "", string logradouro = "", string numero = "", string complemento = "")
        {
            EmpresasDB db = new EmpresasDB();
            int ident = 0;

            if (id == 0)
            {
                ident = db.Salvar(new Empresas(id, empresa, cnpj, codigo, cep, cidade, numero, logradouro, complemento, 0, bairro,0));
                Empresas emp = db.Buscar(id);
            }
            else
            {
                ident = id;
                Empresas emp = db.Buscar(id);
                emp.idempresa = id;
                emp.txempresa = empresa;
                emp.txcnpj = cnpj;
                emp.txcodigo = codigo;
                emp.nrcep = cep;
                emp.idcidade = cidade;
                emp.txnumero = numero;
                emp.txlogradouro = logradouro;
                emp.txcomplemento = complemento;
                emp.txbairro = bairro;

                db.Alterar(emp);

                db.RemoverEmails(ident);
                db.RemoverTelefones(ident);
            }
 
            var arrE = email.Split(',');
            foreach (var i in arrE)
            {
                if (i != "")
                {
                    new EmpresasDB().SalvarEmail(ident, i);
                }
            }

            var arrT = telefone.Split(',');
            foreach (var i in arrT)
            {
                if (i != "")
                {
                    var arrTemp = i.Split('|');
                    new EmpresasDB().SalvarTelefone(ident, arrTemp[1], Convert.ToInt32(arrTemp[0]));
                }

            }

            return Json(new Retorno());
        }

        [Autenticacao]
        [HttpPost]
        public ActionResult Cep(string cep)
        {
            return Json(new CepView(cep));
        }

        [Autenticacao]
        public int VerificaEmpresa(int id = 0, string nome = "", string cnpj = "")
        {
            int retorno = 0;
            if (cnpj != "") { retorno = new EmpresasDB().VerificaEmpresaCNPJ(id, cnpj); }
            if (retorno == 0) { retorno = new EmpresasDB().VerificaEmpresa(id, nome); }
            return retorno;
        }

        [Autenticacao]
        public int VerificaEmpresaExcluir(string id = "")
        {
            int retorno = new EmpresasDB().VerificaEmpresaExcluir(id);
            return retorno;
        }

        [Autenticacao]
        public ActionResult Importar()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];
            ViewBag.organizador = Convert.ToInt32(cookie.Value);
            return PartialView();
        }

        [Autenticacao]
        public JsonResult ImportarConcluir(HttpPostedFileBase txArquivo)
        {
            string result = "";
            HttpCookie cookie = HttpContext.Request.Cookies["ciclo_usuario"];
            if (txArquivo != null)
            {
                if (txArquivo.ContentLength > 0)
                {
                    string fileName = Path.GetExtension(txArquivo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/importacao"), Convert.ToInt32(cookie.Value) + fileName);
                    txArquivo.SaveAs(path);
                    result = Importacao.ImportaEmpresa(path);
                }
            }

            return Json(result);
        }
    }
}