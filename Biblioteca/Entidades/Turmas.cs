using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Turmas
    {
        public int idturma { get; set; }
        public string txturma { get; set; }
        public DateTime dtinicio { get; set; }
        public DateTime dtfim { get; set; }
        public string txlocal { get; set; }
        public int nrmaximo { get; set; }
        public int nrminimo { get; set; }
        public string txdescritivo { get; set; }
        public int idcidade { get; set; }

        public Turmas()
        {
            this.idturma = 0;
            this.txturma = "";
            this.dtinicio = DateTime.Now;
            this.dtfim = DateTime.Now;
            this.txlocal = "";
            this.nrmaximo = 0;
            this.nrminimo = 0;
            this.txdescritivo = "";
            this.idcidade = 0;
        }

        public Turmas(int id, string turma, DateTime inicio, DateTime fim, string local, int maximo, int minimo, string descritivo, int cidade)
        {
            this.idturma = id;
            this.txturma = turma;
            this.dtinicio = inicio;
            this.dtfim = fim;
            this.txlocal = local;
            this.nrmaximo = maximo;
            this.nrminimo = minimo;
            this.txdescritivo = descritivo;
            this.idcidade = cidade;
        }

        public void Salvar()
        {
            new TurmasDB().Salvar(this);
        }

        public void Alterar()
        {
            new TurmasDB().Alterar(this);
        }

        public void Excluir()
        {
            new TurmasDB().Excluir(this);
        }
    }
}
