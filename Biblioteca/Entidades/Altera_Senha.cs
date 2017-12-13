using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entidades
{
    public class Altera_Senha
    {
        public int idperfil { get; set; }
        public int idusuario { get; set; }
        public DateTime data { get; set; }
        public string codigo { get; set; }
        public Byte ativo { get; set; }
        public string senha { get; set; }

        public Altera_Senha()
        {
            this.idperfil = 0;
            this.idusuario = 0;
            this.data = DateTime.Now;
            this.codigo = "";
            this.ativo = 1;
            this.senha = "";
        }

        public Altera_Senha(int perfil, int usuario, DateTime data, string codigo, Byte ativo)
        {
            this.idperfil = perfil;
            this.idusuario = usuario;
            this.data = data;
            this.codigo = codigo;
            this.ativo = ativo;
        }

        public void Salvar()
        {
            new Altera_SenhaDB().Salvar(this);
        }

        public void Alterar()
        {
            new Altera_SenhaDB().Alterar(this);
        }

    }
}
