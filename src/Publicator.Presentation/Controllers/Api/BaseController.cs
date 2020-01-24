using Microsoft.AspNetCore.Mvc;

namespace Publicator.Presentation.Controllers
{
    /// <summary>
    /// Base api controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        public BaseController() : base()
        {
        }
    }
}