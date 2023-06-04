namespace SME.Acessos.Infra.Dominio
{
    public interface IRepositorioBaseAcessos<TEntidade> : IRepositorioBase<TEntidade, long>
        where TEntidade : EntidadeBaseAcessos
    {
    }
}
