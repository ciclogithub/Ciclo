using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Funcoes
{
    public static class Nulo
    {
        public static string IfNull(string valor, string retorno)
        {
            try
            {
                if (valor == null)
                {
                    return retorno;
                } else
                {
                    return valor;
                }
            }
            catch (Exception)
            {
                return null; 
            }
        }
    }
}
