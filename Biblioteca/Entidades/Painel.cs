using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DB;

namespace Biblioteca.Entidades
{
    public class Painel
    {
        public int codigo { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string email { get; set; }

        public Painel()
        {
            this.codigo = 0;
            this.nome = "";
            this.senha = "";
            this.email = "";
        }

        public Painel(int codigo, string nome, string senha, string email)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.senha = senha;
            this.email = email;
        }


    }
}
