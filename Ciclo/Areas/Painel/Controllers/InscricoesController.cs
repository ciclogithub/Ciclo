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
    public class InscricoesController : Controller
    {
        [Autenticacao]
        public ActionResult Index(string filtro = "", int status = 0, int pagina = 1)
        {
            List<Inscricoes> list = new List<Inscricoes>();
            ViewBag.filtro = filtro;
            ViewBag.status = status;

            if (filtro == "")
                list = new InscricoesDB().Listar(status, pagina, 10);
            else
                list = new InscricoesDB().Listar(status, filtro, pagina, 10);

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
        public JsonResult Recusar(string id, string motivo)
        {
            var arr = id.Split(',');
            foreach (var i in arr)
            {
                Inscricoes inscricao = new InscricoesDB().Buscar(Convert.ToInt32(i));
                new InscricoesDB().Recusar(Convert.ToInt32(i), motivo);
                inscricao.EmailRecusa(inscricao.idcurso, inscricao.idusuario, motivo);
            }

            return Json(new Retorno());
        }

        [Autenticacao]
        public JsonResult Confirmacao(string id)
        {
            var arr = id.Split(',');
            foreach (var i in arr)
            {
                Inscricoes inscricao = new InscricoesDB().Buscar(Convert.ToInt32(i));
                int aluno = new UsuariosDB().BuscarCodigoAluno(inscricao.idusuario);

                // Verifica se o usuário já possui vínculo como aluno
                if (aluno == 0)
                {
                    Usuarios usuario = new UsuariosDB().BuscarCompleto(inscricao.idusuario);
                    int cpf = new UsuariosDB().BuscarAlunoPorCpf(usuario.txcpf);
                    int email = new UsuariosDB().BuscarAlunoPorEmail(usuario.txemail);

                    // Verifica se o CPF ou email já existem na base de alunos
                    if ((cpf == 0) && (email == 0))
                    {
                        // Cria o aluno com base no usuário
                        Alunos al = new Alunos();
                        al.txaluno = usuario.txusuario;
                        al.idorganizador = inscricao.idorganizador;
                        al.txcpf = usuario.txcpf;
                        al.idcidade = usuario.idcidade;
                        aluno = al.Salvar();

                        // Grava o email
                        new AlunosDB().SalvarEmail(aluno, usuario.txemail);

                        // Grava os mercados

                        // Grava os telefones

                        // Grava as redes sociais

                        // Grava relação usuario x aluno
                        usuario.GravaAluno(usuario.idusuario, aluno);

                        new InscricoesDB().Confirmar(Convert.ToInt32(i));
                        new CursosDB().SalvarAlunos(inscricao.idcurso, aluno);
                    }
                    else
                    {
                        aluno = cpf;
                        if (aluno == 0) { aluno = email; }

                        // Grava relação usuario x aluno
                        usuario.GravaAluno(usuario.idusuario, aluno);

                        new InscricoesDB().Confirmar(Convert.ToInt32(i));
                        new CursosDB().SalvarAlunos(inscricao.idcurso, aluno);
                    }
                }
                else
                {
                    new InscricoesDB().Confirmar(Convert.ToInt32(i));
                    new CursosDB().SalvarAlunos(inscricao.idcurso, aluno);
                }
                inscricao.EmailConfirmacao(inscricao.idcurso, inscricao.idusuario);
            }

            return Json(new Retorno());
        }
    }
}