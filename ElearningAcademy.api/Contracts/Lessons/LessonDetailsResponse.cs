namespace ElearningAcademy.api.Contracts.Lessons
{
    public record LessonDetailsResponse
    (
        int Id,
        string Title,
        IEnumerable<ContentResponse> Contents,
        IEnumerable<ImageResponse> Images
    );
}
