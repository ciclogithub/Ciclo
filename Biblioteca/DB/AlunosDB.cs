using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;

namespace Biblioteca.DB
{
    public class AlunosDB
    {
        public void Salvar(Alunos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Aluno (txaluno, txcpf, txlocal) VALUES (@aluno, @cpf, @local) ");
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("cpf", variavel.txcpf);
                query.SetParameter("local", variavel.txlocal);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Alunos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Alunos SET txaluno = @aluno, txpcf = @cpf, txlocal = @local WHERE idaluno = @id");
                query.SetParameter("id", variavel.idaluno);
                query.SetParameter("aluno", variavel.txaluno);
                query.SetParameter("cpf", variavel.txcpf);
                query.SetParameter("local", variavel.txlocal);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Excluir(Alunos variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Alunos WHERE idaluno = @id");
                query.SetParameter("id", variavel.idaluno);
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