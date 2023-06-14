using System.Net;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.Acessos.Repositorios;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoPerfilUsuario : IServicoPerfilUsuario
    {
        private readonly IRepositorioUsuarioGrupo repositorioUsuarioGrupo;
        private readonly IRepositorioGrupoPermissao repositorioGrupoPermissao;
        private readonly IRepositorioPermissao repositorioPermissao;
        private readonly IServicoTokenJwt servicoTokenJwt;
        private const string PERFIL_EXTERNO_DESCRICAO = "Externo";
        private const string PERFIL_EXTERNO_GUID = "3092428D-CA98-4788-9717-E706DF1945A0";
        private readonly IRepositorioUsuario repositorioUsuario;

        public ServicoPerfilUsuario(IRepositorioUsuarioGrupo repositorioUsuarioGrupo,IServicoTokenJwt servicoTokenJwt,IRepositorioGrupoPermissao repositorioGrupoPermissao,IRepositorioPermissao repositorioPermissao,IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuarioGrupo = repositorioUsuarioGrupo ?? throw new ArgumentNullException(nameof(repositorioUsuarioGrupo));
            this.servicoTokenJwt = servicoTokenJwt ?? throw new ArgumentNullException(nameof(servicoTokenJwt));
            this.repositorioGrupoPermissao = repositorioGrupoPermissao ?? throw new ArgumentNullException(nameof(repositorioGrupoPermissao));
            this.repositorioPermissao = repositorioPermissao ?? throw new ArgumentNullException(nameof(repositorioPermissao));
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<RetornoPerfilUsuarioDTO> ObterPerfisToken(string login, int sistemaId)
        {
            string nomeUsuario, emailUsuario;
            Guid perfilUsuarioId;
            var codPermissoes = Enumerable.Empty<long>();

            var perfisUsuario = await repositorioUsuarioGrupo.ObterPerfisUsuario(login, sistemaId);
            if (perfisUsuario.Any())
            {
                var perfilUsuario = perfisUsuario.FirstOrDefault();
                var modulos = await repositorioGrupoPermissao.ObterModulosPorPerfilSistema(perfilUsuario.GrupoId,sistemaId);
                var permissoes = await repositorioPermissao.ObterPermissoesPorModulos(modulos);
                codPermissoes = permissoes.ToList().Select(p => p.Id);
                nomeUsuario = perfilUsuario.PessoaNome;
                emailUsuario = perfilUsuario.UsuarioEmail;
                perfilUsuarioId = perfilUsuario.GrupoId;
            }
            else
            {
                var usuarioCoreSSO = await repositorioUsuario.ObterPorLogin(login);
                if (usuarioCoreSSO == null)
                    throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO, HttpStatusCode.Unauthorized);

                nomeUsuario = usuarioCoreSSO.Pessoa.Nome;
                emailUsuario = usuarioCoreSSO.Email;
                perfilUsuarioId = new Guid(PERFIL_EXTERNO_GUID);
            }
            
            var token = servicoTokenJwt.GerarToken(login, nomeUsuario, perfilUsuarioId, codPermissoes);
            var dataExpiracaoToken = servicoTokenJwt.ObterDataHoraExpiracao();

            var retorno = new RetornoPerfilUsuarioDTO()
            {
                UsuarioLogin = login,
                UsuarioNome = nomeUsuario,
                Email = emailUsuario,
                Token = token,
                PerfilUsuario = perfisUsuario.Any() 
                    ? perfisUsuario.Select(s => 
                        new PerfilUsuarioDTO() { Perfil = s.GrupoId, PerfilNome = s.GrupoNome }).ToList() 
                    : ObterPerfilExterno(),
                DataHoraExpiracao = dataExpiracaoToken,
                Autenticado = true,
            };
            return retorno;
        }

        private IList<PerfilUsuarioDTO> ObterPerfilExterno()
        {
            return new List<PerfilUsuarioDTO>() { new (new Guid(PERFIL_EXTERNO_GUID), PERFIL_EXTERNO_DESCRICAO) };
        }
    }
}
