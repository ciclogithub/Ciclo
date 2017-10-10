using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;

namespace Biblioteca.DB
{
    public class InstrutoresDB
    {
        public void Salvar(Instrutores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Instrutores (txinstrutor, txemail, txtelefone, txdescritivo) VALUES (@instrutor, @email, @telefone, @descritivo) ");
                query.SetParameter("instrutor", variavel.txinstrutor);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("telefone", variavel.txtelefone);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Instrutores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Instrutores SET txinstrutor = @empresa, txemail = @email, txtelefone = @telefone, txdescritivo = @descritivo WHERE idinstrutor = @id");
                query.SetParameter("id", variavel.idinstrutor);
                query.SetParameter("empresa", variavel.txinstrutor);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("telefone", variavel.txtelefone);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Excluir(Instrutores variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Instrutores WHERE idinstrutor = @id");
                query.SetParameter("id", variavel.idinstrutor);
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
