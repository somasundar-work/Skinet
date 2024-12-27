using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FallbackController : BaseApiController
    {
        public IActionResult Index()
        {
            return PhysicalFile(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
                "text/html"
            );
        }
    }
}
