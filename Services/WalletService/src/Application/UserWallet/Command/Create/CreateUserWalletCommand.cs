using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DatabaseContext;
using Common;
using MediatR;
using Domain.Entities;

namespace Application.UserWallet.Command
{
    public class CreateUserWalletCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
    }

    public class CreateUserWalletCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateUserWalletCommand, Result<string>>
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<Result<string>> Handle(CreateUserWalletCommand req, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(req.UserId)) return new Result<string> { Message = "Please provide a valid user id.", Status = false };

            var newWallet = new Wallet
            {
                UserId = req.UserId,
                Balance = default,
            };

            await _dbContext.Wallets.AddAsync(newWallet, cancellationToken);
            var result = await _dbContext.SaveChangesAsync();

            if(result > 0) return new Result<string> { Message = "Wallet created successfully.", Status = true };

            return new Result<string> { Message = "Unable to create new user wallet.", Status = false };
        }
    }
}