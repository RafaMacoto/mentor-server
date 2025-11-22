using mentor.DTOs.User;
using mentor.DTOs;
using mentor.Models;

namespace mentor.Services.Interfaces
{
    public interface IUserService
    {

        Task<UserDTO> CreateAsync(UserCreateDTO dto);
        Task<UserDTO?> GetByIdAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> UpdateAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
