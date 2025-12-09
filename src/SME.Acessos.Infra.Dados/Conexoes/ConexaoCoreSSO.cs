using Microsoft.Data.SqlClient;

namespace SME.Acessos.Infra.Dados
{
    public class ConexaoCoreSSO : ConexaoBase, IConexaoCoreSSO
    {
        public ConexaoCoreSSO(string stringConexao)
        {
            conexao = new SqlConnection(stringConexao);
            Abrir();
        }
    }
}
