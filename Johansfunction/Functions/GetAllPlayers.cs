using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Johansfunction.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Johansfunction.Functions
{
    public class GetAllPlayers
    {
        private readonly ILogger<GetAllPlayers> _logger;
        private readonly AppDbContext _dbContext;
        public GetAllPlayers(AppDbContext context, ILogger<GetAllPlayers> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [Function("GetPlayers")]
        public async Task<IActionResult> GetPlayers([HttpTrigger(AuthorizationLevel.Function, "get", Route = "player")] HttpRequest req)
        {
            var players = await _dbContext.AthleticPlayers.ToListAsync();
            return new OkObjectResult(players);
        }

        
    }
}
