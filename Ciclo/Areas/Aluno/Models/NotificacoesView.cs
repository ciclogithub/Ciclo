using Biblioteca.DB;
using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Aluno.Models
{
    public class NotificacoesView
    {
        public List<Categorias> categorias { get; set; }
        public List<Mercados> mercados { get; set; }
        public List<Paises> paises { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public Notificacoes usuario { get; set; }
        public string localidades { get; set; }

        public NotificacoesView(int id = 0)
        {
            this.categorias = new CategoriasDB().Listar();
            this.mercados = new MercadosDB().Listar();
            this.paises = new PaisesDB().Listar();
            this.especialidades = new EspecialidadesDB().Listar();
            this.usuario = new NotificacoesDB().Buscar(id);

            if (usuario != null)
            {
                string pais, estado, cidade, texto, valor;
                string[] array = usuario.idlocalidade.Split(',');
                foreach (string x in array)
                {
                    string[] array2 = x.Split('_');
                    pais = new PaisesDB().BuscarPais(Convert.ToInt32(array2[0]));
                    texto = pais;
                    valor = array2[0];
                    if (array2[1] != "")
                    {
                        estado = new EstadosDB().BuscarEstado(Convert.ToInt32(array2[1]));
                        texto = texto + " / " + estado;
                        valor = valor + "_" + array2[1];
                    } else
                    {
                        valor = valor + "_";
                    }

                    if (array2[2] != "")
                    {
                        cidade = new CidadesDB().BuscarCidade(Convert.ToInt32(array2[2]));
                        texto = texto + " / " + cidade;
                        valor = valor + "_" + array2[2];
                    } else
                    {
                        valor = valor + "_";
                    }

                    this.localidades = this.localidades + "<li id='" + valor + "'><i class='glyphicon glyphicon-trash' onclick=\"remove('localidade', '" + valor + "')\"></i><span>" + texto + "</span></li>";
                }
            }
        }
    }
}