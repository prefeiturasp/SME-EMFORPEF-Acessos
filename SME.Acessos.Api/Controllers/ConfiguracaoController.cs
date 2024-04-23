using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    public class ConfiguracaoController : BaseController
    {
        [HttpGet("email/sistema/{sistemaId}")]
        [ProducesResponseType(typeof(ConfiguracaoEmailDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ObterGruposPorSistemaId([FromRoute] long sistemaId,
            [FromServices] IServicoEmail servicoEmail)
        {
            var configuracaoEmail = await servicoEmail.ObterConfiguracaoEmail(sistemaId);
            return Ok(configuracaoEmail);
        }
    }
}