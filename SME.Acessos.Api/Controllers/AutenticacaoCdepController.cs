using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.Constantes;
using SME.Acessos.Aplicacao.DTO;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Api.Controllers
{
    [Route("api/v1/autenticacao-cdep")]
    [ApiController]
    public class AutenticacaoCdepController : BaseController
    {
        private readonly IServicoAutenticacao servicoAutenticacao;
        
        public AutenticacaoCdepController(IServicoAutenticacao servicoAutenticacao)
        {
            this.servicoAutenticacao = servicoAutenticacao ?? throw new ArgumentNullException(nameof(servicoAutenticacao));
        }
        
        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(RetornoUsuarioCdepDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Autenticar([FromForm] string login, [FromForm] string senha)
        {
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(senha))
                return BadRequest(MensagemNegocio.LOGIN_SENHA_SAO_OBRIGATORIOS);
            
            var retornoAutenticacao = await servicoAutenticacao.Autenticar(login, senha);
            
            return Ok(retornoAutenticacao);
        }
    }
}
