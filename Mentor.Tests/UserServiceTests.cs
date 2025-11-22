using mentor.Data;
using mentor.DTOs;
using mentor.Models;
using mentor.Services;
using Microsoft.EntityFrameworkCore;          

using FluentAssertions;
using mentor.DTOs.User;

namespace Mentor.Tests
{
    public class UserServiceTests
    {
        private MentorDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MentorDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new MentorDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUser()
        {
            var context = GetDbContext();
            var service = new UserService(context);

            var dto = new UserCreateDTO(
                "Rafa Macoto",
                "rafa@email",
                "123456"
            );

            var result = await service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.FullName.Should().Be("Rafa Macoto");

            context.Users.Count().Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser()
        {
            var context = GetDbContext();
            var service = new UserService(context);

            var user = new User { FullName = "Fulano", Email = "a@a", Password = "123" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var found = await service.GetByIdAsync(user.Id);

            found.Should().NotBeNull();
            found!.Id.Should().Be(user.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            var context = GetDbContext();
            var service = new UserService(context);

            context.Users.Add(new User { FullName = "A", Email = "a", Password = "123" });
            context.Users.Add(new User { FullName = "B", Email = "b", Password = "123" });

            await context.SaveChangesAsync();

            var list = await service.GetAllAsync();

            list.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            var context = GetDbContext();
            var service = new UserService(context);

            var user = new User { FullName = "Old", Email = "old", Password = "123" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var updated = await service.UpdateAsync(
                user.Id,
                new UserUpdateDto("New Name", "new@mail", "123", "Goal")
            );

            updated.Should().NotBeNull();
            updated!.FullName.Should().Be("New Name");
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUser()
        {
            var context = GetDbContext();
            var service = new UserService(context);

            var user = new User { FullName = "To Delete", Email = "d", Password = "123" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var result = await service.DeleteAsync(user.Id);

            result.Should().BeTrue();
            context.Users.Count().Should().Be(0);
        }
    }
}
