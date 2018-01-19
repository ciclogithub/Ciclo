using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Biblioteca.Entidades;

namespace Biblioteca.DB
{
    public class PainelAlunoDB
    {
        public bool BuscarPerfil(int codigo)
        {
            try
            {
                bool retorno = false;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from usuarios where idusuario = @codigo and (txempresa is not null or idcidade is not null)");
                query.SetParameter("codigo", codigo);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    retorno = true;
                }
                reader.Close();

                Query query2 = session.CreateQuery("select * from Usuarios_Telefones where idusuario = @codigo");
                query2.SetParameter("codigo", codigo);
                IDataReader reader2 = query2.ExecuteQuery();

                if (reader2.Read())
                {
                    retorno = true;
                }
                reader2.Close();
                session.Close();

                return retorno;

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public bool BuscarMercado(int codigo)
        {
            try
            {
                bool retorno = false;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Usuarios_mercados where idusuario = @codigo");
                query.SetParameter("codigo", codigo);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    retorno = true;
                }
                reader.Close();

                Query query2 = session.CreateQuery("select * from Usuarios_especialidades where idusuario = @codigo");
                query2.SetParameter("codigo", codigo);
                IDataReader reader2 = query.ExecuteQuery();

                if (reader2.Read())
                {
                    retorno = true;
                }
                reader2.Close();
                session.Close();

                return retorno;

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public bool BuscarCursos(int codigo)
        {
            try
            {
                bool retorno = false;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Usuarios_certificados where idusuario = @codigo");
                query.SetParameter("codigo", codigo);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    retorno = true;
                }
                reader.Close();
                session.Close();

                return retorno;

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public bool BuscarInteresse(int codigo)
        {
            try
            {
                bool retorno = false;
                DBSession session = new DBSession();
                Query query = session.CreateQuery("select * from Usuarios_notificacoes where idusuario = @codigo");
                query.SetParameter("codigo", codigo);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    retorno = true;
                }
                reader.Close();
                session.Close();

                return retorno;

            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
