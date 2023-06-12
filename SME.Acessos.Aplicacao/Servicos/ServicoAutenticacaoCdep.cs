using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoAutenticacaoCdep : IServicoAutenticacaoCdep
    {
        private readonly IRepositorioPerfilUsuario repositorioPerfilUsuario;
        private readonly IRepositorioModuloGrupoPermissao repositorioModuloGrupoPermissao;
        private readonly IRepositorioPermissao repositorioPermissao;
        private readonly IServicoTokenJwt servicoTokenJwt;

        public ServicoAutenticacaoCdep(IRepositorioPerfilUsuario repositorioPerfilUsuario,IServicoTokenJwt servicoTokenJwt,IRepositorioModuloGrupoPermissao repositorioModuloGrupoPermissao,IRepositorioPermissao repositorioPermissao)
        {
            this.repositorioPerfilUsuario = repositorioPerfilUsuario ?? throw new ArgumentNullException(nameof(repositorioPerfilUsuario));
            this.servicoTokenJwt = servicoTokenJwt ?? throw new ArgumentNullException(nameof(servicoTokenJwt));
            this.repositorioModuloGrupoPermissao = repositorioModuloGrupoPermissao ?? throw new ArgumentNullException(nameof(repositorioModuloGrupoPermissao));
            this.repositorioPermissao = repositorioPermissao ?? throw new ArgumentNullException(nameof(repositorioPermissao));
        }

        public async Task<RetornoUsuarioCdepDto> ObterPerfisToken(RetornoAutenticacaoDto retornoAutenticacao)
        {
            var perfisUsuario = await repositorioPerfilUsuario.ObterPerfisUsuario(retornoAutenticacao.Login, (int)Sistema.Cdep);

            var perfilUsuario = perfisUsuario.FirstOrDefault();
            var modulos = await repositorioModuloGrupoPermissao.ObterModulosPorPerfilSistema(perfilUsuario.Id,(int)Sistema.Cdep);
            var permissoes = await repositorioPermissao.ObterPermissoesPorModulos(modulos);
            var codPermissoes = permissoes.ToList().Select(p => p);
            var token = servicoTokenJwt.GerarToken(retornoAutenticacao.Login, retornoAutenticacao.Nome, perfilUsuario.Id, codPermissoes);
            var dataExpiracaoToken = servicoTokenJwt.ObterDataHoraExpiracao();

            var retorno = new RetornoUsuarioCdepDto()
            {
                UsuarioLogin = retornoAutenticacao.Login,
                UsuarioNome = retornoAutenticacao.Nome,
                Email = retornoAutenticacao.Email,
                Token = token,
                PerfilUsuario = perfisUsuario.Select(s => new PerfilUsuarioDto()
                    { Perfil = s.Id, PerfilNome = s.Nome }).ToList(),
                DataHoraExpiracao = dataExpiracaoToken,
                Autenticado = true,
            };
            return retorno;
        }
    }
}
