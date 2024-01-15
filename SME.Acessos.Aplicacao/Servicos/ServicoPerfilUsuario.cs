using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;
using System.Net;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoPerfilUsuario : IServicoPerfilUsuario
    {
        private readonly IRepositorioUsuarioGrupoPessoa repositorioUsuarioGrupoPessoa;
        private readonly IRepositorioGrupoPermissao repositorioGrupoPermissao;
        private readonly IRepositorioPermissao repositorioPermissao;
        private readonly IServicoTokenJwt servicoTokenJwt;
        private readonly IRepositorioUsuario repositorioUsuario;

        public ServicoPerfilUsuario(IRepositorioUsuarioGrupoPessoa repositorioUsuarioGrupoPessoa, IServicoTokenJwt servicoTokenJwt, IRepositorioGrupoPermissao repositorioGrupoPermissao, IRepositorioPermissao repositorioPermissao, IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuarioGrupoPessoa = repositorioUsuarioGrupoPessoa ?? throw new ArgumentNullException(nameof(repositorioUsuarioGrupoPessoa));
            this.servicoTokenJwt = servicoTokenJwt ?? throw new ArgumentNullException(nameof(servicoTokenJwt));
            this.repositorioGrupoPermissao = repositorioGrupoPermissao ?? throw new ArgumentNullException(nameof(repositorioGrupoPermissao));
            this.repositorioPermissao = repositorioPermissao ?? throw new ArgumentNullException(nameof(repositorioPermissao));
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<RetornoPerfilUsuarioDTO> ObterPerfisToken(string login, int sistemaId, Guid? perfilUsuarioId = null)
        {
            string nomeUsuario, emailUsuario,cpfUsuario;
            var codPermissoes = Enumerable.Empty<long>();

            var perfisUsuario = await repositorioUsuarioGrupoPessoa.ObterPerfisUsuario(login, sistemaId) ?? Enumerable.Empty<UsuarioGrupoPessoa>();
            if (perfisUsuario.Any())
            {
                UsuarioGrupoPessoa perfilUsuario;
                if (perfilUsuarioId.HasValue)
                    perfilUsuario = perfisUsuario.FirstOrDefault(t => t.GrupoId == perfilUsuarioId) ??
                        throw new NegocioException($"Perfil {perfilUsuarioId} não encontrado para o usuário {login}");
                else
                    perfilUsuario = perfisUsuario.FirstOrDefault() ??
                        throw new NegocioException($"Nenhum Perfil encontrado para o usuário");

                nomeUsuario = perfilUsuario.PessoaNome;
                emailUsuario = perfilUsuario.UsuarioEmail;
                perfilUsuarioId = perfilUsuario.GrupoId;
                cpfUsuario = perfilUsuario.Cpf;

                var modulos = await repositorioGrupoPermissao.ObterModulosPorPerfilSistema(perfilUsuarioId.Value, sistemaId);
                if (modulos != null && modulos.Any())
                {
                    var permissoes = await repositorioPermissao.ObterPermissoesPorModulos(modulos);
                    codPermissoes = permissoes.ToList().Select(p => p.Id);
                }
            }
            else
            {
                var usuarioCoreSSO = await repositorioUsuario.ObterPorLogin(login) ??
                    throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO, HttpStatusCode.Unauthorized);

                nomeUsuario = usuarioCoreSSO.Pessoa.Nome;
                emailUsuario = usuarioCoreSSO.Email;
                cpfUsuario = usuarioCoreSSO.Documento.Numero;
            }

            var dres = await repositorioUsuario.ObterDresPorLoginEPerfil(login,perfilUsuarioId);
            
            var token = servicoTokenJwt.GerarToken(login, nomeUsuario, sistemaId, perfilUsuarioId, codPermissoes,perfisUsuario, dres);
            var dataExpiracaoToken = servicoTokenJwt.ObterDataHoraExpiracao();

            var perfis = perfisUsuario.Select(s => new PerfilUsuarioDTO() { Perfil = s.GrupoId, PerfilNome = s.GrupoNome });

            var retorno = new RetornoPerfilUsuarioDTO()
            {
                UsuarioLogin = login,
                UsuarioNome = nomeUsuario,
                Cpf = cpfUsuario,
                Email = emailUsuario,
                Token = token,
                PerfilUsuario = perfis,
                DataHoraExpiracao = dataExpiracaoToken,
                Autenticado = true,
            };
            return retorno;
        }

        public Task<RetornoPerfilUsuarioDTO> Revalidar(string token)
        {
            var dadosUsuario = servicoTokenJwt.ObterDadosToken(token);
            return ObterPerfisToken(dadosUsuario.Login, dadosUsuario.Sistema, dadosUsuario.Perfil);
        }
    }
}
