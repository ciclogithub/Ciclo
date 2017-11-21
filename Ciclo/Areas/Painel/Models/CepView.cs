using Biblioteca.DB;
using Biblioteca.Entidades;
using Correios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciclo.Areas.Painel.Models
{
    public class CepView
    {
        public string endereco { get; set; }
        public int idestado { get; set; }
        public int idcidade { get; set; }
        public string bairro { get; set; }

        public CepView(string cep)
        {
            try
            {
                using (CorreiosApi service = new CorreiosApi())
                {
                    var dados = service.consultaCEP(cep);

                    if (dados == null)
                    {
                        this.endereco = "";
                        this.idestado = 0;
                        this.idcidade = 0;
                        this.bairro = "";
                    }
                    else
                    {
                        this.endereco = dados.end;
                        this.idestado = new EstadosDB().Buscar(dados.uf);
                        this.idcidade = new CidadesDB().Buscar(this.idestado, dados.cidade);
                        this.bairro = dados.bairro;
                    }

                }
            }
            catch (Exception error)
            {
                this.endereco = error.Message;
            }
            
        }
        
    }
}