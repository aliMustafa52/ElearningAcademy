using ElearningAcademy.api.Contracts.Topics;

namespace ElearningAcademy.api.Contracts.Curriculums
{
    public record CurriculumResponseWithTopics
    (
        int Id,
        string Title,
        string Summary,
        IEnumerable<string> Goals,
        IEnumerable<TopicResponse> Topics
    );
}
