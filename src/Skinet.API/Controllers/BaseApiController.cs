using Microsoft.AspNetCore.Mvc;
using Skinet.API.RequestHelpers;
using Skinet.Application.Interfaces;
using Skinet.Entities;

namespace Skinet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<IActionResult> CreatePagedResult<T>(
        IGenericRepository<T> repo,
        ISpecification<T> spec,
        int pageIndex,
        int pageSize
    )
        where T : BaseEntity
    {
        var data = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);
        var pagination = new Pagination<T>(pageIndex, pageSize, count, data);
        return Ok(pagination);
    }
}
