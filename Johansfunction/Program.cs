using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Johansfunction.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(async (hostContext, services) =>
    {
        const string secretName = "connectionstring2";
        var keyVaultName = "akerstroemKeyVault";
        var kvUri = $"https://{keyVaultName}.vault.azure.net";

        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

        // Retrieve the secret synchronously
        var secret = client.GetSecret(secretName);

        string sqlConnectionString = secret.Value.Value;

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(sqlConnectionString);
        });
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

    })
    .Build();

await host.RunAsync();