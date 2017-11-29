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
        public string txespecialidade { get; set; }

        public Especialidades()
        {
            this.idespecialidade = 0;
            this.txespecialidade = "";
        }

        public Especialidades(int id, string especialidade)
        {
            this.idespecialidade = id;
            this.txespecialidade = especialidade;
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
