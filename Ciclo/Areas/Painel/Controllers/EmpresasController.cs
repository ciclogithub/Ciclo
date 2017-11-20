﻿using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Painel.Controllers
{
    public class EmpresasController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string empresa = "", int pagina = 1)
        {
            List<Empresas> list = new List<Empresas>();
            ViewBag.aluno = empresa;

            if (empresa == "")
                list = new EmpresasDB().Listar(pagina, 10);
            else
                list = new EmpresasDB().Listar(empresa, pagina, 10);

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

            return PartialView(empresa);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id = 0, string nome = "", string cpf = "", string email = "", string telefone = "", int especialidade = 0, int cidade = 0, int cor = 0, string empresa = "", string obs = "", string redes = "")
        {
            AlunosDB db = new AlunosDB();
            int ident = 0;

            if (id == 0)
            {
                ident = db.Salvar(new Alunos(id, nome, cpf, especialidade, cidade, cor, empresa, 0, obs,0));
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
                db.RemoverRedesSociais(ident);
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
    }
}