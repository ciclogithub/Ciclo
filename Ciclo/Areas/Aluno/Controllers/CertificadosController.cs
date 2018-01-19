using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Areas.Aluno.Controllers
{
    public class CertificadosController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string filtro = "", int pagina = 1)
        {
            List<Certificados> list = new List<Certificados>();
            ViewBag.filtro = filtro;

            if (filtro == "")
                list = new CertificadosDB().Listar(pagina, 10);
            else
                list = new CertificadosDB().Listar(filtro, pagina, 10);

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
                new CertificadosDB().Excluir(Convert.ToInt32(i));
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public ActionResult Incluir(int id = 0)
        {
            Certificados certificado = new Certificados();
            if (id != 0)
            {
                certificado = new CertificadosDB().Buscar(id);
            }

            return PartialView(certificado);
        }

        [Autenticacao]
        public JsonResult IncluirConcluir(int id, string certificadora, string curso, DateTime data_inicio, DateTime data_fim, string instrutor, int nota)
        {
            CertificadosDB db = new CertificadosDB();

            if (id == 0)
            {
                db.Salvar(new Certificados(id, certificadora, curso, data_inicio, data_fim, 0, instrutor, nota));
                Certificados certificado = db.Buscar(id);
            }
            else
            {
                Certificados certificado = db.Buscar(id);
                certificado.txcertificadora = certificadora;
                certificado.txcurso = curso;
                certificado.dtinicio = data_inicio;
                certificado.dtfim = data_fim;
                certificado.nrnota = nota;
                certificado.txinstrutor = instrutor;

                db.Alterar(certificado);
            }

            return Json(new Retorno());
        }
    }
}