using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UserDirectory.Controllers;
using UserDirectory.Models;
using Xunit;

namespace UserDirectory.Tests
{
    public class UsersControllerTests : TestBase
    {
        // ✅ Ensures GetUsers returns all users present in the database
        [Fact]
        public async Task GetUsers_ReturnsAllUsers()
        {
            Context.Users.AddRange(
                new User { Name = "Alice", Age = 30, City = "Chennai", State = "TN", Pincode = "600001" },
                new User { Name = "Bob", Age = 25, City = "Delhi", State = "DL", Pincode = "110001" }
            );
            await Context.SaveChangesAsync();

            var controller = new UsersController(Context);
            var result = await controller.GetUsers();

            Assert.Equal(4, result?.Value?.Count());
        }

        // ✅ Ensures GetUser returns the correct user when the ID exists
        [Fact]
        public async Task GetUser_ReturnsUser_WhenExists()
        {
            var user = new User { Name = "Charlie", Age = 40, City = "Mumbai", State = "MH", Pincode = "400001" };
            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            var controller = new UsersController(Context);
            var result = await controller.GetUser(user.Id);

            Assert.Equal("Charlie", result?.Value?.Name);
        }

        // ✅ Ensures GetUser returns NotFound when the ID does not exist
        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenNotExists()
        {
            var controller = new UsersController(Context);
            var result = await controller.GetUser(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        // ✅ Ensures CreateUser returns CreatedAtAction with the new user when valid
        [Fact]
        public async Task CreateUser_ReturnsCreatedUser_WhenValid()
        {
            var controller = new UsersController(Context);
            var user = new User { Name = "David", Age = 28, City = "Pune", State = "MH", Pincode = "411001" };

            var result = await controller.CreateUser(user);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdUser = Assert.IsType<User>(createdResult.Value);
            Assert.Equal("David", createdUser.Name);
        }

        // ✅ Ensures UpdateUser returns NoContent when the update is valid
        [Fact]
        public async Task UpdateUser_ReturnsNoContent_WhenValid()
        {
            var user = new User { Name = "Eve", Age = 35, City = "Hyderabad", State = "TS", Pincode = "500001" };
            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            var controller = new UsersController(Context);
            user.City = "Secunderabad";

            var result = await controller.UpdateUser(user.Id, user);

            Assert.IsType<NoContentResult>(result);
        }

        // ✅ Ensures UpdateUser returns BadRequest when the ID does not match
        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenIdMismatch()
        {
            var user = new User { Id = 1, Name = "Frank", Age = 45, City = "Kolkata", State = "WB", Pincode = "700001" };
            var controller = new UsersController(Context);

            var result = await controller.UpdateUser(999, user);

            Assert.IsType<BadRequestResult>(result);
        }

        // ✅ Ensures DeleteUser removes the user and returns NoContent when the ID exists
        [Fact]
        public async Task DeleteUser_RemovesUser_WhenExists()
        {
            var user = new User { Name = "Grace", Age = 50, City = "Jaipur", State = "RJ", Pincode = "302001" };
            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            var controller = new UsersController(Context);
            var result = await controller.DeleteUser(user.Id);

            Assert.DoesNotContain(Context.Users, u => u.Id == user.Id);
        }

        // ✅ Ensures DeleteUser returns NotFound when the ID does not exist
        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenNotExists()
        {
            var controller = new UsersController(Context);
            var result = await controller.DeleteUser(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}