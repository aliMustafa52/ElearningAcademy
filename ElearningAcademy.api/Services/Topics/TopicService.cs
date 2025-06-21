using ElearningAcademy.api.Contracts.Topics;

namespace ElearningAcademy.api.Services.Topics
{
    public class TopicService(IGeneralRepository<Topic> topicRepository, IGeneralRepository<Curriculum> curriculumRepository) : ITopicService
    {
        private readonly IGeneralRepository<Topic> _topicRepository = topicRepository;
        private readonly IGeneralRepository<Curriculum> _curriculumRepository = curriculumRepository;

        public async Task<IEnumerable<TopicResponse>> GetTopicsForCurriculumAsync(int curriculumId, CancellationToken cancellationToken = default)
        {
            var topics = await _topicRepository
                .Get(t => t.CurriculumId == curriculumId && t.IsActive)
                .ProjectToType<TopicResponse>()
                .ToListAsync(cancellationToken);

            return topics;
        }

        public async Task<Result<TopicResponse>> GetTopicByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var topicResponse = await _topicRepository
                .Get(t => t.Id == id && t.IsActive)
                .ProjectToType<TopicResponse>()
                .SingleOrDefaultAsync(cancellationToken);

            if (topicResponse is null)
                return Result.Failure<TopicResponse>(TopicErrors.TopicNotFound);

            return Result.Success(topicResponse);
        }

        public async Task<Result<TopicResponse>> CreateTopicAsync(int curriculumId, TopicRequest request, CancellationToken cancellationToken = default)
        {
            bool parentExists = await _curriculumRepository.AnyAsync(c => c.Id == curriculumId && c.IsActive, cancellationToken);
            if (!parentExists)
            {
                return Result.Failure<TopicResponse>(TopicErrors.ParentCurriculumNotFound);
            }

            var topic = request.Adapt<Topic>();
            topic.CurriculumId = curriculumId;

            var addedTopic = await _topicRepository.AddAsync(topic);

            return Result.Success(addedTopic.Adapt<TopicResponse>());
        }

        public async Task<Result> UpdateTopicAsync(int id, TopicRequest request, CancellationToken cancellationToken = default)
        {
            var topic = await _topicRepository.GetByIdWithTrackingAsync(id);

            if (topic is null || !topic.IsActive)
            {
                return Result.Failure(TopicErrors.TopicNotFound);
            }

            topic.Title = request.Title;

            await _topicRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteTopicAsync(int id, CancellationToken cancellationToken = default)
        {
            var isDeleted = await _topicRepository.DeleteAsync(id);

            if (!isDeleted)
                return Result.Failure(TopicErrors.TopicNotFound);

            return Result.Success();
        }
    }
}
