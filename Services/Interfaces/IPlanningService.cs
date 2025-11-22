using mentor.DTOs.Planning;

namespace mentor.Services.Interfaces
{
    public interface IPlanningService
    {
        Task<PlanningResponse?> GeneratePlanningAsync(PlanningRequestDTO request);
    }
}
