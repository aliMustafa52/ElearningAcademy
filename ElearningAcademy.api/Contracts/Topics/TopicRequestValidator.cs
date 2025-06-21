namespace ElearningAcademy.api.Contracts.Topics
{
    public class TopicRequestValidator : AbstractValidator<TopicRequest>
    {
        public TopicRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(3, 200);
        }
    }
}
