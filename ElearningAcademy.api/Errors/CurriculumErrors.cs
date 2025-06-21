namespace ElearningAcademy.api.Errors
{
    public static class CurriculumErrors
    {
        public static readonly Error CurriculumNotFound =
            new("Curriculum.NotFound", "No Curriculum was found with the given ID", StatusCodes.Status404NotFound);
    }
}
