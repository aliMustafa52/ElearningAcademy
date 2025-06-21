using ElearningAcademy.api.Contracts.Lessons;

namespace ElearningAcademy.api.Services.Lessons
{
    public class LessonService(IGeneralRepository<Lesson> lessonRepository,
            IGeneralRepository<Topic> topicRepository,
            IFileStorageService fileStorageService) : ILessonService
    {
        private readonly IGeneralRepository<Lesson> _lessonRepository = lessonRepository;
        private readonly IGeneralRepository<Topic> _topicRepository = topicRepository;
        private readonly IFileStorageService _fileStorageService = fileStorageService;

        public async Task<IEnumerable<LessonSummaryResponse>> GetLessonsForTopicAsync(int topicId, CancellationToken cancellationToken = default)
        {
            return await _lessonRepository
                .Get(l => l.TopicId == topicId && l.IsActive)
                .ProjectToType<LessonSummaryResponse>()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<LessonDetailsResponse>> GetLessonDetailsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var lessonDetails = await _lessonRepository
                .Get(l => l.Id == id && l.IsActive)
                .ProjectToType<LessonDetailsResponse>()
                .SingleOrDefaultAsync(cancellationToken);

            if (lessonDetails is null)
                return Result.Failure<LessonDetailsResponse>(LessonErrors.LessonNotFound);

            return Result.Success(lessonDetails);
        }

        public async Task<Result<LessonDetailsResponse>> CreateLessonAsync(int topicId, LessonRequest request, CancellationToken cancellationToken = default)
        {
            bool parentTopicExists = await _topicRepository.AnyAsync(t => t.Id == topicId && t.IsActive, cancellationToken);
            if (!parentTopicExists)
                return Result.Failure<LessonDetailsResponse>(LessonErrors.ParentTopicNotFound);

            var lesson = new Lesson
            {
                Title = request.Title,
                TopicId = topicId
            };

            if (request.LessonContents is not null)
            {
                int contentOrder = 1;
                foreach (var contentText in request.LessonContents)
                {
                    lesson.LessonContents.Add(new LessonContent { Text = contentText, Order = contentOrder++ });
                }
            }

            if (request.LessonImages is not null)
            {
                int imageOrder = 1;
                foreach (var imageFile in request.LessonImages)
                {
                    var imageUrl = await _fileStorageService.UploadImageAsync(imageFile, cancellationToken);
                    lesson.LessonImages.Add(new LessonImage { ImageUrl = imageUrl, Order = imageOrder++ });
                }
            }

            var addedLesson = await _lessonRepository.AddAsync(lesson);

            return Result.Success(addedLesson.Adapt<LessonDetailsResponse>());
        }

        public async Task<Result> UpdateLessonAsync(int id, LessonRequest request, CancellationToken cancellationToken = default)
        {
            var lesson = await _lessonRepository.Get(l => l.Id == id && l.IsActive)
                .Include(l => l.LessonContents)
                .Include(l => l.LessonImages)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (lesson is null)
                return Result.Failure(LessonErrors.LessonNotFound);

            // Update the title
            lesson.Title = request.Title;

            lesson.LessonContents.Clear();
            if (request.LessonContents is not null)
            {
                int contentOrder = 1;
                foreach (var contentText in request.LessonContents)
                {
                    lesson.LessonContents.Add(new LessonContent { Text = contentText, Order = contentOrder++ });
                }
            }


            foreach (var oldImage in lesson.LessonImages)
            {
                await _fileStorageService.DeleteFileAsync(oldImage.ImageUrl, cancellationToken);
            }
            lesson.LessonImages.Clear();

            if (request.LessonImages is not null)
            {
                int imageOrder = 1;
                foreach (var newImageFile in request.LessonImages)
                {
                    var newImageUrl = await _fileStorageService.UploadImageAsync(newImageFile, cancellationToken);
                    lesson.LessonImages.Add(new LessonImage { ImageUrl = newImageUrl, Order = imageOrder++ });
                }
            }

            await _lessonRepository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteLessonAsync(int id, CancellationToken cancellationToken = default)
        {
            var lesson = await _lessonRepository.Get(l => l.Id == id)
                                                .Include(l => l.LessonImages)
                                                .SingleOrDefaultAsync(cancellationToken);
            if (lesson is null)
                return Result.Failure(LessonErrors.LessonNotFound);

            foreach (var image in lesson.LessonImages)
                await _fileStorageService.DeleteFileAsync(image.ImageUrl, cancellationToken);

            var isDeleted = await _lessonRepository.DeleteAsync(id);
            if (!isDeleted)
                return Result.Failure(LessonErrors.LessonNotFound);

            return Result.Success();
        }
    }
}
