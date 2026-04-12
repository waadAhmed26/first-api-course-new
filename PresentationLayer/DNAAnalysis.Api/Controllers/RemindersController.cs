using DNAAnalysis.API.Responses;
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
        var data = await _reminderService.GetAllAsync(GetUserId());

        return Ok(new ApiResponse<IEnumerable<ReminderDto>>(
            data,
            "Reminders retrieved successfully"
        ));
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus([FromRoute] ReminderStatus status)
    {
        var data = await _reminderService.GetByStatusAsync(GetUserId(), status);

        return Ok(new ApiResponse<IEnumerable<ReminderDto>>(
            data,
            "Reminders retrieved successfully"
        ));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var reminder = await _reminderService.GetByIdAsync(id, GetUserId());

        if (reminder == null)
        {
            return NotFound(new ApiResponse<string>(
                new[] { "Reminder not found" },
                "Not Found"
            ));
        }

        return Ok(new ApiResponse<ReminderDto>(
            reminder,
            "Reminder retrieved successfully"
        ));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReminderDto dto)
    {
        var result = await _reminderService.CreateAsync(GetUserId(), dto);

        return Ok(new ApiResponse<ReminderDto>(
            result,
            "Reminder created successfully"
        ));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReminderDto dto)
    {
        var result = await _reminderService.UpdateAsync(id, GetUserId(), dto);

        if (!result)
        {
            return NotFound(new ApiResponse<string>(
                new[] { "Reminder not found" },
                "Not Found"
            ));
        }

        return Ok(new ApiResponse<string>(
            "Reminder updated successfully",
            "Success"
        ));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _reminderService.DeleteAsync(id, GetUserId());

        if (!result)
        {
            return NotFound(new ApiResponse<string>(
                new[] { "Reminder not found" },
                "Not Found"
            ));
        }

        return Ok(new ApiResponse<string>(
            "Reminder deleted successfully",
            "Success"
        ));
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        var data = await _reminderService.GetByDateAsync(GetUserId(), date);

        return Ok(new ApiResponse<IEnumerable<ReminderDto>>(
            data,
            "Reminders retrieved successfully"
        ));
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete([FromRoute] int id)
    {
        var result = await _reminderService.MarkAsCompleteAsync(id, GetUserId());

        if (!result)
        {
            return NotFound(new ApiResponse<string>(
                new[] { "Reminder not found" },
                "Not Found"
            ));
        }

        return Ok(new ApiResponse<string>(
            "Reminder marked as completed",
            "Success"
        ));
    }
}