using FluentValidation;
using Kognit.API.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Wallets.Commands.UpdateWallet
{
    public class UpdateWalletCommandValidator : AbstractValidator<UpdateWalletCommand>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UpdateWalletCommandValidator(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;

            RuleFor(p => p.BankName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(UserExists).WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> UserExists(Guid userId, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(userId) != null;
        }
    }
}