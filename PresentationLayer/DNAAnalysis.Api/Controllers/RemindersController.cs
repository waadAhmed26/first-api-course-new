using DNAAnalysis.Services.Abstraction;
using DNAAnalysis.Shared.DTOs.Alarm;
using DNAAnalysis.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DNAAnalysis.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RemindersController : ControllerBase
{
    private readonly IReminderService _reminderService;

    public RemindersController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _reminderService.GetAllAsync(GetUserId()));
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(ReminderStatus status)
    {
        return Ok(await _reminderService.GetByStatusAsync(GetUserId(), status));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reminder = await _reminderService.GetByIdAsync(id, GetUserId());

        if (reminder == null)
            return NotFound();

        return Ok(reminder);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReminderDto dto)
    {
        return Ok(await _reminderService.CreateAsync(GetUserId(), dto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateReminderDto dto)
    {
        var result = await _reminderService.UpdateAsync(id, GetUserId(), dto);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _reminderService.DeleteAsync(id, GetUserId());

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        return Ok(await _reminderService.GetByDateAsync(GetUserId(), date));
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var result = await _reminderService.MarkAsCompleteAsync(id, GetUserId());

        if (!result)
            return NotFound();

        return NoContent();
    }
}