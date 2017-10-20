using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Temas
    {
        public int idtema { get; set; }
        public string txtema { get; set; }
        public string txsubtitulo { get; set; }
        public string txdescritivo { get; set; }

        public Temas()
        {
            this.idtema = 0;
            this.txtema = "";
            this.txsubtitulo = "";
            this.txdescritivo = "";
        }

        public Temas(int id, string tema, string subtitulo, string descritivo)
        {
            this.idtema = id;
            this.txtema = tema;
            this.txsubtitulo = subtitulo;
            this.txdescritivo = descritivo;
        }

        public int Salvar()
        {
            int ident = new TemasDB().Salvar(this);
            return ident;
        }

        public void Alterar()
        {
            new TemasDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new TemasDB().Excluir(ident);
        }
    }
}
