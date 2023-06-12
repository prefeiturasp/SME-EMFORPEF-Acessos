using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoAutenticacaoCdep : IServicoAutenticacaoCdep
    {
        private readonly IRepositorioPerfilUsuario repositorioPerfilUsuario;
        private readonly IRepositorioGrupoPermissao repositorioGrupoPermissao;
        private readonly IRepositorioPermissao repositorioPermissao;
        private readonly IServicoTokenJwt servicoTokenJwt;

        public ServicoAutenticacaoCdep(IRepositorioPerfilUsuario repositorioPerfilUsuario,IServicoTokenJwt servicoTokenJwt,IRepositorioGrupoPermissao repositorioGrupoPermissao,IRepositorioPermissao repositorioPermissao)
        {
            this.repositorioPerfilUsuario = repositorioPerfilUsuario ?? throw new ArgumentNullException(nameof(repositorioPerfilUsuario));
            this.servicoTokenJwt = servicoTokenJwt ?? throw new ArgumentNullException(nameof(servicoTokenJwt));
            this.repositorioGrupoPermissao = repositorioGrupoPermissao ?? throw new ArgumentNullException(nameof(repositorioGrupoPermissao));
            this.repositorioPermissao = repositorioPermissao ?? throw new ArgumentNullException(nameof(repositorioPermissao));
        }

        public async Task<RetornoUsuarioCdepDTO> ObterPerfisToken(RetornoAutenticacaoDTO retornoAutenticacao)
        {
            var perfisUsuario = await repositorioPerfilUsuario.ObterPerfisUsuario(retornoAutenticacao.Login, (int)Sistema.Cdep);

            var perfilUsuario = perfisUsuario.FirstOrDefault();
            var modulos = await repositorioGrupoPermissao.ObterModulosPorPerfilSistema(perfilUsuario.Id,(int)Sistema.Cdep);
            var permissoes = await repositorioPermissao.ObterPermissoesPorModulos(modulos);
            var codPermissoes = permissoes.ToList().Select(p => p.Id);
            var token = servicoTokenJwt.GerarToken(retornoAutenticacao.Login, retornoAutenticacao.Nome, perfilUsuario.Id, codPermissoes);
            var dataExpiracaoToken = servicoTokenJwt.ObterDataHoraExpiracao();

            var retorno = new RetornoUsuarioCdepDTO()
            {
                UsuarioLogin = retornoAutenticacao.Login,
                UsuarioNome = retornoAutenticacao.Nome,
                Email = retornoAutenticacao.Email,
                Token = token,
                PerfilUsuario = perfisUsuario.Select(s => new PerfilUsuarioDTO()
                    { Perfil = s.Id, PerfilNome = s.Nome }).ToList(),
                DataHoraExpiracao = dataExpiracaoToken,
                Autenticado = true,
            };
            return retorno;
        }
    }
}
