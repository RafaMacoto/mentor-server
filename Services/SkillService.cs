using System;
using mentor.Data;
using mentor.DTOs.Skill;
using mentor.Models;
using mentor.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace mentor.Services
{
    public class SkillService : ISkillService
    {
        private readonly MentorDbContext _context;

        public SkillService(MentorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SkillDTO>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Skills
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
            .Take(pageSize)
                .Select(s => new SkillDTO(s.Id, s.Name, s.UserId))
            .ToListAsync();
        }

        public async Task<SkillDTO?> GetByIdAsync(int id)
        {
            return await _context.Skills
                .AsNoTracking()
            .Where(s => s.Id == id)
                .Select(s => new SkillDTO(s.Id, s.Name, s.UserId))
                .FirstOrDefaultAsync();
        }

        public async Task<SkillDTO> CreateAsync(SkillCreateDTO dto)
        {
            var skill = new Skill
            {
                Name = dto.Name,
                UserId = dto.UserId
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return new SkillDTO(skill.Id, skill.Name, skill.UserId);
        }

        public async Task<SkillDTO?> UpdateAsync(int id, SkillUpdateDTO dto)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return null;

            skill.Name = dto.Name;

            await _context.SaveChangesAsync();

            return new SkillDTO(skill.Id, skill.Name, skill.UserId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return false;

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
