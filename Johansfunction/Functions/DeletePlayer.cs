using Johansfunction.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Johansfunction.Functions
{
    public class DeletePlayer
    {
        private readonly ILogger<DeletePlayer> _logger;
        private readonly AppDbContext _dbContext;
        public DeletePlayer(AppDbContext context, ILogger<DeletePlayer> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [Function("RemovePlayer")]
        public async Task<IActionResult> RemovePlayer([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "player/{id}")] HttpRequest req, int id)
        {
            var player = await _dbContext.AthleticPlayers.FirstOrDefaultAsync(p => p.Id == id);
            if (player == null)
            {
                _logger.LogInformation("The player could not be found");
                return new NotFoundResult();
            } 

            _dbContext.AthleticPlayers.Remove(player);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("The player you selected was removed");
            return new NoContentResult();
        }
    }
}
