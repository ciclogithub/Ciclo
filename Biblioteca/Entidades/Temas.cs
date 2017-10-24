using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Temas
    {
        public int idtema { get; set; }
        public int idorganizador { get; set; }
        public string txtema { get; set; }
        public string txsubtitulo { get; set; }
        public string txdescritivo { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_instrutores"];

        public Temas()
        {
            this.idtema = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txtema = "";
            this.txsubtitulo = "";
            this.txdescritivo = "";
        }

        public Temas(int id, string tema, string subtitulo, string descritivo)
        {
            this.idtema = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txtema = tema;
            this.txsubtitulo = subtitulo;
            this.txdescritivo = descritivo;
        }

        public void Salvar()
        {
            new TemasDB().Salvar(this);
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
