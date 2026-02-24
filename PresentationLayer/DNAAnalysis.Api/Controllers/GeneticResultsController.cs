using DNAAnalysis.Services.Abstraction;
using DNAAnalysis.Shared.GeneticResultDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNAAnalysis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneticResultsController : ControllerBase
    {
        private readonly IGeneticResultService _resultService;

        public GeneticResultsController(IGeneticResultService resultService)
        {
            _resultService = resultService;
        }

        // 🧠 Endpoint للـ AI يحط النتيجة
        [HttpPost("{requestId}")]
        public async Task<IActionResult> AddResult(int requestId, CreateGeneticResultDto dto)
        {
            await _resultService.AddResultAsync(requestId, dto);
            return Ok("Result added successfully");
        }

        // 👤 Endpoint للـ User يشوف النتيجة
        [Authorize]
        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetResult(int requestId)
        {
            var result = await _resultService.GetResultByRequestIdAsync(requestId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}