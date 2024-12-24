using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Dtos;

namespace Skinet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
        return BadRequest();
    }

    [HttpGet("not-found")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
        throw new Exception("This is a server error");
    }

    [HttpPost("validation-error")]
    public ActionResult GetValidationError(CreateProductDto product)
    {
        return Ok(product);
    }
}
