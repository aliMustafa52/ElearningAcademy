namespace ElearningAcademy.api.Errors
{
    public static class LessonErrors
    {
        public static readonly Error LessonNotFound = new("Lesson.NotFound", "The lesson with the specified ID was not found.", StatusCodes.Status404NotFound);
        public static readonly Error ParentTopicNotFound = new("Lesson.ParentNotFound", "The parent topic for this lesson does not exist or is inactive.", StatusCodes.Status404NotFound);
    }
}
