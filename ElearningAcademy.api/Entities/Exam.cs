namespace ElearningAcademy.api.Entities
{
    public class Exam : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = default!;
    }
}
