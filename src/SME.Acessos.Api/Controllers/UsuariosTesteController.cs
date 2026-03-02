using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    public class UsuariosTesteController(IServicoUsuarioTeste servicoUsuarioTeste, IWebHostEnvironment environment) : BaseController
    {
        [HttpPost("cadastrar-em-massa")]
        [ApiExplorerSettings(IgnoreApi = true)] // Oculta a rota da documentação do Swagger
        public async Task<IActionResult> CadastrarUsuariosEmMassa(int quantidade, Guid? perfilId)
        {
            // Trava de segurança absoluta: Aborta se entrar em Produção por engano
            if (environment.IsProduction())
            {
                return NotFound("Endpoint de testes indisponível neste ambiente.");
            }
            if (quantidade > 500)
            {
                return BadRequest("Para testes de carga, a quantidade máxima por lote deve ser de 500 usuários. Aumente o número de Virtual Users (VUs) na sua ferramenta de QA.");
            }

            var usuariosCadastrados = await servicoUsuarioTeste.CadastrarUsuariosEmMassaAsync(quantidade, perfilId);
            return Ok(usuariosCadastrados);
        }
    }
}
