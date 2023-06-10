using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Infra.Dados
{
    public class RepositorioQuery
    {
        protected IConexaoBase conexao;

        public RepositorioQuery(IConexaoBase conexao)
        {
            this.conexao = conexao;
        }
        
        protected T QueryFirstOrDefaultSQL<T>(SqlConnection conn, string query, object param)
        {
            return conn.QueryFirstOrDefault<T>(query, param);
        }

        protected async Task<T> QueryFirstOrDefaultSQLAsync<T>(SqlConnection conn, string query, object param)
        {
            return await conn.QueryFirstOrDefaultAsync<T>(query, param);
        }

        protected IEnumerable<T> QueryCollectionSQL<T>(SqlConnection conn, string query, object param)
        {
            return conn.Query<T>(query, param, null, true, 120);
        }

        protected Task<IEnumerable<T>> QueryCollectionSQLAsync<T>(SqlConnection conn, string query, object param)
        {
            return conn.QueryAsync<T>(query, param, null, 120);
        }

        protected T QueryFirstOrDefaultSQLParameterless<T>(SqlConnection conn, string query)
        {
            return conn.QueryFirstOrDefault<T>(query, null, null, 120);
        }

        protected IEnumerable<T> QueryCollectionSQLParameterless<T>(SqlConnection conn, string query)
        {
            return conn.Query<T>(query, null, null, true, 120);
        }

        protected async Task<IEnumerable<T>> QueryCollectionSQLParameterlessAsync<T>(SqlConnection conn, string query)
        {
            return await conn.QueryAsync<T>(query, null, null, 120, System.Data.CommandType.Text);
        }

        protected Task<T> QueryFirstOrDefault<T>(IDbConnection conn, string query, object param)
        {
            return conn.QueryFirstOrDefaultAsync<T>(query, param);
        }

        protected string MontaQuery(string query, string where, string order = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine(query);

            sb.AppendLine(where);

            if (order.IsNotNull())
            {
                sb.AppendLine(order);
            }

            return sb.ToString();
        }

        protected string MontaQueryUnion(string query, string where, string order = "")
        {
            var sb = new StringBuilder();

            sb.Append(string.Format(query, where));

            if (order.IsNotNull())
            {
                sb.AppendLine(order);
            }

            return sb.ToString();
        }
    }
}