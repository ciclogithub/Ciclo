using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Especialidades
    {
        public int idespecialidade { get; set; }
        public int idorganizador { get; set; }
        public string txespecialidade { get; set; }
        public string txsigla { get; set; }
        public int total { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

        public Especialidades()
        {
            this.idespecialidade = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txespecialidade = "";
            this.txsigla = "";
            this.total = 0;
        }

        public Especialidades(int id, string especialidade, string sigla, int total)
        {
            this.idespecialidade = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txespecialidade = especialidade;
            this.txsigla = sigla;
            this.total = total;
        }

        public void Salvar()
        {
            new EspecialidadesDB().Salvar(this);
        }

        public void Alterar()
        {
            new EspecialidadesDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new EspecialidadesDB().Excluir(ident);
        }
    }
}
