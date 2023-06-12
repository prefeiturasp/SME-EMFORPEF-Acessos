using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    [Route("api/v1/autenticacao-cdep")]
    [ApiController]
    public class AutenticacaoCdepController : BaseController
    {
        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(RetornoUsuarioCdepDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Autenticar([FromBody] AutenticacaoDTO autenticacaoDto, [FromServices] IServicoAutenticacao servicoAutenticacao, [FromServices] IServicoAutenticacaoCdep servicoAutenticacaoCdep)
        {
            var retornoAutenticacao = await servicoAutenticacao.Autenticar(autenticacaoDto.Login, autenticacaoDto.Senha);

            var retornoUsuarioCdepDto = await servicoAutenticacaoCdep.ObterPerfisToken(retornoAutenticacao);
            
            return Ok(retornoUsuarioCdepDto);
        }
    }
}
