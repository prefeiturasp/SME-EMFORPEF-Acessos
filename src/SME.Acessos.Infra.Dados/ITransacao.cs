using System.Data;

namespace SME.Acessos.Infra.Dados
{
    public interface ITransacao
    {
        IDbTransaction Iniciar();
        IDbTransaction Iniciar(IsolationLevel il);
    }
}
