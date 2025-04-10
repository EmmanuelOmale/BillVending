using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.UserWallet.Command.Create;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillPaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BillPaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> PayBill(PayBillCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? Ok(result)
                : StatusCode(502, result);
        }
    }
}