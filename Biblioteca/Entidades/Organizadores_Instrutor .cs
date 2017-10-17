using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Organizadores_Instrutor
    {
        public int idorganizador { get; set; }
        public int idinstrutor { get; set; }

        public Organizadores_Instrutor()
        {
            this.idorganizador = 0;
            this.idinstrutor = 0;
        }

        public Organizadores_Instrutor(int id, int id2)
        {
            this.idorganizador = id;
            this.idinstrutor = id2;
        }
      
        public void Salvar()
        {
            new Organizadores_InstrutorDB().Salvar(this);
        }

        public void Excluir()
        {
            //new Organizadores_InstrutorDB().Excluir(this);
        }

    }
}
