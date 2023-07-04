using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Enumerados;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    public class UsuariosController : BaseController
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
        public async Task<IActionResult> UsuarioCadastradoCoreSSO([FromRoute] string login, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var retorno = await servicoUsuarios.UsuarioCadastradoCoreSSO(login);

            return Ok(retorno);
        }
        
        [HttpPost("{login}/vincular-perfil/{perfilId}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> VincularPerfil([FromRoute] string login, Guid perfilId, [FromServices] IServicoUsuarioGrupo servicoUsuarioGrupo)
        {
            var retorno = await servicoUsuarioGrupo.VincularPerfil(login,perfilId);

            return Ok(retorno);
        }
        
        [HttpGet("{login}")]
        [ProducesResponseType(typeof(DadosUsuarioDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(601)]
        public async Task<IActionResult> MeusDados([FromRoute] string login, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var retorno = await servicoUsuarios.ObterMeusDados(login);

            return Ok(retorno);
        }
        
        [HttpPut("{login}/senha")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AlterarSenha([FromRoute] string login,[FromBody] AlterarSenhaUsuarioDTO alterarSenhaUsuarioDto, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var retorno = await servicoUsuarios.AlterarSenha(login,alterarSenhaUsuarioDto);

            return Ok(retorno);
        }
        
        [HttpPut("{login}/email")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AlterarEmail([FromRoute] string login, [FromBody] AlterarEmailUsuarioDTO alterarEmailUsuarioDto, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var retorno = await servicoUsuarios.AlterarEmail(login, alterarEmailUsuarioDto);

            return Ok(retorno);
        }
        
        [HttpGet("{login}/sistemas/{sistemaId}/recuperar-senha")] 
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(601)]
        public async Task<IActionResult> SolicitarRecuperacaoSenha([FromRoute] string login, long sistemaId, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            return Ok(await servicoUsuarios.RecuperarSenha(login, sistemaId));
        }
        
        [HttpGet("{token}/sistemas/{sistemaId}/validar")] 
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> TokenRecuperacaoSenhaEstaValido([FromRoute] Guid token, long sistemaId, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            return Ok(await servicoUsuarios.ValidarTokenRecuperacaoSenha(token, sistemaId));
        }
        
        [HttpPut("sistemas/{sistemaId}/senha")] 
        [ProducesResponseType(typeof(RetornoAlteracaoSenhaDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(601)]
        public async Task<IActionResult> AlterarSenhaComTokenRecuperacao([FromRoute] long sistemaId,[FromBody] AlterarSenhaPorTokenDto alterarSenha, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var senhaAlterada = await servicoUsuarios.AlterarSenhaPorToken(sistemaId,alterarSenha);
            return TratarRetornoAlterarSenha(senhaAlterada);
        }
        
        private IActionResult TratarRetornoAlterarSenha(RetornoAlteracaoSenhaDto retornoAlterar)
        {
            return retornoAlterar.Status switch
            {
                AlterarSenhaStatus.TokenExpirado => Unauthorized(MensagemNegocio.TOKEN_INVALIDO_OU_EXPIRADO),
                AlterarSenhaStatus.ForaPadrao => Unauthorized(MensagemNegocio.SENHA_FORA_DO_PADRAO),
                AlterarSenhaStatus.NaoEncontrado => Unauthorized(MensagemNegocio.USUARIO_OU_SENHA_INCORRETOS),
                AlterarSenhaStatus.OK => Ok(retornoAlterar.Login),
                AlterarSenhaStatus.NoHistorico => Unauthorized(MensagemNegocio.A_SENHA_NAO_PODE_SER_UMA_DAS_ULTIMAS_5_ANTERIORES),
                AlterarSenhaStatus.SenhaPadrao => Unauthorized(MensagemNegocio.A_NOVA_SENHA_NAO_PODE_SER_UMA_SENHA_PADRAO),
                _ => Ok(retornoAlterar.Login)
            };
        }
    }
}
