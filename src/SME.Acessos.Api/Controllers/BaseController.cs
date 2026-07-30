using Microsoft.AspNetCore.Mvc;
using SME.Acessos.Api.Middlewares;

namespace SME.Acessos.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ChaveIntegracaoApi]
    public class BaseController : ControllerBase
    {
    }
}
