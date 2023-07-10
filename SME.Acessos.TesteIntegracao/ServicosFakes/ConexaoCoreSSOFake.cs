using System.Data.SqlClient;
using SME.Acessos.Infra.Dados;

namespace SME.Acessos.TesteIntegracao.ServicosFakes
{
    public class ConexaoCoreSSOFake : ConexaoBase, IConexaoCoreSSO
    {
        public ConexaoCoreSSOFake(string stringConexao)
        {
        }
    }
}
