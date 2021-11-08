using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Api.Models.User
{
    public class Credentials
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class CredentialsValidator : AbstractValidator<Credentials>
    {
        public CredentialsValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username is required")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
