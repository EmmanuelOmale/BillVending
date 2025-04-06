using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.UserWallet.Command;
using MediatR;

namespace Api
{
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
        [Route("wallet/create")]
        public async Task<IActionResult> CreateUserWallet([FromBody] CreateUserWalletCommand command)
        {
            if (command == null || string.IsNullOrWhiteSpace(command.UserId)) return BadRequest(new { Message = "User id required."});

            var result = await _mediator.Send(command);

            return result.Status ? Ok(new { Message = result.Message}) : BadRequest(new { Message = result.Message});
        }

    }
}