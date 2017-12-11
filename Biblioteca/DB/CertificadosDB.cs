using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Biblioteca.DB
{
    public class CertificadosDB
    {
        public void Salvar(Certificados variavel)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Usuarios_Certificados (idusuario, txcertificadora, txcurso, dtinicio, dtfim) VALUES (@usuario, @certificadora, @curso, @dtinicio, @dtfim)");
                query.SetParameter("usuario", Convert.ToInt32(cookie.Value));
                query.SetParameter("certificadora", variavel.txcertificadora);
                query.SetParameter("curso", variavel.txcurso);
                query.SetParameter("dtinicio", variavel.dtinicio);
                query.SetParameter("dtfim", variavel.dtfim);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Alterar(Certificados variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Usuarios_Certificados SET txcertificadora = @certificadora, txcurso = @curso, dtinicio = @dtinicio, dtfim = @dtfim WHERE idcertificado = @id");
                query.SetParameter("id", variavel.idcertificado);
                query.SetParameter("certificadora", variavel.txcertificadora);
                query.SetParameter("curso", variavel.txcurso);
                query.SetParameter("dtinicio", variavel.dtinicio);
                query.SetParameter("dtfim", variavel.dtfim);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("DELETE FROM Usuarios_Certificados WHERE idcertificado = @id");
                query.SetParameter("id", id);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public List<Certificados> Listar(int page = 1, int regs = 10)
        {
            try
            {
                List<Certificados> list_certificados = new List<Certificados>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* from Usuarios_Certificados WHERE idusuario = @usuario ORDER by txcertificadora OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("usuario", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_certificados.Add(new Certificados(Convert.ToInt32(reader["idcertificado"]), Convert.ToString(reader["txcertificadora"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["dtinicio"]), Convert.ToString(reader["dtfim"]),  Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_certificados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Certificados> Listar(string tema, int page = 1, int regs = 10)
        {
            try
            {
                List<Certificados> list_certificados = new List<Certificados>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total,* from Usuarios_Certificados WHERE idusuario = @usuario and (txcertificadora LIKE @certificado OR txcurso LIKE @certificado) ORDER by txtema OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("certificado", "%" + tema.Replace(" ", "%") + "%");
                query.SetParameter("usuario", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    list_certificados.Add(new Certificados(Convert.ToInt32(reader["idcertificado"]), Convert.ToString(reader["txcertificadora"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["dtinicio"]), Convert.ToString(reader["dtfim"]), Convert.ToInt32(reader["total"])));
                }
                reader.Close();
                session.Close();

                return list_certificados;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Certificados Buscar(int id)
        {
            try
            {
                Certificados certificado = null;

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Usuarios_Certificados WHERE idcertificado = @id");
                quey.SetParameter("id", id);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    certificado = new Certificados(Convert.ToInt32(reader["idcertificado"]), Convert.ToString(reader["txcertificadora"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["dtinicio"]), Convert.ToString(reader["dtfim"]), Convert.ToInt32(reader["total"]));
                }
                reader.Close();
                session.Close();

                return certificado;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
