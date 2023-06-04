using System.Data;

namespace SME.Acessos.Infra.Dados
{
    public class Transacao : ITransacao
    {
        private readonly IConexaoBase conexao;

        public Transacao(IConexaoBase conexao)
        {
            this.conexao = conexao;
        }

        public IDbTransaction Iniciar()
            => conexao.Obter().BeginTransaction();

        public IDbTransaction Iniciar(IsolationLevel il)
            => conexao.Obter().BeginTransaction(il);
    }
}
