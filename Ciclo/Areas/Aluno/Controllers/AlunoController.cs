﻿using Biblioteca.Filters;
using Ciclo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Aluno.Controllers
{
    public class AlunoController : Controller
    {
        [Autenticacao]
        public ActionResult Index()
        {
            return View();
        }

        [Autenticacao]
        public ActionResult Usuario()
        {
            return PartialView(new VerificaLogadoAluno().Buscar());
        }

        public ActionResult Sair()
        {
            HttpCookie cookie = Request.Cookies["ciclo_usuario"];
            cookie.Value = "";
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);

            HttpCookie cookie2 = Request.Cookies["ciclo_perfil"];
            cookie2.Value = "";
            cookie2.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "../Login");
        }

    }
}