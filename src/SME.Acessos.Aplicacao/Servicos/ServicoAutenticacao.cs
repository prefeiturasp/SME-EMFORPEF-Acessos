using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;
using SME.Acessos.Infra.Dominio.Extensoes;
using System.Net;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoAutenticacao(IRepositorioUsuario repositorioUsuario) : IServicoAutenticacao
    {
        public async Task<RetornoAutenticacaoDTO> Autenticar(string? login, string? senha)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
                throw new NegocioException(MensagemNegocio.LOGIN_SENHA_SAO_OBRIGATORIOS, HttpStatusCode.BadRequest);
            
            var usuarioCoreSSO = await repositorioUsuario.ObterPorLogin(login, true) ?? 
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO, HttpStatusCode.Unauthorized);

            if (!CriptografiaExtensions.EqualsSenha(senha, usuarioCoreSSO.Senha, usuarioCoreSSO.Criptografia))
                throw new NegocioException(MensagemNegocio.USUARIO_OU_SENHA_INCORRETOS, HttpStatusCode.Unauthorized);
            
            var retorno = new RetornoAutenticacaoDTO()
            {
                Login = usuarioCoreSSO.Login,
                Nome = usuarioCoreSSO.Pessoa?.Nome ?? "",
                NomeSocial = usuarioCoreSSO.Pessoa?.NomeSocial,
                Email = usuarioCoreSSO.Email,
                Cpf = usuarioCoreSSO.Documento?.Numero ?? ""
            };
            return retorno;
        }
    }
}
