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
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login", new { curso = curso });
            }
        }
    }
}