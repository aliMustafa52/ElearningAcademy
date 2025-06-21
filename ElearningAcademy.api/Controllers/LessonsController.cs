using ElearningAcademy.api.Contracts.Lessons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElearningAcademy.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController(ILessonService lessonService) : ControllerBase
    {
        private readonly ILessonService _lessonService = lessonService;

        [HttpGet("api/topics/{topicId}/lessons")]
        public async Task<IActionResult> GetLessonsForTopic([FromRoute] int topicId, CancellationToken cancellationToken)
        {
            var lessons = await _lessonService.GetLessonsForTopicAsync(topicId, cancellationToken);
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonDetails([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _lessonService.GetLessonDetailsByIdAsync(id, cancellationToken);
            return result.IsSuccess
                    ? Ok(result.Value)
                    : result.ToProblem();
        }

        [HttpPost("api/topics/{topicId}/lessons")]
        public async Task<IActionResult> CreateLesson([FromRoute] int topicId, [FromForm] LessonRequest request, CancellationToken cancellationToken)
        {
            var result = await _lessonService.CreateLessonAsync(topicId, request, cancellationToken);

            return result.IsSuccess
                    ? CreatedAtAction(nameof(GetLessonDetails), new { id = result.Value.Id }, result.Value)
                    : result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson([FromRoute] int id, [FromForm] LessonRequest request, CancellationToken cancellationToken)
        {
            var result = await _lessonService.UpdateLessonAsync(id, request, cancellationToken);
            return result.IsSuccess
                    ? NoContent()
                    : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id, CancellationToken cancellationToken)
        {
            var result = await _lessonService.DeleteLessonAsync(id, cancellationToken);
            return result.IsSuccess
                    ? NoContent()
                    : result.ToProblem();
        }
    }
}
