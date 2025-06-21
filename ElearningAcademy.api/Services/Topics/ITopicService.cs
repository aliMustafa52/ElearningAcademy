using ElearningAcademy.api.Contracts.Topics;

namespace ElearningAcademy.api.Services.Topics
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicResponse>> GetTopicsForCurriculumAsync(int curriculumId, CancellationToken cancellationToken = default);
        Task<Result<TopicResponse>> GetTopicByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<TopicResponse>> CreateTopicAsync(int curriculumId, TopicRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateTopicAsync(int id, TopicRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteTopicAsync(int id, CancellationToken cancellationToken = default);
    }
}
