namespace ElearningAcademy.api.Errors
{
    public static class TopicErrors
    {
        public static readonly Error TopicNotFound = new("Topic.NotFound", "The topic with the specified ID was not found.", StatusCodes.Status404NotFound);
        public static readonly Error ParentCurriculumNotFound = new("Topic.ParentNotFound", "The parent curriculum for this topic does not exist or is inactive.", StatusCodes.Status404NotFound);
    }
}
