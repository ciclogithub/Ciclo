using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DB
{
    public class Query
    {
        private IDbCommand comando;
        public Query(String sql, IDbConnection connection)
        {
            comando = connection.CreateCommand();
            comando.CommandText = sql;
        }
        public void ExecuteUpdate()
        {
            comando.ExecuteNonQuery();
        }
        public IDataReader ExecuteQuery()
        {
            return comando.ExecuteReader();
        }
        public Query SetParameter(String nome, object valor)
        {
            var parametro = comando.CreateParameter();
            parametro.ParameterName = nome;
            parametro.Value = valor;
            comando.Parameters.Add(parametro);
            return this;
        }
    }

    public class DBSession
    {
        private IDbConnection conexao;
        public DBSession()
        {
            string conec = "Data Source=FSOARES-PC\\SQLEXPRESS; Initial Catalog=Instrutores; User ID=sa; Password=Ciclo2017; Language=Portuguese;  Max Pool Size=10000; Database=Instrutores";

            conexao = new SqlConnection(conec);

            conexao.Open();
        }
        public void Close()
        {
            conexao.Close();
        }
        public Query CreateQuery(String sql)
        {
            return new Query(sql, conexao);
        }
    }
}
