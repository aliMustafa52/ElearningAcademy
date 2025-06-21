namespace ElearningAcademy.api.Entities
{
    public class LessonImage : BaseModel
    {
        public string ImageUrl { get; set; } = string.Empty;

        public int Order { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = default!;
    }
}
