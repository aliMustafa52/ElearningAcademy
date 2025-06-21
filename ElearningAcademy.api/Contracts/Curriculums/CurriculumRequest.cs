namespace ElearningAcademy.api.Contracts.Curriculums
{
    public record CurriculumRequest
    (
        string Title,
        string Summary,
        IEnumerable<string> Goals
    );
}
