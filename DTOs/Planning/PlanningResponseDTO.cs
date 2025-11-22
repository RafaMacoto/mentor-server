namespace mentor.DTOs.Planning
{
    public record PlanningItemResponse(
       long Id,
       string Description,
       bool Completed
   );

    public record PlanningResponse(
        long Id,
        string Goal,
        string Recommendation,
        DateTime CreatedAt,
        long UserId,
        List<PlanningItemResponse> Items
    );
}
