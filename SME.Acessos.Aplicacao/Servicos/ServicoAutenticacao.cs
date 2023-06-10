using System.Net;
using AutoMapper;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoAutenticacao : IServicoAutenticacao
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IRepositorioPerfilUsuario repositorioPerfilUsuario;
        private readonly IRepositorioModuloGrupoPermissao repositorioModuloGrupoPermissao;
        private readonly IRepositorioPermissao repositorioPermissao;
        private readonly IServicoTokenJwt servicoTokenJwt;

        public ServicoAutenticacao(IRepositorioUsuario repositorioUsuario,IRepositorioPerfilUsuario repositorioPerfilUsuario,IServicoTokenJwt servicoTokenJwt,IRepositorioModuloGrupoPermissao repositorioModuloGrupoPermissao,IRepositorioPermissao repositorioPermissao)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            this.repositorioPerfilUsuario = repositorioPerfilUsuario ?? throw new ArgumentNullException(nameof(repositorioPerfilUsuario));
            this.servicoTokenJwt = servicoTokenJwt ?? throw new ArgumentNullException(nameof(servicoTokenJwt));
            this.repositorioModuloGrupoPermissao = repositorioModuloGrupoPermissao ?? throw new ArgumentNullException(nameof(repositorioModuloGrupoPermissao));
            this.repositorioPermissao = repositorioPermissao ?? throw new ArgumentNullException(nameof(repositorioPermissao));
        }

        public async Task<RetornoUsuarioCdepDto> Autenticar(string login, string senha)
        {
            var usuarioCoreSSO = await repositorioUsuario.ObterPorLogin(login);
            if (usuarioCoreSSO == null)
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO, HttpStatusCode.Unauthorized);

            if (!CriptografiaExtensions.EqualsSenha(senha, usuarioCoreSSO.Senha, TipoCriptografia.TripleDES))
                throw new NegocioException(MensagemNegocio.USUARIO_OU_SENHA_INCORRETOS, HttpStatusCode.Unauthorized);

            var perfisUsuario = await repositorioPerfilUsuario.ObterPerfisUsuario(login, (int)Sistema.Cdep);

            var perfilUsuario = perfisUsuario.FirstOrDefault();
            var modulos = await repositorioModuloGrupoPermissao.ObterModulosPorPerfilSistema(perfilUsuario.Id,(int)Sistema.Cdep);
            var permissoes = await repositorioPermissao.ObterPermissoesPorModulos(modulos);
            var codPermissoes = permissoes.ToList().Select(p => p);
            var token = servicoTokenJwt.GerarToken(login, usuarioCoreSSO.Nome, perfilUsuario.Id, codPermissoes);
            var dataExpiracaoToken = servicoTokenJwt.ObterDataHoraExpiracao();

            var retorno = new RetornoUsuarioCdepDto()
            {
                UsuarioLogin = usuarioCoreSSO.Login,
                UsuarioNome = usuarioCoreSSO.Nome,
                Email = usuarioCoreSSO.Email,
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
