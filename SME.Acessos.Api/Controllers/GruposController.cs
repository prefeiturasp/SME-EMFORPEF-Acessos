using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    public class GruposController : BaseController
    {
        [HttpGet("sistema/{sistemaId}")]
        [ProducesResponseType(typeof(GrupoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ObterGruposPorSistemaId([FromRoute] long sistemaId, [FromServices] IServicoGrupos servicoGrupo)
        {
            var grupos = await servicoGrupo.ObterGruposPorSistemaId(sistemaId);
            return Ok(grupos);
        }
    }
}
