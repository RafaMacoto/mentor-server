using mentor.Data;
using mentor.Models;
using mentor.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using mentor.DTOs.User;
using mentor.DTOs.Skill;
using mentor.DTOs;

namespace mentor.Services;

public class UserService : IUserService
{
    private readonly MentorDbContext _context;

    public UserService(MentorDbContext context)
    {
        _context = context;
    }

   
    public async Task<UserDTO> CreateAsync(UserCreateDTO dto)
    {
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Password = dto.Password,
            CareerGoal = "" 
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDTO(
            user.Id,
            user.FullName,
            user.Email,
            Enumerable.Empty<SkillDTO>()
        );
    }


    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var user = await _context.Users
            .Include(u => u.Skills)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return null;

        return new UserDTO(
            user.Id,
            user.FullName,
            user.Email,
            user.Skills.Select(s => new SkillDTO(s.Id, s.Name, s.UserId))
        );
    }

   
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Skills)
            .AsNoTracking()
            .Select(u => new UserDTO(
                u.Id,
                u.FullName,
                u.Email,
                u.Skills.Select(s => new SkillDTO(s.Id, s.Name, s.UserId))
            ))
            .ToListAsync();
    }

  
    public async Task<UserDTO?> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.FullName = dto.FullName;
        user.Email = dto.Email;
        user.Password = dto.Password;
        user.CareerGoal = dto.GoalCarreer;

        await _context.SaveChangesAsync();

        var skills = await _context.Skills
            .Where(s => s.UserId == user.Id)
            .Select(s => new SkillDTO(s.Id, s.Name, s.UserId))
            .ToListAsync();

        return new UserDTO(
            user.Id,
            user.FullName,
            user.Email,
            skills
        );
    }

 
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
