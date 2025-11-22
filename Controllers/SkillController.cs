using mentor.DTOs.Skill;
using mentor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using mentor.SwaggerExamples;
using mentor.DTOs.Commons;



namespace mentor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private const int MaxPageSize = 100;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as skills (paginado)",
            Description = "Retorna as skills cadastradas com paginação e links HATEOAS")]
        [SwaggerResponse(200, "Lista de skills retornada com sucesso", typeof(PagedResponse<SkillDTO>))]
        public async Task<ActionResult<PagedResponse<SkillDTO>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            pageSize = Math.Min(pageSize, MaxPageSize);

            var all = (await _skillService.GetAllAsync(pageNumber, pageSize)).ToList();
            var total = all.Count;

            var response = new PagedResponse<SkillDTO>(all, total, pageNumber, pageSize);

            response.Links.Add(new LinkDTO("self",
                Url.Action(nameof(GetAll), "Skill", new { pageNumber, pageSize }, Request.Scheme)!, "GET"));

            if (pageNumber > 1)
                response.Links.Add(new LinkDTO("prev",
                    Url.Action(nameof(GetAll), "Skill", new { pageNumber = pageNumber - 1, pageSize }, Request.Scheme)!, "GET"));

            if (pageNumber < response.TotalPages)
                response.Links.Add(new LinkDTO("next",
                    Url.Action(nameof(GetAll), "Skill", new { pageNumber = pageNumber + 1, pageSize }, Request.Scheme)!, "GET"));

            return Ok(response);
        }

       
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Busca uma skill por ID",
            Description = "Retorna uma skill específica pelo seu ID")]
        [SwaggerResponse(200, "Skill encontrada", typeof(SkillDTO))]
        [SwaggerResponse(404, "Skill não encontrada")]
        public async Task<ActionResult<SkillDTO>> GetById(int id)
        {
            var skill = await _skillService.GetByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

  
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria uma nova skill",
            Description = "Cria uma skill associada a um usuário")]
        [SwaggerResponse(201, "Skill criada com sucesso", typeof(SkillDTO))]
        [SwaggerRequestExample(typeof(SkillCreateDTO), typeof(SkillCreateExample))]
        public async Task<ActionResult<SkillDTO>> Create([FromBody] SkillCreateDTO dto)
        {
            var skill = await _skillService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
        }

      
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza uma skill",
            Description = "Atualiza o nome de uma skill existente")]
        [SwaggerResponse(200, "Skill atualizada com sucesso", typeof(SkillDTO))]
        [SwaggerResponse(404, "Skill não encontrada")]
        [SwaggerRequestExample(typeof(SkillUpdateDTO), typeof(SkillUpdateExample))]
        public async Task<ActionResult<SkillDTO>> Update(int id, [FromBody] SkillUpdateDTO dto)
        {
            var skill = await _skillService.UpdateAsync(id, dto);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

       
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleta uma skill",
            Description = "Remove uma skill existente pelo ID")]
        [SwaggerResponse(204, "Skill deletada com sucesso")]
        [SwaggerResponse(404, "Skill não encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _skillService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
