using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DB
{

    public class ContadoresDB
    {
        public Contadores Listar(int organizador)
        {
            try
            {
                Contadores contador = null;

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select (select count(*) from Instrutores where idorganizador = 1) instrutor, (select count(*) from cursos where idorganizador = @organizador) cursos, (select count(*) from alunos where idorganizador = @organizador) alunos, ISNULL((select(SUM((cast(cv.nrnota1 + cv.nrnota2 + cv.nrnota3 + cv.nrnota4 + cv.nrnota5 as float) / 5)) / count(*)) from cursos c inner join cursos_alunos ca on ca.idcurso = c.idcurso inner join cursos_avaliacoes cv on cv.idcursoaluno = ca.idcursoaluno where c.idorganizador = @organizador),0) avaliacaoes, (select count(*) from empresas where idorganizador = @organizador) empresas, (select count(*) from alunos a where a.idorganizador = @organizador and isnull((case when A.txcpf = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = a.txcpf OR EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) end), 0) = 1) alunos_ciclo, (select count(*) from alunos a where a.idorganizador = @organizador and isnull((case when A.txcpf = '' then(select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = a.txcpf OR EXISTS(select ae.idaluno from Alunos_Emails as ae WHERE ae.txemail = e.email and ae.idaluno = a.idaluno)) end), 0) = 2) alunos_compra, (select count(*) from empresas em where em.idorganizador = @organizador and isnull((case when em.txcnpj = '' then (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = em.txcnpj OR EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) end), 0) = 1) empresas_ciclo, (select count(*) from empresas em where em.idorganizador = @organizador and isnull((case when em.txcnpj = '' then(select max(e.venda) from diagweb.diagweb.ExisteCliente as e where EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) else (select max(e.venda) from diagweb.diagweb.ExisteCliente as e where e.cpf_cnpj = em.txcnpj OR EXISTS(select ee.idempresa from Empresas_Emails as ee WHERE ee.txemail = e.email and ee.idempresa = em.idempresa)) end), 0) = 2) empresas_compra, (select count(*) from alunos where idorganizador = @organizador and idaluno not in (select idaluno from alunos_emails)) as alunos_email, (select count(*) from empresas where idorganizador = @organizador and idempresa not in (select idempresa from empresas_emails)) as empresas_email, (select count(*) from Temas where idorganizador = @organizador) temas, (select count(*) from Locais where idorganizador = @organizador) locais, (select COUNT(*) from Cursos_Alunos ca inner join Cursos c on c.idcurso = ca.idcurso where c.idorganizador = 2 and ca.txavaliacao <> '') avalenviada, (select COUNT(*) from Cursos_Avaliacoes ca inner join Cursos_Alunos cl on cl.idcursoaluno = ca.idcursoaluno inner join Cursos c on c.idcurso = cl.idcurso where c.idorganizador = 2 and cl.txavaliacao <> '') avalrecebido");
                query.SetParameter("organizador", organizador);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    contador = new Contadores(Convert.ToInt32(reader["instrutor"]), Convert.ToInt32(reader["cursos"]), Convert.ToInt32(reader["alunos"]), Convert.ToInt32(reader["avaliacaoes"]), Convert.ToInt32(reader["empresas"]), Convert.ToInt32(reader["alunos_ciclo"]), Convert.ToInt32(reader["alunos_compra"]), Convert.ToInt32(reader["alunos_email"]), Convert.ToInt32(reader["empresas_ciclo"]), Convert.ToInt32(reader["empresas_compra"]), Convert.ToInt32(reader["empresas_email"]), Convert.ToInt32(reader["temas"]), Convert.ToInt32(reader["locais"]), Convert.ToInt32(reader["avalenviada"]), Convert.ToInt32(reader["avalrecebido"]));
                }
                reader.Close();
                session.Close();

                return contador;

            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
