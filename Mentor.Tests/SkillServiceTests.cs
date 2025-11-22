using mentor.Data;
using mentor.DTOs.Skill;
using mentor.Models;
using mentor.Services;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace Mentor.Tests
{
    public class SkillServiceTests
    {
        private MentorDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MentorDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new MentorDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateSkill()
        {
            var context = GetDbContext();
            var service = new SkillService(context);

            var dto = new SkillCreateDTO("C#", 1);

            var result = await service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.Name.Should().Be("C#");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSkill()
        {
            var context = GetDbContext();
            var service = new SkillService(context);

            var skill = new Skill { Name = "Java", UserId = 1 };
            context.Skills.Add(skill);
            await context.SaveChangesAsync();

            var found = await service.GetByIdAsync(skill.Id);

            found.Should().NotBeNull();
            found!.Id.Should().Be(skill.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnPagedSkills()
        {
            var context = GetDbContext();
            var service = new SkillService(context);

            for (int i = 1; i <= 10; i++)
                context.Skills.Add(new Skill { Name = "Skill " + i, UserId = 1 });

            await context.SaveChangesAsync();

            var list = await service.GetAllAsync(1, 5);

            list.Should().HaveCount(5);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateSkill()
        {
            var context = GetDbContext();
            var service = new SkillService(context);

            var skill = new Skill { Name = "OldName", UserId = 1 };
            context.Skills.Add(skill);
            await context.SaveChangesAsync();

            var updated = await service.UpdateAsync(skill.Id, new SkillUpdateDTO("NewName"));

            updated.Should().NotBeNull();
            updated!.Name.Should().Be("NewName");
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteSkill()
        {
            var context = GetDbContext();
            var service = new SkillService(context);

            var skill = new Skill { Name = "Delete", UserId = 1 };
            context.Skills.Add(skill);
            await context.SaveChangesAsync();

            var result = await service.DeleteAsync(skill.Id);

            result.Should().BeTrue();
            context.Skills.Count().Should().Be(0);
        }
    }
}
