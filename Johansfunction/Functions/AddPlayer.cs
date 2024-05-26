using Johansfunction.Data;
using Johansfunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Johansfunction.Functions
{
    public class AddPlayer
    {
        private readonly ILogger<AddPlayer> _logger;
        private readonly AppDbContext _dbContext;

        public AddPlayer(AppDbContext context, ILogger<AddPlayer> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        //Create player
        [Function("CreatePlayer")]
        public async Task<IActionResult> CreatePlayer([HttpTrigger(AuthorizationLevel.Function, "post", Route = "player")] HttpRequest req, ILogger logger)
        {
            var request = await new StreamReader(req.Body).ReadToEndAsync();
            var player = JsonConvert.DeserializeObject<AthleticPlayer>(request);
            _dbContext.AthleticPlayers.Add(player);
            await _dbContext.SaveChangesAsync(); 
            return new CreatedResult("/players", player);
        }
    }
}
