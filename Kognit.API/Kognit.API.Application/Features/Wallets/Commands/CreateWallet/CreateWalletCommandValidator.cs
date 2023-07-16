using FluentValidation;
using Kognit.API.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Wallets.Commands.CreateWallet
{
    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public CreateWalletCommandValidator(IWalletRepositoryAsync walletRepository, IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;

            RuleFor(p => p.BankName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Value)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

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
