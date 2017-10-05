using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;

namespace Biblioteca.DB
{
    public class EmpresasDB
    {
        public void Salvar(Empresas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Empresas (txempresa, txemail, txsenha, txtelefone, txdescritivo) VALUES (@empresa, @email, @senha, @telefone, @descritivo) ");
                query.SetParameter("empresa", variavel.txempresa);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("senha", variavel.txsenha);
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

        public void Alterar(Empresas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Empresas SET txempresa = @empresa, txemail = @email, txsenha = @senha, txtelefone = @telefone, txdescritivo = @descritivo WHERE idempresa = @id");
                query.SetParameter("id", variavel.idempresa);
                query.SetParameter("empresa", variavel.txempresa);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("senha", variavel.txsenha);
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

        public void Excluir(Empresas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Empresas WHERE idempresa = @id");
                query.SetParameter("id", variavel.idempresa);
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
