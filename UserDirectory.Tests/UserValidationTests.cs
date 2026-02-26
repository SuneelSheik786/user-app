using System.ComponentModel.DataAnnotations;
using UserDirectory.Models;
using Xunit;

namespace UserDirectory.Tests
{
    public class UserValidationTests : TestBase
    {
        private bool ValidateModel(User user, out ValidationContext context, out List<ValidationResult> results)
        {
            // Create a validation context for the given object (User)
            context = new ValidationContext(user, null, null);

            // Prepare a list to hold any validation errors
            results = new List<ValidationResult>();

            // Run validation against all DataAnnotation attributes on the User model
            // Returns true if valid, false if any rules fail
            return Validator.TryValidateObject(user, context, results, true);
        }


        // ✅ Ensures Name validation fails when too short (<2 chars)
        [Fact]
        public void Name_Should_Be_Invalid_WhenTooShort()
        {
            var user = new User { Name = "A", Age = 30, City = "Chennai", State = "TN", Pincode = "600001" };
            var isValid = ValidateModel(user, out _, out var results);
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("minimum length"));
        }

        // ✅ Ensures Age validation fails when outside allowed range (0–120)
        [Fact]
        public void Age_Should_Be_Invalid_WhenOutOfRange()
        {
            var user = new User { Name = "Alice", Age = 200, City = "Chennai", State = "TN", Pincode = "600001" };
            var isValid = ValidateModel(user, out _, out var results);
            Assert.False(isValid);
        }

        // ✅ Ensures Pincode validation fails when too short (<4 chars)
        [Fact]
        public void Pincode_Should_Be_Invalid_WhenTooShort()
        {
            var user = new User { Name = "Bob", Age = 25, City = "Delhi", State = "DL", Pincode = "12" };
            var isValid = ValidateModel(user, out _, out var results);
            Assert.False(isValid);
        }

        // ✅ Ensures a valid User passes all validation rules
        [Fact]
        public void Valid_User_Should_Pass_Validation()
        {
            var user = new User { Name = "Charlie", Age = 40, City = "Mumbai", State = "MH", Pincode = "400001" };
            var isValid = ValidateModel(user, out _, out var results);
            Assert.True(isValid);
            Assert.Empty(results);
        }
    }
}