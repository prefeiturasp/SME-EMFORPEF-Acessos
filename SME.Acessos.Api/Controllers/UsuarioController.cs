using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Aplicacao.DTO;
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
    }
}
