using ElearningAcademy.api.Contracts.Lessons;

namespace ElearningAcademy.api.Services.Lessons
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonSummaryResponse>> GetLessonsForTopicAsync(int topicId, CancellationToken cancellationToken = default);
        Task<Result<LessonDetailsResponse>> GetLessonDetailsByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<LessonDetailsResponse>> CreateLessonAsync(int topicId, LessonRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateLessonAsync(int id, LessonRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteLessonAsync(int id, CancellationToken cancellationToken = default);
    }
}
