using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoPerfilUsuario : IServicoPerfilUsuario
    {
        private readonly IRepositorioUsuarioGrupo repositorioUsuarioGrupo;
        private readonly IRepositorioGrupoPermissao repositorioGrupoPermissao;
        private readonly IRepositorioPermissao repositorioPermissao;
        private readonly IServicoTokenJwt servicoTokenJwt;

        public ServicoPerfilUsuario(IRepositorioUsuarioGrupo repositorioUsuarioGrupo,IServicoTokenJwt servicoTokenJwt,IRepositorioGrupoPermissao repositorioGrupoPermissao,IRepositorioPermissao repositorioPermissao)
        {
            this.repositorioUsuarioGrupo = repositorioUsuarioGrupo ?? throw new ArgumentNullException(nameof(repositorioUsuarioGrupo));
            this.servicoTokenJwt = servicoTokenJwt ?? throw new ArgumentNullException(nameof(servicoTokenJwt));
            this.repositorioGrupoPermissao = repositorioGrupoPermissao ?? throw new ArgumentNullException(nameof(repositorioGrupoPermissao));
            this.repositorioPermissao = repositorioPermissao ?? throw new ArgumentNullException(nameof(repositorioPermissao));
        }

        public async Task<RetornoPerfilUsuarioDTO> ObterPerfisToken(string login, int sistemaId)
        {
            var perfisUsuario = await repositorioUsuarioGrupo.ObterPerfisUsuario(login, sistemaId);

            var perfilUsuario = perfisUsuario.FirstOrDefault();
            var modulos = await repositorioGrupoPermissao.ObterModulosPorPerfilSistema(perfilUsuario.GrupoId,sistemaId);
            var permissoes = await repositorioPermissao.ObterPermissoesPorModulos(modulos);
            var codPermissoes = permissoes.ToList().Select(p => p.Id);
            var token = servicoTokenJwt.GerarToken(login, perfilUsuario.PessoaNome, perfilUsuario.Id, codPermissoes);
            var dataExpiracaoToken = servicoTokenJwt.ObterDataHoraExpiracao();

            var retorno = new RetornoPerfilUsuarioDTO()
            {
                UsuarioLogin = login,
                UsuarioNome = perfilUsuario.PessoaNome,
                Email = perfilUsuario.UsuarioEmail,
                Token = token,
                PerfilUsuario = perfisUsuario.Any() ? perfisUsuario.Select(s => new PerfilUsuarioDTO()
                    { Perfil = s.GrupoId, PerfilNome = s.GrupoNome }).ToList() : null,
                DataHoraExpiracao = dataExpiracaoToken,
                Autenticado = true,
            };
            return retorno;
        }
    }
}
