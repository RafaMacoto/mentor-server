using mentor.DTOs.Planning;
using mentor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace mentor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanningController : ControllerBase
    {
        private readonly IPlanningService _planningService;

        public PlanningController(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        [HttpPost("generate")]
        [SwaggerOperation(
            Summary = "Gera um planejamento de carreira",
            Description = "Recebe goal + lista de skills e retorna o planejamento gerado pela API externa")]
        [SwaggerResponse(200, "Planejamento gerado com sucesso", typeof(PlanningResponse))]
        [SwaggerResponse(400, "Request inválida")]
        [SwaggerResponse(500, "Erro interno ao gerar o planejamento")]
        public async Task<ActionResult<PlanningResponse>> Generate([FromBody] PlanningRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Goal))
                return BadRequest("Goal é obrigatório.");

            var result = await _planningService.GeneratePlanningAsync(request);

            if (result == null)
                return StatusCode(500, "Erro ao gerar planejamento na API externa.");

            return Ok(result);
        }
    }
}
