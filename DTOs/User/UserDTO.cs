using mentor.DTOs.Skill;

namespace mentor.DTOs.User


{
    public record UserDTO(
     int Id,
     string FullName,
     string Email,
     IEnumerable<SkillDTO> Skills
 );
}
