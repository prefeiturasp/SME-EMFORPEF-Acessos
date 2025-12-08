using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace SME.Acessos.Infra.Servicos.Log
{
    public class ConexoesRabbit : IConexoesRabbit
    {
        public ObjectPool<IModel> pool { get; set; }

        protected ConexoesRabbit(ConfiguracaoRabbit configuracaoRabbit, ObjectPoolProvider poolProvider)
        {
            var policy = new RabbitModelPooledObjectPolicy(configuracaoRabbit);

            pool = poolProvider.Create(policy);
        }

        public IModel Get()
            => pool.Get();

        public void Return(IModel conexao)
            => pool.Return(conexao);
    }


    public class ConexoesRabbitAcessos(ConfiguracaoRabbitOptions configuracaoRabbit, ObjectPoolProvider poolProvider) : ConexoesRabbit(configuracaoRabbit, poolProvider), IConexoesRabbitAcessos
    {
    }

    public class ConexoesRabbitLogs(ConfiguracaoRabbitLogsOptions configuracaoRabbit, ObjectPoolProvider poolProvider) : ConexoesRabbit(configuracaoRabbit, poolProvider), IConexoesRabbitLogs
    {
    }
}