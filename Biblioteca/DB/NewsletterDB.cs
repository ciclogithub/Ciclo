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
    public class NewsletterDB
    {
        public void Salvar(Newsletter variavel)
        {
            try
            {
                DBSession session = new DBSession();
                Query query = session.CreateQuery("INSERT INTO Newsletter (txnome, txcurso, txemail, txwhatsapp, txcidade, txestado) VALUES (@nome, @curso, @email, @whatsapp, @cidade, @estado)");
                query.SetParameter("nome", variavel.txnome);
                query.SetParameter("curso", variavel.txcurso);
                query.SetParameter("email", variavel.txemail);
                query.SetParameter("whatsapp", variavel.txwhatsapp);
                query.SetParameter("cidade", variavel.txcidade);
                query.SetParameter("estado", variavel.txestado);
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
