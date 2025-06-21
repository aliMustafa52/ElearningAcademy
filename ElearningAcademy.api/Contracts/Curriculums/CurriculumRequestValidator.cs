namespace ElearningAcademy.api.Contracts.Curriculums
{
    public class CurriculumRequestValidator : AbstractValidator<CurriculumRequest>
    {
        public CurriculumRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(3,200);

            RuleFor(x => x.Summary)
                .NotEmpty()
                .Length(3, 2000);
        }
    }
}
