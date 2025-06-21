using ElearningAcademy.api.Contracts.Topics;

namespace ElearningAcademy.api.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Curriculum, CurriculumResponse>()
                .Map(dest => dest.Goals, src => src.Goals.Select(x => x.Description));

            config.NewConfig<CurriculumRequest, Curriculum>()
                .Map(dest => dest.Goals, src => src.Goals.Select(x => new Goal { Description = x}));

            config.NewConfig<Curriculum, CurriculumResponseWithTopics>()
                .Map(dest => dest.Goals, src => src.Goals.Select(x => x.Description))
                .Map(dest => dest.Topics, src => src.Topics
                        .Select(x => new TopicResponse(x.Id, x.Title))
                );
        }
    }
}
