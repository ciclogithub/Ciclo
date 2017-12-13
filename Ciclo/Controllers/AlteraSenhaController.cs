using Biblioteca.DB;
using Biblioteca.Entidades;
using Biblioteca.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciclo.Controllers
{
    public class AlteraSenhaController : Controller
    {
        // GET: AlteraSenha
        public ActionResult Index()
        {
            string code = Request.QueryString["q"];
            {
                return View(new Altera_SenhaDB().Buscar(code));
            }
        }

        public JsonResult NovaSenha(int perfil = 0, int usuario = 0, string codigo = "", string txnovasenha = "", string txconfirmasenha = "")
        {
            Retorno ret = new Retorno();

            if ((txnovasenha == "") || (txconfirmasenha == "") || (txnovasenha != txconfirmasenha)) {
                ret.mensagem = "Senha e confirmação não preenchidas ou diferentes";
                ret.id = 0;
            } else {
                Altera_Senha db = new Altera_Senha();
                db.codigo = codigo;
                db.ativo = 0;
                db.idperfil = perfil;
                db.idusuario = usuario;
                db.senha = MD5Hash.CalculaHash(txnovasenha);
                db.Alterar();
                ret.id = 1;
                ret.mensagem = "Senha alterada com sucesso";
            }
            return Json(ret);
        }
    }
}