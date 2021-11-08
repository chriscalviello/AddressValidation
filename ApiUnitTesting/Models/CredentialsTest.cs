using Api.Models.User;
using FluentValidation.TestHelper;
using Xunit;
namespace ApiUnitTesting.Models
{
    public class CredentialsTest
    {
        private readonly CredentialsValidator _validator = new CredentialsValidator();

        public CredentialsTest()
        {
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenNoUsername_ShouldHaveValidationError(string username)
        {
            var sut = new Credentials(username, "123456");
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Fact]
        public void GivenShortUsername_ShouldHaveValidationError()
        {
            var sut = new Credentials("Joe", "123456");
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenNoPassword_ShouldHaveValidationError(string password)
        {
            var sut = new Credentials("Username", password);
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void GivenShortPassword_ShouldHaveValidationError()
        {
            var sut = new Credentials("Username", "12345");
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
