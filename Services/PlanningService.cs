using System.Net.Http.Json;
using mentor.DTOs.Planning;
using mentor.Services.Interfaces;

namespace mentor.Services
{
    public class PlanningService : IPlanningService
    {
        private readonly HttpClient _http;

        public PlanningService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<PlanningResponse?> GeneratePlanningAsync(PlanningRequestDTO request)
        {
            var url = "https://appmentoria.azurewebsites.net/planning";

            var response = await _http.PostAsJsonAsync(url, request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<PlanningResponse>();
        }
    }
}

