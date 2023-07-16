using Kognit.API.Application.Features.Wallets.Commands.CreateWallet;
using Kognit.API.Application.Features.Wallets.Commands.DeleteWallet;
using Kognit.API.Application.Features.Wallets.Queries.GetWalletById;
using Kognit.API.Application.Features.Wallets.Queries.GetWallets;
using Kognit.API.WebApi.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kognit.API.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class WalletsController : BaseController
    {

        /// <summary>
        ///     Retorna uma lista de carteiras com os filtros e campos especificados.
        /// </summary>
        /// <param name="filter">Query params utilizados para busca e filtragem.</param>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetWalletsQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        ///     Retorna uma carteira pelo seu Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetWalletByIdQuery { Id = id }));
        }

        /// <summary>
        ///     Cria uma nova carteira.
        /// </summary>
        /// <param name="command">Comando com as informações necessárias para a criação da carteira.</param>
        /// <returns>Usuário criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateWalletCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        /// <summary>
        ///     Atualiza os dados uma carteira pelo seu Id.
        /// </summary>
        /// <param name="id">Id da carteira a ser atualizada.</param>
        /// <param name="request">Requisição com as informações necessárias para a atualização da carteira.</param>
        /// <returns>Carteira após a atualização.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateWalletRequest request)
        {
            var command = request.ToCommand(id);

            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        ///     Apaga uma carteira pelo seu Id.
        /// </summary>
        /// <returns>Resultado da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteWalletCommand { Id = id }));
        }
    }
}