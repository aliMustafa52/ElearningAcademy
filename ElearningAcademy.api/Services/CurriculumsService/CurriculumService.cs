using Azure;
using System.Threading;

namespace ElearningAcademy.api.Services.CurriculumsService
{
    public class CurriculumService(IGeneralRepository<Curriculum> CurriculumRepository) : ICurriculumService
    {
        private readonly IGeneralRepository<Curriculum> _curriculumRepository = CurriculumRepository;

        public async Task<IEnumerable<CurriculumResponse>> GetAllCurriculumsAsync(CancellationToken cancellationToken = default)
        {
            var Curriculums = await _curriculumRepository.GetAll()
                    .ProjectToType<CurriculumResponse>()
                    .ToListAsync(cancellationToken);

            return Curriculums;
        }

        public async Task<Result<CurriculumResponse>> GetCurriculumByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var Curriculum = await _curriculumRepository.Get(c => c.Id == id && c.IsActive)
                                .ProjectToType<CurriculumResponse>()
                                .SingleOrDefaultAsync(cancellationToken);
            if (Curriculum is null)
                return Result.Failure<CurriculumResponse>(CurriculumErrors.CurriculumNotFound);

            var response = Curriculum.Adapt<CurriculumResponse>();

            return Result.Success(response);
        }

        public async Task<Result<CurriculumResponseWithTopics>> GetCurriculumWithDetailsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var curriculum = await _curriculumRepository.Get(c => c.Id == id && c.IsActive)
                                .ProjectToType<CurriculumResponseWithTopics>()
                                .SingleOrDefaultAsync(cancellationToken);
            if (curriculum is null)
                return Result.Failure<CurriculumResponseWithTopics>(CurriculumErrors.CurriculumNotFound);

            return Result.Success(curriculum);
        }


        public async Task<CurriculumResponse> CreateCurriculumAsync(CurriculumRequest request, CancellationToken cancellationToken = default)
        {
            var curriculum = request.Adapt<Curriculum>();
            var addedCurriculum = await _curriculumRepository.AddAsync(curriculum);

            return addedCurriculum.Adapt<CurriculumResponse>();
        }

        public async Task<Result> UpdateCurriculumAsync(int id, CurriculumRequest request, CancellationToken cancellationToken = default)
        {
            var curriculum = await _curriculumRepository.Get(c => c.Id == id && c.IsActive)
                        .Include(c => c.Goals)
                        .AsTracking()
                        .SingleOrDefaultAsync(cancellationToken);
            if (curriculum is null)
                return Result.Failure(CurriculumErrors.CurriculumNotFound);

            curriculum.Title = request.Title;
            curriculum.Summary = request.Summary;

            var goalsToRemove = curriculum.Goals
                    .Where(dbGoal => !request.Goals.Contains(dbGoal.Description))
                    .ToList();

            foreach (var goal in goalsToRemove)
            {
                curriculum.Goals.Remove(goal);
            }

            var existingGoalDescriptions = curriculum.Goals.Select(g => g.Description).ToList();
            var goalsToAdd = request.Goals
                .Where(reqGoalDesc => !existingGoalDescriptions.Contains(reqGoalDesc))
                .ToList();

            foreach (var goalDescription in goalsToAdd)
            {
                curriculum.Goals.Add(new Goal { Description = goalDescription }); 
            }

            await _curriculumRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteCurriculumAsync(int id, CancellationToken cancellationToken = default)
        {
            var isDeleted = await _curriculumRepository.DeleteAsync(id);
            if (!isDeleted)
                return Result.Failure(CurriculumErrors.CurriculumNotFound);

            return Result.Success();
        }


    }
}
