using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;

namespace Biblioteca.DB
{
    public class TurmasDB
    {
        public void Salvar(Turmas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Turmas (txturma, dtinicio, dtfim, txlocal, nrmaximo, nrminimo, txdescritivo, idcidade) VALUES (@turma, @inicio, @fim, @local, @maximo, @minimo, @descritivo, @cidade) ");
                query.SetParameter("turma", variavel.txturma);
                query.SetParameter("inicio", variavel.dtinicio);
                query.SetParameter("fim", variavel.dtfim);
                query.SetParameter("local", variavel.txlocal);
                query.SetParameter("maximo", variavel.nrmaximo);
                query.SetParameter("minimo", variavel.nrminimo);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.SetParameter("cidade", variavel.idcidade);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Turmas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Turmas SET txturma = @turma, dtinicio = @inicio, dtfim = @fim, txlocal = @local, nrmaximo = @maximo, nrminimo = @minimo, txdescritivo = @descritivo, idcidade = @cidade WHERE idturma = @id");
                query.SetParameter("id", variavel.idturma);
                query.SetParameter("turma", variavel.txturma);
                query.SetParameter("inicio", variavel.dtinicio);
                query.SetParameter("fim", variavel.dtfim);
                query.SetParameter("local", variavel.txlocal);
                query.SetParameter("maximo", variavel.nrmaximo);
                query.SetParameter("minimo", variavel.nrminimo);
                query.SetParameter("descritivo", variavel.txdescritivo);
                query.SetParameter("cidade", variavel.idcidade);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Excluir(Turmas variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Turmas WHERE idturma = @id");
                query.SetParameter("id", variavel.idturma);
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
