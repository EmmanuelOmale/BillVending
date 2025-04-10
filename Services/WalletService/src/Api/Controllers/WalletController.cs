using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.UserWallet.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application.UserWallet.Command.Update;
using Application.UserWallet.Queries;


namespace Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly ILogger<WalletController> _logger;
        private readonly IMediator _mediator;

        public WalletController(ILogger<WalletController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUserWallet([FromBody] CreateUserWalletCommand command)
        {
            if (command == null || string.IsNullOrWhiteSpace(command.UserId)) return BadRequest(new { Message = "User id required." });

            var result = await _mediator.Send(command);

            return result.Status ? Ok(new { Message = result.Message }) : BadRequest(new { Message = result.Message });
        }

        [HttpPost("fund")]
        public async Task<IActionResult> FundWallet(FundWalletCommand command)
        {
            await _mediator.Send(command);
            return Ok("Wallet funded");
        }

        [HttpGet("balance/{userId}")]
        public async Task<IActionResult> GetBalance(Guid userId)
        {
            var balance = await _mediator.Send(new GetBalanceQuery {UserId = userId });
            return Ok(new { userId, balance });
        }

    }
}