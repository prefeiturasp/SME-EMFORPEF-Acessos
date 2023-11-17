using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    [Route("api/v1/autenticacao")]
    [ApiController]
    public class AutenticacaoController : BaseController
    {
        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(RetornoPerfilUsuarioDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Autenticar([FromBody] AutenticacaoDTO autenticacaoDto, [FromServices] IServicoAutenticacao servicoAutenticacao)
        {
            var retornoAutenticacao = await servicoAutenticacao.Autenticar(autenticacaoDto.Login, autenticacaoDto.Senha);

            return Ok(retornoAutenticacao);
        }

        [HttpGet("usuarios/{login}/sistemas/{sistemaId}/perfis")]
        [ProducesResponseType(typeof(RetornoPerfilUsuarioDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ListarPerfisUsuario(
            [FromServices] IServicoPerfilUsuario servicoPerfilUsuario,
            string login, int sistemaId)
        {
            var retorno = await servicoPerfilUsuario.ObterPerfisToken(login, sistemaId);

            return Ok(retorno);
        }

        [HttpGet("usuarios/{login}/sistemas/{sistemaId}/perfis/{perfilUsuarioId}")]
        [ProducesResponseType(typeof(RetornoPerfilUsuarioDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ListarPerfisUsuario(
            [FromServices] IServicoPerfilUsuario servicoPerfilUsuario,
            string login, int sistemaId, Guid perfilUsuarioId)
        {
            var retorno = await servicoPerfilUsuario.ObterPerfisToken(login, sistemaId, perfilUsuarioId);

            return Ok(retorno);
        }
    }
}
