using Johansfunction.Data;
using Johansfunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Johansfunction.Functions
{
    public class UpdatePlayer
    {
        private readonly ILogger<UpdatePlayer> _logger;
        private readonly AppDbContext _dbContext;
        public UpdatePlayer(AppDbContext context, ILogger<UpdatePlayer> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [Function("UpdatePlayer")]
        public async Task<IActionResult> UpdateOnePlayer([HttpTrigger(AuthorizationLevel.Function, "put", Route = "player/{id}")] HttpRequest req, int id)
        {
            var selectedPlayer = await _dbContext.AthleticPlayers.FirstOrDefaultAsync(p => p.Id == id);

            if (selectedPlayer == null) return new NotFoundResult();
            else
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var updatedPlayer = JsonConvert.DeserializeObject<AthleticPlayer>(requestBody);
                selectedPlayer.Name = updatedPlayer.Name;
                selectedPlayer.Salary = updatedPlayer.Salary;
                
                await _dbContext.SaveChangesAsync();
                return new OkObjectResult(selectedPlayer);
            }
        }
    }
}
