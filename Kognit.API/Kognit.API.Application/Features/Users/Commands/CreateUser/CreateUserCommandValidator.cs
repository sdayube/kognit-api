using FluentValidation;
using Kognit.API.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public CreateUserCommandValidator(IUserRepositoryAsync positionRepository)
        {
            _userRepository = positionRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.")
                .MinimumLength(2).WithMessage("{PropertyName} must be at least 2 characters.");

            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .LessThanOrEqualTo(System.DateTime.Now.AddYears(-18)).WithMessage("{PropertyName} must have been at least 18 years ago.");

            RuleFor(p => p.CPF)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Length(11).WithMessage("{PropertyName} must have 11 characters.")
                .Matches(@"^\d+$").WithMessage("{PropertyName} must contain only numbers.")
                .MustAsync(IsUniqueCpf).WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsUniqueCpf(string positionNumber, CancellationToken cancellationToken)
        {
            return await _userRepository.IsUniqueCpfAsync(positionNumber);
        }
    }
}