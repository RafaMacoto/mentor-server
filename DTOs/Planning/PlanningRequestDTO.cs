namespace mentor.DTOs.Planning
{
    public record PlanningRequestDTO(
        string Goal,
        List<string> Skills
    );
}
