using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.Interfaces;

namespace SME.Acessos.Api.Controllers
{
    public class UsuariosTesteController(IServicoUsuarioTeste servicoUsuarioTeste, IWebHostEnvironment environment) : BaseController
    {
        [HttpPost("cadastrar-em-massa")]
        public async Task<IActionResult> CadastrarUsuariosEmMassa(int quantidade, Guid? perfilId)
        {
            // Trava de segurança absoluta: Aborta se entrar em Produção por engano
            if (environment.IsProduction())
            {
                return NotFound("Endpoint de testes indisponível neste ambiente.");
            }
            if (quantidade > 5000)
            {
                return BadRequest("Para testes de carga, a quantidade máxima por lote deve ser de 5000 usuários.");
            }

            var usuariosCadastrados = await servicoUsuarioTeste.CadastrarUsuariosEmMassaAsync(quantidade, perfilId);
            return Ok(usuariosCadastrados);
        }

        [HttpDelete("excluir-em-massa")]
        public async Task<IActionResult> ExcluirUsuariosEmMassa([FromBody] IEnumerable<string> logins)
        {
            // Trava de segurança absoluta: Retorna 404 em Produção para ofuscar o endpoint
            if (environment.IsProduction())
                return NotFound();

            if (logins == null || !logins.Any())
                return BadRequest("A lista de logins não pode ser vazia.");

            // Repassando diretamente, assumindo que você criou esse método na IServicoUsuarios
            await servicoUsuarioTeste.ExcluirUsuariosEmMassaAsync(logins);

            return NoContent(); // 204 - Deletado com sucesso
        }
    }
}
