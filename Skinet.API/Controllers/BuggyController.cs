using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Dtos;

namespace Skinet.API.Controllers;

public class BuggyController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest();
    }

    [AllowAnonymous]
    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }

    [AllowAnonymous]
    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
        throw new Exception("This is a server error");
    }

    [AllowAnonymous]
    [HttpPost("validation-error")]
    public IActionResult GetValidationError(CreateProductDto product)
    {
        return Ok(product);
    }

    [HttpGet("secrets")]
    public IActionResult GetSecrets()
    {
        var user = User.FindFirst(ClaimTypes.Name)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok($"Hello, {user} with id of {id}");
    }
}
