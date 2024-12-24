using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Dtos;

namespace Skinet.API.Controllers;

[AllowAnonymous]
public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest();
    }

    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
        throw new Exception("This is a server error");
    }

    [HttpPost("validation-error")]
    public IActionResult GetValidationError(CreateProductDto product)
    {
        return Ok(product);
    }

    [Authorize]
    [HttpGet("secrets")]
    public IActionResult GetSecrets()
    {
        var user = User.FindFirst(ClaimTypes.Name)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok($"Hello, {user} with id of {id}");
    }
}
