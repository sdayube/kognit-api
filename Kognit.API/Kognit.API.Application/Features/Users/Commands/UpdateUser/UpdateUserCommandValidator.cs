using FluentValidation;
using Kognit.API.Application.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UpdateUserCommandValidator(IUserRepositoryAsync positionRepository)
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
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("{PropertyName} must have been at least 18 years ago.");

            RuleFor(p => p.CPF)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Length(11).WithMessage("{PropertyName} must have 11 characters.")
                .Matches(@"^\d+$").WithMessage("{PropertyName} must contain only numbers.")
                .MustAsync((updateUserCommand, cpf, _cancellationToken) => IsUniqueCpf(updateUserCommand.Id, cpf))
            .WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsUniqueCpf(Guid userId, string cpf)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.CPF == cpf)
            {
                return true;
            }

            return await _userRepository.IsUniqueCpfAsync(cpf);
        }
    }
}