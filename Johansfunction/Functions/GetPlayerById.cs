using Johansfunction.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Johansfunction.Functions
{
    public class GetPlayerById
    {
        private readonly ILogger<GetPlayerById> _logger;
        private readonly AppDbContext _dbContext;
        public GetPlayerById(AppDbContext context, ILogger<GetPlayerById> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [Function("GetPlayer")]
        public async Task<IActionResult> GetPlayer([HttpTrigger(AuthorizationLevel.Function, "get", Route = "player/{id}")] HttpRequest req, int id)
        {
            var player = await _dbContext.AthleticPlayers.FirstOrDefaultAsync(p => p.Id == id);
            if (player == null) return new NotFoundResult();

            return new OkObjectResult(player);
        }
    }
}
