namespace ElearningAcademy.api.Entities
{
    public class Lesson : BaseModel
    {
        public string Title { get; set; } = string.Empty;

        public ICollection<LessonContent> LessonContents { get; set; } = [];
        public ICollection<LessonImage> LessonImages { get; set; } = [];

        public int TopicId { get; set; }
        public Topic Topic { get; set; } = default!;

        public ICollection<Exam> Exams { get; set; } = [];
    }
}
