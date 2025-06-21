namespace ElearningAcademy.api.Contracts.Lessons
{
    public record LessonRequest
    (
        string Title,
        IEnumerable<string>? LessonContents,
        IFormFileCollection? LessonImages
    );
}
