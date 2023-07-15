using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.CreatePosition
{
    public partial class InsertMockPositionCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedPositionCommandHandler : IRequestHandler<InsertMockPositionCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _positionRepository;

        public SeedPositionCommandHandler(IUserRepositoryAsync positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<Response<int>> Handle(InsertMockPositionCommand request, CancellationToken cancellationToken)
        {
            await _positionRepository.SeedDataAsync(request.RowCount);
            return new Response<int>(request.RowCount);
        }
    }
}