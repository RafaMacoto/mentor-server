using mentor.DTOs.Skill;

namespace mentor.Services.Interfaces
{
    public interface ISkillService
    {

        Task<IEnumerable<SkillDTO>> GetAllAsync(int pageNumber, int pageSize);
        Task<SkillDTO?> GetByIdAsync(int id);
        Task<SkillDTO> CreateAsync(SkillCreateDTO dto);
        Task<SkillDTO> UpdateAsync(int id, SkillUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
