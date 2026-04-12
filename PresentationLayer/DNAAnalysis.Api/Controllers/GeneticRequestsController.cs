using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DNAAnalysis.Services.Abstraction;
using DNAAnalysis.Shared.GeneticRequestDtos;
using DNAAnalysis.Api.Requests;
using DNAAnalysis.API.Responses;

namespace DNAAnalysis.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeneticRequestsController : ControllerBase
{
    private readonly IGeneticRequestService _service;

    public GeneticRequestsController(IGeneticRequestService service)
    {
        _service = service;
    }

    // ================= USER =================

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<object>>> Create([FromForm] CreateGeneticRequestFormDto form)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized(new ApiResponse<string>(
                new List<string> { "Unauthorized" }, "User not authenticated"));

        string? fatherPath;
        string? motherPath;
        string? childPath = null;

        // ✅ Save Father & Mother (Required)
        fatherPath = await SaveFileAsync(form.FatherFile!);
        motherPath = await SaveFileAsync(form.MotherFile!);

        // ✅ Save Child (Optional)
        if (form.ChildFile != null)
        {
            childPath = await SaveFileAsync(form.ChildFile);
        }

        var dto = new CreateGeneticRequestDto
{
    FatherFilePath = fatherPath,
    MotherFilePath = motherPath,
    ChildFilePath = childPath,
    TestType = form.TestType // ✅ مهم جداً
};

        var requestId = await _service.CreateRequestAsync(userId, dto);

        return Ok(new ApiResponse<object>(
            new { Id = requestId },
            "Genetic request created successfully"));
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<ActionResult<ApiResponse<object>>> GetMyRequests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized(new ApiResponse<string>(
                new List<string> { "Unauthorized" }, "User not authenticated"));

        var requests = await _service.GetUserRequestsAsync(userId);

        return Ok(new ApiResponse<object>(
            requests,
            "User requests retrieved successfully"));
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized(new ApiResponse<string>(
                new List<string> { "Unauthorized" }, "User not authenticated"));

        var request = await _service.GetByIdForUserAsync(id, userId, User.IsInRole("Admin"));

        if (request is null)
            return NotFound(new ApiResponse<string>(
                new List<string> { "Request not found" }, "Not Found"));

        return Ok(new ApiResponse<object>(
            request,
            "Request retrieved successfully"));
    }

    // ================= ADMIN =================

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> GetAll()
    {
        var requests = await _service.GetAllRequestsAsync();

        return Ok(new ApiResponse<object>(
            requests,
            "All requests retrieved successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateStatus(int id, UpdateRequestStatusDto dto)
    {
        await _service.UpdateStatusAsync(id, dto.Status);

        return Ok(new ApiResponse<string>(
            "Status updated successfully",
            "Update completed"));
    }

    // ================= PRIVATE =================

    private async Task<string> SaveFileAsync(IFormFile file)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Path.Combine("uploads", uniqueFileName);
    }
}