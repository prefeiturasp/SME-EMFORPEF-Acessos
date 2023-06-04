using System.Data;

namespace SME.Acessos.Infra.Dados
{
    public interface IConexaoBase : IDisposable
    {
        void Abrir();
        void Fechar();
        IDbConnection Obter();
    }
}
