namespace ElearningAcademy.api.Entities
{
    public class Topic : BaseModel
    {
        public string Title { get; set; } = string.Empty;


        public int CurriculumId { get; set; }
        public virtual Curriculum Curriculum { get; set; } = default!;

        public ICollection<Lesson> Lessons { get; set; } = [];
    }
}
