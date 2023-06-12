using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO;
using SME.Acessos.Infra.Dominio.Acessos.Entidades;

namespace SME.Acessos.Infra.Dados
{
    public static class RegistrarMapeamentos
    {
        public static void Registrar()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UsuarioMap());
                config.AddMap(new GrupoMap());
                config.AddMap(new GrupoPermissaoMap());
                config.AddMap(new ModuloMap());
                config.AddMap(new PessoaMap());
                config.AddMap(new SistemaMap());
                config.AddMap(new VisaoMap());
                config.AddMap(new Mapeamentos.Acessos.ModuloMap());

                config.ForDommel();
            });
        }
    }
}
