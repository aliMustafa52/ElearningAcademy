namespace ElearningAcademy.api.Services.CurriculumsService
{
    public interface ICurriculumService
    {
        Task<IEnumerable<CurriculumResponse>> GetAllCurriculumsAsync(CancellationToken cancellationToken = default);
        Task<Result<CurriculumResponse>> GetCurriculumByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<CurriculumResponseWithTopics>> GetCurriculumWithDetailsByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CurriculumResponse> CreateCurriculumAsync(CurriculumRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateCurriculumAsync(int id, CurriculumRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteCurriculumAsync(int id, CancellationToken cancellationToken = default);
    }
}
