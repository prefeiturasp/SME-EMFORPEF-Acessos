using System.Net;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Enumeradores;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao.Servicos
{
    public class ServicoAutenticacao : IServicoAutenticacao
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public ServicoAutenticacao(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<RetornoAutenticacaoDTO> Autenticar(string login, string senha)
        {
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(senha))
                throw new NegocioException(MensagemNegocio.LOGIN_SENHA_SAO_OBRIGATORIOS, HttpStatusCode.BadRequest);
            
            var usuarioCoreSSO = await repositorioUsuario.ObterPorLogin(login);
            if (usuarioCoreSSO == null)
                throw new NegocioException(MensagemNegocio.USUARIO_NAO_ENCONTRADO, HttpStatusCode.Unauthorized);

            if (!CriptografiaExtensions.EqualsSenha(senha, usuarioCoreSSO.Senha, TipoCriptografia.TripleDES))
                throw new NegocioException(MensagemNegocio.USUARIO_OU_SENHA_INCORRETOS, HttpStatusCode.Unauthorized);
            
            var retorno = new RetornoAutenticacaoDTO()
            {
                Login = usuarioCoreSSO.Login,
                Nome = usuarioCoreSSO.Pessoa.Nome,
                Email = usuarioCoreSSO.Email
            };
            return retorno;
        }
    }
}
