namespace ElearningAcademy.api.Contracts.Curriculums
{
    public record CurriculumResponse
    (
        int Id,
        string Title,
        string Summary,
        IEnumerable<string> Goals
    );
}
