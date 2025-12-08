using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dominio.Acessos.Repositorios
{
    public interface IRepositorioBaseAcessos<TEntidade> : IRepositorioBase<TEntidade, long>
        where TEntidade : EntidadeBaseAcessos
    {
    }
}
