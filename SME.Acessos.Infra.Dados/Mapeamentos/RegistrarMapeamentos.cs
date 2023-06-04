using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using SME.Acessos.Infra.Dados.Mapeamentos.CoreSSO;

namespace SME.Acessos.Infra.Dados
{
    public static class RegistrarMapeamentos
    {
        public static void Registrar()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UsuarioMap());

                config.ForDommel();
            });
        }
    }
}
