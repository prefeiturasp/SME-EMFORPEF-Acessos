using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.Interfaces;
using SME.Acessos.Infra.Dominio.CoreSSO;

namespace SME.Acessos.Api.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("usuarios")]
        [ProducesResponseType(typeof(IList<Usuario>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ObterUsuariosCoreSSO([FromServices]IServicoUsuarios servicoUsuarios)
        {
            var usuarios = await servicoUsuarios.ObterTodosUsuarios();

            if (usuarios?.Any() != true)
                return NoContent();

            return Ok(usuarios);
        }

        [HttpGet("usuarios/{id}")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ObterUsuariosCoreSSO(Guid id, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var usuario = await servicoUsuarios.ObterUsuarioPorId(id);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }

        [HttpGet("usuarios/login/{login}")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ObterUsuariosCoreSSO(string login, [FromServices] IServicoUsuarios servicoUsuarios)
        {
            var usuario = await servicoUsuarios.ObterUsuarioPorLogin(login);

            if (usuario == null)
                return NoContent();

            return Ok(usuario);
        }
    }
}
