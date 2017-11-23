using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Logado
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int perfil { get; set; }
        
        public Logado()
        {
            this.id = 0;
            this.nome = "";
            this.perfil = 0;
        }

        public Logado(int id, string nome, int perfil)
        {
            this.id = id;
            this.nome = nome;
            this.perfil = perfil;
        }

    }
}
