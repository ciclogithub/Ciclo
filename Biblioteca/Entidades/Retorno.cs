using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Retorno
    {
        public bool erro { get; set; } = false;
        public int id { get; set; } = 0;
        public string mensagem { get; set; } = "";
        public double valor { get; set; } = 0;
        public string link { get; set; } = "";
        public int retorno { get; set; } = 0;
    }
}