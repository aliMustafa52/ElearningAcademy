namespace ElearningAcademy.api.Contracts.Lessons
{
    public class LessonRequestValidator : AbstractValidator<LessonRequest>
    {
        public LessonRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(3, 200);
        }
    }
}
