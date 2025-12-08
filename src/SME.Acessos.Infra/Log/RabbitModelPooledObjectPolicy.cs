using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace SME.Acessos.Infra.Servicos.Log
{
    public class RabbitModelPooledObjectPolicy(ConfiguracaoRabbit configuracaoRabbitOptions) : IPooledObjectPolicy<IModel>
    {
        private readonly IConnection conexao = GetConnection(configuracaoRabbitOptions ?? throw new ArgumentNullException(nameof(configuracaoRabbitOptions)));

        private static IConnection GetConnection(ConfiguracaoRabbit configuracaoRabbit)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuracaoRabbit.HostName,
                UserName = configuracaoRabbit.UserName,
                Password = configuracaoRabbit.Password,
                VirtualHost = configuracaoRabbit.VirtualHost
            };

            return factory.CreateConnection();
        }

        public IModel Create()
        {
            var channel = conexao.CreateModel();
            channel.ConfirmSelect();
            return channel;
        }

        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
                return true;
            else
            {
                obj?.Dispose();
                return false;
            }
        }
    }
}
