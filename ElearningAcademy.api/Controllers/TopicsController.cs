using ElearningAcademy.api.Contracts.Topics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElearningAcademy.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(ITopicService topicService) : ControllerBase
    {
        private readonly ITopicService _topicService = topicService;

        [HttpGet("api/curriculums/{curriculumId}/topics")]
        public async Task<IActionResult> GetTopicsForCurriculum([FromRoute] int curriculumId, CancellationToken cancellationToken)
        {
            var topics = await _topicService.GetTopicsForCurriculumAsync(curriculumId, cancellationToken);
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _topicService.GetTopicByIdAsync(id, cancellationToken);
            return result.IsSuccess
                    ? Ok(result.Value)
                    : result.ToProblem();
        }

        [HttpPost("api/curriculums/{curriculumId}/topics")]
        public async Task<IActionResult> CreateTopic([FromRoute] int curriculumId, [FromBody] TopicRequest request, CancellationToken cancellationToken)
        {
            var result = await _topicService.CreateTopicAsync(curriculumId, request, cancellationToken);

            return result.IsSuccess
                    ? CreatedAtAction(nameof(GetTopicById), new { id = result.Value.Id }, result.Value)
                    : result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic([FromRoute] int id, [FromBody] TopicRequest request, CancellationToken cancellationToken)
        {
            var result = await _topicService.UpdateTopicAsync(id, request, cancellationToken);
            return result.IsSuccess
                    ? NoContent()
                    : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id, CancellationToken cancellationToken)
        {
            var result = await _topicService.DeleteTopicAsync(id, cancellationToken);
            return result.IsSuccess
                    ? NoContent()
                    : result.ToProblem();
        }
    }
}
