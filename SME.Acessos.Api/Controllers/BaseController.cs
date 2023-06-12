using Microsoft.AspNetCore.Mvc;

namespace SME.Acessos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ChaveIntegracaoApi]
    public class BaseController : ControllerBase
    {
    }
}
