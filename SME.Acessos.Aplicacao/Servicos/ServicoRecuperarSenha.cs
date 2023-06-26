using AutoMapper;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO.Entidades;
using SME.Acessos.Infra.Dominio.CoreSSO.Repositorios;
using SME.Acessos.Infra.Dominio.Extensions;

namespace SME.Acessos.Aplicacao
{
    public class ServicoRecuperarSenha : IServicoRecuperarSenha
    {
        private readonly IRepositorioSistemaRecuperacaoSenha sistemaRecuperacaoSenha;

        public ServicoRecuperarSenha(ISistemaRecuperacaoSenha sistemaRecuperacaoSenha)
        {
            this.sistemaRecuperacaoSenha = sistemaRecuperacaoSenha ?? throw new ArgumentNullException(nameof(sistemaRecuperacaoSenha));
        }


        public async Task<string> RecuperarSenha(string login, int sistema)
        {
            var sistemaRecuperacao = await sistemaRecuperacaoSenhaRepository.ObterSistema(sistema);
            if (sistemaRecuperacao is null)
                throw new NegocioException("O sistema informado não foi identificado na base de integração do EOL");

            var usuarioCore = await autenticacaoSGPService.CarregarDadosDoUsuarioAsync(login);

            if (usuarioCore == null)
                throw new NegocioException("Usuário ou RF não encontrado");

            var usuario = await usuarioService.ObterUsuarioOuAdiciona(login);

            usuario.IniciarRecuperacaoDeSenha(usuarioCore.Email);
            await usuarioService.Salvar(usuario);

            EnviarEmailRecuperacao(usuarioCore, usuario.TokenRecuperacaoSenha.Value, sistemaRecuperacao, login);
            return usuarioCore.Email;
        }
    }
}
