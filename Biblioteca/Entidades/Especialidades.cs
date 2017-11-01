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

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

        public Especialidades()
        {
            this.idespecialidade = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txespecialidade = "";
            this.txsigla = "";
        }

        public Especialidades(int id, string especialidade, string sigla)
        {
            this.idespecialidade = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txespecialidade = especialidade;
            this.txsigla = sigla;
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
