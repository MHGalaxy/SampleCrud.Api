using Microsoft.AspNetCore.Mvc;
using SampleCrud.Application.DTOs;
using SampleCrud.Application.Services;

namespace SampleCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvidersController : ControllerBase
{
    private readonly ProviderService _service;

    public ProvidersController(ProviderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProviderDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{providerId:int}")]
    public async Task<ActionResult<ProviderDto>> GetById(int providerId)
    {
        var provider = await _service.GetByIdAsync(providerId);
        return provider is null ? NotFound() : Ok(provider);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProviderDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { providerId = id }, null);
    }

    [HttpPut("{providerId:int}")]
    public async Task<ActionResult> Update(int providerId, UpdateProviderDto dto)
    {
        var ok = await _service.UpdateAsync(providerId, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{providerId:int}")]
    public async Task<ActionResult> Delete(int providerId)
    {
        var ok = await _service.DeleteAsync(providerId);
        return ok ? NoContent() : NotFound();
    }
}
