using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElearningAcademy.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculumsController(ICurriculumService curriculumService) : ControllerBase
    {
        private readonly ICurriculumService _curriculumService = curriculumService;

        [HttpGet("")]
        public async Task<IActionResult> GetAllCurriculums(CancellationToken cancellationToken)
        {
            var curriculums = await _curriculumService.GetAllCurriculumsAsync(cancellationToken);
            return Ok(curriculums);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurriculumById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _curriculumService.GetCurriculumByIdAsync(id, cancellationToken);
            return result.IsSuccess 
                    ? Ok(result.Value)
                    : result.ToProblem();
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetCurriculumWithDetails([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _curriculumService.GetCurriculumWithDetailsByIdAsync(id, cancellationToken);
            return result.IsSuccess
                    ? Ok(result.Value)
                    : result.ToProblem();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurriculum([FromBody] CurriculumRequest request, CancellationToken cancellationToken)
        {
            var createdCurriculum = await _curriculumService.CreateCurriculumAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetCurriculumById), new { id = createdCurriculum.Id }, createdCurriculum);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurriculum([FromRoute] int id, [FromBody] CurriculumRequest request, CancellationToken cancellationToken)
        {
            var result = await _curriculumService.UpdateCurriculumAsync(id, request, cancellationToken);
            return result.IsSuccess
                    ? NoContent()
                    : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurriculum([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _curriculumService.DeleteCurriculumAsync(id, cancellationToken);
            return result.IsSuccess
                    ? NoContent()
                    : result.ToProblem();
        }
    }
}
