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
    public class InscricoesDB
    {
        public void Salvar(Inscricoes variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Inscricoes (idusuario, idcurso, dtinscricao, idstatus) VALUES (@usuario, @curso, @data, @status)");
                query.SetParameter("usuario", variavel.idusuario);
                query.SetParameter("curso", variavel.idcurso);
                query.SetParameter("data", variavel.dtinscricao);
                query.SetParameter("status", variavel.idstatus);
                query.ExecuteUpdate();
                session.Close();          
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Recusar(int inscricao = 0, string motivo = "")
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Inscricoes set idstatus = 2, dtstatus = getdate(), txmotivo = @motivo where idinscricao = @inscricao");
                query.SetParameter("inscricao", inscricao);
                query.SetParameter("motivo", motivo);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public void Confirmar(int inscricao = 0)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("UPDATE Inscricoes set idstatus = 1, dtstatus = getdate() where idinscricao = @inscricao");
                query.SetParameter("inscricao", inscricao);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public Inscricoes Buscar(int usuario = 0, int curso = 0)
        {
            try
            {
                Inscricoes inscricoes = null;

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT * FROM Inscricoes WHERE idusuario = @usuario and idcurso = @curso");
                quey.SetParameter("usuario", usuario);
                quey.SetParameter("curso", curso);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    inscricoes = new Inscricoes(Convert.ToInt32(reader["idinscricao"]), Convert.ToInt32(reader["idusuario"]), Convert.ToDateTime(reader["dtinscricao"]), Convert.ToInt32(reader["idcurso"]), Convert.ToInt32(reader["idstatus"]), Convert.ToDateTime(reader["dtstatus"]), Convert.ToString(reader["txmotivo"]));
                }
                reader.Close();
                session.Close();

                return inscricoes;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Inscricoes Buscar(int inscricao = 0)
        {
            try
            {
                Inscricoes inscricoes = null;

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query quey = session.CreateQuery("SELECT *, isnull(dtinscricao,'') as data_inscricao, isnull(dtstatus,'') as data_status FROM Inscricoes WHERE idinscricao = @inscricao");
                quey.SetParameter("inscricao", inscricao);
                IDataReader reader = quey.ExecuteQuery();

                if (reader.Read())
                {
                    inscricoes = new Inscricoes(Convert.ToInt32(reader["idinscricao"]), Convert.ToInt32(reader["idusuario"]), Convert.ToDateTime(reader["data_inscricao"]), Convert.ToInt32(reader["idcurso"]), Convert.ToInt32(reader["idstatus"]), Convert.ToDateTime(reader["data_status"]), Convert.ToString(reader["txmotivo"]));
                }
                reader.Close();
                session.Close();

                return inscricoes;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Inscricoes> Listar(int status = 0, int page = 1, int regs = 10)
        {
            try
            {
                List<Inscricoes> inscricoes = new List<Inscricoes>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, i.txmotivo, isnull(i.dtstatus,'') as dtstatus, i.idinscricao, isnull(i.dtinscricao,'') as dtinscricao, c.txcurso, u.txusuario from inscricoes i left join Cursos c on c.idcurso = i.idcurso left join Usuarios u on u.idusuario = i.idusuario where i.idstatus = @idstatus and c.idorganizador = @idorganizador order by c.txcurso, u.txusuario OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("idstatus", status);
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    inscricoes.Add(new Inscricoes(Convert.ToInt32(reader["idinscricao"]), Convert.ToDateTime(reader["dtinscricao"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txusuario"]), Convert.ToInt32(reader["total"]), Convert.ToDateTime(reader["dtstatus"]), Convert.ToString(reader["txmotivo"])));
                }
                reader.Close();
                session.Close();

                return inscricoes;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<Inscricoes> Listar(int status = 0, string filtro = "", int page = 1, int regs = 10)
        {
            try
            {
                List<Inscricoes> inscricoes = new List<Inscricoes>();

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

                DBSession session = new DBSession();
                Query query = session.CreateQuery("select COUNT(*) OVER() as total, i.txmotivo, i.dtstatus, i.idinscricao, i.dtinscricao, c.txcurso, u.txusuario from inscricoes i left join Cursos c on c.idcurso = i.idcurso left join Usuarios u on u.idusuario = i.idusuario where (c.txcurso like @filtro OR u.txusuario like @filtro) and i.idstatus = @idstatus and c.idorganizador = @idorganizador order by c.txcurso, u.txusuario OFFSET @regs * (@page - 1) ROWS FETCH NEXT @regs ROWS ONLY");
                query.SetParameter("filtro", "%" + filtro.Replace(" ", "%") + "%");
                query.SetParameter("idstatus", status);
                query.SetParameter("idorganizador", Convert.ToInt32(cookie.Value));
                query.SetParameter("regs", regs);
                query.SetParameter("page", page);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    inscricoes.Add(new Inscricoes(Convert.ToInt32(reader["idinscricao"]), Convert.ToDateTime(reader["dtinscricao"]), Convert.ToString(reader["txcurso"]), Convert.ToString(reader["txusuario"]), Convert.ToInt32(reader["total"]), Convert.ToDateTime(reader["dtstatus"]), Convert.ToString(reader["txmotivo"])));
                }
                reader.Close();
                session.Close();

                return inscricoes;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
