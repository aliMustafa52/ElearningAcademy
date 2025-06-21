namespace ElearningAcademy.api.Entities
{
    public class Curriculum : BaseModel
    {
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;
        public ICollection<Goal> Goals { get; set; } = [];

        public ICollection<Topic> Topics { get; set; } = [];
    }
}
