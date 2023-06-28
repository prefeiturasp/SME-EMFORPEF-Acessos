using Microsoft.AspNetCore.Mvc;
using SME.Acesos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Enumerados;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    [Route("api/v1/usuarios")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        [HttpPost("cadastrar")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Autenticar([FromBody] UsuarioDTO usuarioDto, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var retornoAutenticacao = await servicoUsuarios.Cadastrar(usuarioDto);
            
            return Ok(retornoAutenticacao);
        }
        
        [HttpGet("{login}/cadastrado")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]        
        public async Task<IActionResult> UsuarioCadastradoCoreSSO(string login, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var retorno = await servicoUsuarios.UsuarioCadastradoCoreSSO(login);

            return Ok(retorno);
        }
        
        [HttpPost("{login}/vincular-perfil/{perfilId}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]        
        public async Task<IActionResult> VincularPerfil(string login, Guid perfilId, [FromServices] IServicoUsuarioGrupo servicoUsuarioGrupo)
        {
            var retorno = await servicoUsuarioGrupo.VincularPerfil(login,perfilId);

            return Ok(retorno);
        }
        
        [HttpGet("{login}/sistemas/{sistemaId}/recuperar-senha")] 
        public async Task<IActionResult> SolicitarRecuperacaoSenha(string login, long sistemaId, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            return Ok(await servicoUsuarios.RecuperarSenha(login, sistemaId));
        }
        
        [HttpGet("{token}/sistemas/{sistemaId}/validar")] 
        public async Task<IActionResult> TokenRecuperacaoSenhaEstaValido( Guid token, long sistemaId, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            return Ok(await servicoUsuarios.ValidarTokenRecuperacaoSenha(token, sistemaId));
        }
        
        [HttpPut("sistemas/{sistemaId}/senha")] 
        public async Task<IActionResult> AlterarSenhaComTokenRecuperacao([FromRoute] long sistemaId,[FromBody] AlterarSenhaPorTokenDto alterarSenha, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var senhaAlterada = await servicoUsuarios.AlterarSenhaPorToken(sistemaId,alterarSenha);
            return TratarRetornoAlterarSenha(senhaAlterada);
        }
        
        private IActionResult TratarRetornoAlterarSenha(RetornoAlteracaoSenhaDto retornoAlterar)
        {
            switch (retornoAlterar.Status)
            {
                case AlterarSenhaStatus.TokenExpirado:
                    return Unauthorized(MensagemNegocio.TOKEN_INVALIDO_OU_EXPIRADO);
                case AlterarSenhaStatus.ForaPadrao:
                    return Unauthorized(MensagemNegocio.SENHA_FORA_DO_PADRAO);
                case AlterarSenhaStatus.NaoEncontrado:
                    return Unauthorized(MensagemNegocio.USUARIO_OU_SENHA_INCORRETOS);
                case AlterarSenhaStatus.OK:
                    return Ok(retornoAlterar.Login);
                case AlterarSenhaStatus.NoHistorico:
                    return Unauthorized(MensagemNegocio.A_SENHA_NAO_PODE_SER_UMA_DAS_ULTIMAS_5_ANTERIORES);
                case AlterarSenhaStatus.SenhaPadrao:
                    return Unauthorized(MensagemNegocio.A_NOVA_SENHA_NAO_PODE_SER_UMA_SENHA_PADRAO);
                default:
                    return Ok(retornoAlterar.Login);
            }
        }
    }
}
