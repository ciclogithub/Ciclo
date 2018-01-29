using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Modelos_Email
    {
        public int idmodelo { get; set; }
        public int idorganizador { get; set; }
        public string txmodelo { get; set; }
        public string txmensagem { get; set; }
        public int idtipo { get; set; }

        public Modelos_Email()
        {
            this.idmodelo = 0;
            this.idorganizador = 0;
            this.txmodelo = "";
            this.txmensagem = "";
            this.idtipo = 0;
        }

        public Modelos_Email(int id, int organizador, string modelo, string mensagem, int tipo)
        {
            this.idmodelo = id;
            this.idorganizador = organizador;
            this.txmodelo = modelo;
            this.txmensagem = mensagem;
            this.idtipo = tipo;
        }

        public int Salvar()
        {
            int ident = new Modelos_EmailDB().Salvar(this);
            return ident;
        }

        public void Alterar()
        {
            new Modelos_EmailDB().Alterar(this);
        }

    }
}
