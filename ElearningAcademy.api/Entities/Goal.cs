namespace ElearningAcademy.api.Entities
{
    public class Goal : BaseModel
    {
        public string Description { get; set; } = string.Empty;

        public int CurriculumId { get; set; }
        public virtual Curriculum Curriculum { get; set; } = default!;
    }
}
