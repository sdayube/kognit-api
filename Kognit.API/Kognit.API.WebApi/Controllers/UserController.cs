using Kognit.API.Application.Features.Users.Commands.CreatePosition;
using Kognit.API.Application.Features.Users.Commands.DeletePositionById;
using Kognit.API.Application.Features.Users.Commands.UpdateUser;
using Kognit.API.Application.Features.Users.Queries.GetUserById;
using Kognit.API.Application.Features.Users.Queries.GetUsers;
using Kognit.API.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kognit.API.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {

        /// <summary>
        /// Gets a list of positions based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter used to query the positions.</param>
        /// <returns>A list of positions.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// Gets a position by its Id.
        /// </summary>
        /// <param name="id">The Id of the position.</param>
        /// <returns>The position with the specified Id.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
        }

        /// <summary>
        /// Creates a new position.
        /// </summary>
        /// <param name="command">The command containing the data for the new position.</param>
        /// <returns>A 201 Created response containing the newly created position.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreatePositionCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        /// <summary>
        /// Updates a position with the given id using the provided command.
        /// </summary>
        /// <param name="id">The id of the position to update.</param>
        /// <param name="command">The command containing the updated information.</param>
        /// <returns>The updated position.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationConsts.AdminPolicy)]
        public async Task<IActionResult> Put(Guid id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a position by its Id.
        /// </summary>
        /// <param name="id">The Id of the position to delete.</param>
        /// <returns>The result of the deletion.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationConsts.AdminPolicy)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeletePositionByIdCommand { Id = id }));
        }
    }
}