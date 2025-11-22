using mentor.DTOs;
using mentor.DTOs.Commons;
using mentor.DTOs.User;
using mentor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace mentor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private const int MaxPageSize = 100;

        public UserController(IUserService service)
        {
            _service = service;
        }

        
        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os usuários", Description = "Retorna usuários com paginação e HATEOAS")]
        [SwaggerResponse(200, "Lista retornada", typeof(PagedResponse<UserDTO>))]
        public async Task<ActionResult<PagedResponse<UserDTO>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            pageSize = Math.Min(pageSize, MaxPageSize);

            var all = (await _service.GetAllAsync()).ToList();
            var total = all.Count;

            var paged = all.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var response = new PagedResponse<UserDTO>(paged, total, pageNumber, pageSize);

            response.Links.Add(new LinkDTO("self", Url.Action(nameof(GetAll), new { pageNumber, pageSize })!, "GET"));

            if (pageNumber > 1)
                response.Links.Add(new LinkDTO("prev", Url.Action(nameof(GetAll), new { pageNumber = pageNumber - 1, pageSize })!, "GET"));

            if (pageNumber < response.TotalPages)
                response.Links.Add(new LinkDTO("next", Url.Action(nameof(GetAll), new { pageNumber = pageNumber + 1, pageSize })!, "GET"));

            return Ok(response);
        }

      
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca usuário por ID")]
        [SwaggerResponse(200, "Usuário encontrado", typeof(UserDTO))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // CREATE
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um usuário")]
        [SwaggerResponse(201, "Usuário criado", typeof(UserDTO))]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserCreateDTO dto)
        {
            var user = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um usuário")]
        [SwaggerResponse(200, "Usuário atualizado", typeof(UserDTO))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<ActionResult<UserDTO>> Update(int id, [FromBody] UserUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário")]
        [SwaggerResponse(204, "Usuário deletado")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
