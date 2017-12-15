using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Funcoes;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class InscricaoController : Controller
    {
        // GET: Inscricao
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            int curso = Convert.ToInt32(collection["curso"]);
            HttpCookie cookie = Request.Cookies["ciclo_usuario"];
            if (cookie != null)
            {
                Inscricoes insc = new InscricoesDB().Buscar(Convert.ToInt32(cookie.Value), curso);
                if (insc == null)
                {
                    ViewBag.data = "";
                    ViewBag.status = "9";

                    Inscricoes inscricao = new Inscricoes();
                    DateTime data = DateTime.Now;
                    inscricao.dtinscricao = data;
                    inscricao.idcurso = curso;
                    inscricao.idusuario = Convert.ToInt32(cookie.Value);
                    inscricao.idstatus = 0;
                    inscricao.Salvar();
                    inscricao.Email(curso, Convert.ToInt32(cookie.Value), data);
                }
                else
                {
                    ViewBag.data = insc.dtinscricao;
                    ViewBag.status = insc.idstatus;
                }
                
                return View(new CursoView(Convert.ToInt32(curso)));
            }
            else
            {
                return RedirectToAction("Index", "Login", new { curso = curso });
            }
        }
    }
}