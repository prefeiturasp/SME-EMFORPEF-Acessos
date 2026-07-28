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
    public class ServicoPerfilUsuario(IRepositorioUsuarioGrupoPessoa repositorioUsuarioGrupoPessoa, IServicoTokenJwt servicoTokenJwt, IRepositorioGrupoPermissao repositorioGrupoPermissao, IRepositorioPermissao repositorioPermissao, IRepositorioUsuario repositorioUsuario) : IServicoPerfilUsuario
    {
        public async Task<RetornoPerfilUsuarioDTO> ObterPerfisToken(string login, int sistemaId, Guid? perfilUsuarioId = null)
        {
            string nomeUsuario, emailUsuario,cpfUsuario;
            string? nomeSocial;
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
                nomeSocial = perfilUsuario.NomeSocial;
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
                var usuarioCoreSSO = await repositorioUsuario.ObterPorLogin(login, true) ??
                    throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO, HttpStatusCode.Unauthorized);

                nomeUsuario = usuarioCoreSSO.Pessoa!.Nome;
                nomeSocial = usuarioCoreSSO.Pessoa.NomeSocial;
                emailUsuario = usuarioCoreSSO.Email;
                cpfUsuario = usuarioCoreSSO.Documento!.Numero;
            }

            var dres = await repositorioUsuario.ObterDresPorLoginEPerfil(login,perfilUsuarioId);
            
            var token = servicoTokenJwt.GerarToken(new(login, nomeSocial ?? nomeUsuario, sistemaId, perfilUsuarioId, codPermissoes,perfisUsuario, dres));
            var dataExpiracaoToken = servicoTokenJwt.ObterDataHoraExpiracao();

            var perfis = perfisUsuario.Select(s => new PerfilUsuarioDTO() { Perfil = s.GrupoId, PerfilNome = s.GrupoNome });

            var retorno = new RetornoPerfilUsuarioDTO()
            {
                UsuarioLogin = login,
                UsuarioNome = nomeUsuario,
                NomeSocial = nomeSocial,
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

        public async Task<IEnumerable<RetornoUsuriosPareceristasDTO>> ObterUsuariosPerfilPareceristasConecta()
        {
            var pareceristas = await repositorioUsuarioGrupoPessoa.ObterUsuariosPerfilPareceristasConecta() ?? Enumerable.Empty<UsuarioGrupoPessoa>();
            var retorno = Enumerable.Empty<RetornoUsuriosPareceristasDTO>();
            
            if(pareceristas.Any() )
                retorno = pareceristas?.Select(x => 
                new RetornoUsuriosPareceristasDTO() { Nome = x.PessoaNome, Login = x.Login, NomeSocial = x.NomeSocial });
            
            
            return retorno;
        }
    }
}
