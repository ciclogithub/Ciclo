using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Notificacoes
    {
        public int idusuario { get; set; }
        public string idespecialidade { get; set; }
        public string idcategoria { get; set; }
        public string idlocalidade { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public List<Categorias> categorias { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

        public Notificacoes()
        {
            this.idusuario = Convert.ToInt32(cookie.Value);
            this.idespecialidade = "";
            this.idcategoria = "";
            this.idlocalidade = "";
        }

        public Notificacoes(int id, string especialidade, string categoria, string localidade)
        {
            this.idusuario = id;
            this.idespecialidade = especialidade;
            this.idcategoria = categoria;
            this.idlocalidade = localidade;
        }

        public void Salvar()
        {
            new NotificacoesDB().Salvar(this);
        }

        public void Alterar()
        {
            new NotificacoesDB().Alterar(this);
        }
    }
}
