namespace ElearningAcademy.api.Entities
{
    public class LessonContent : BaseModel
    {
        public string Text { get; set; } = string.Empty;

        public int Order { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = default!;
    }
}
