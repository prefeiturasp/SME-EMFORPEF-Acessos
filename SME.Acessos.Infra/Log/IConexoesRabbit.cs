using RabbitMQ.Client;

namespace SME.Acessos.Infra.Servicos
{
    public interface IConexoesRabbit
    {
        IModel Get();
        void Return(IModel conexao);
    }

    public interface IConexoesRabbitAcessos : IConexoesRabbit { }
    public interface IConexoesRabbitLogs : IConexoesRabbit { }
}
