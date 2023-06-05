namespace SME.Acessos.Infra.Dominio
{
    public interface IRepositorioBaseCoreSSO<TEntidade> : IRepositorioBase<TEntidade, Guid>
        where TEntidade : EntidadeBaseCoreSSO
    {
    }
}
