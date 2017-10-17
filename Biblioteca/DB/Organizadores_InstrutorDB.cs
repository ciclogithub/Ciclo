using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;

namespace Biblioteca.DB
{
    public class Organizadores_InstrutorDB
    {

        public void Salvar(Organizadores_Instrutor variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Organizadores_Instrutores (idorganizador, idinstrutor) VALUES (@organizador, @instrutor) ");
                query.SetParameter("organizador", variavel.idorganizador);
                query.SetParameter("instrutor", variavel.idinstrutor);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        
    }
}
