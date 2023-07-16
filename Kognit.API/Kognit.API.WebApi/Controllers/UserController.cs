using AutoMapper;
using Kognit.API.Application.Features.Users.Commands.CreateUser;
using Kognit.API.Application.Features.Users.Commands.DeletePositionById;
using Kognit.API.Application.Features.Users.Queries.GetUserById;
using Kognit.API.Application.Features.Users.Queries.GetUsers;
using Kognit.API.WebApi.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kognit.API.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///     Retorna uma lista de usuários com os filtros e campos especificados.
        /// </summary>
        /// <param name="filter">Query params utilizados para busca e filtragem.</param>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        ///     Retorna um usuário pelo seu Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
        }

        /// <summary>
        ///     Cria um novo usuário.
        /// </summary>
        /// <param name="command">Comando com as informações necessárias para a criação do usuário.</param>
        /// <returns>Usuário criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        /// <summary>
        ///     Atualiza os dados um usuário pelo seu Id.
        /// </summary>
        /// <param name="id">Id do usuário a ser atualizado.</param>
        /// <param name="request">Requisição com as informações necessárias para a atualização do usuário.</param>
        /// <returns>Usuário após a atualização.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateUserRequest request)
        {
            var command = request.ToCommand(id);

            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        ///     Apaga um usuário pelo seu Id.
        /// </summary>
        /// <returns>Resultado da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeletePositionByIdCommand { Id = id }));
        }
    }
}