using Swashbuckle.AspNetCore.Filters;
using mentor.DTOs.Skill;

namespace mentor.SwaggerExamples
{
    public class SkillCreateExample : IExamplesProvider<SkillCreateDTO>
    {
        public SkillCreateDTO GetExamples()
        {
            return new SkillCreateDTO(
                Name: "C#",
                UserId: 1
            );
        }
    }

    public class SkillUpdateExample : IExamplesProvider<SkillUpdateDTO>
    {
        public SkillUpdateDTO GetExamples()
        {
            return new SkillUpdateDTO(
                Name: "C# Avançado"
            );
        }
    }
}
