using System.Net.Http.Json;
using AutoFixture;
using NBomber.Contracts;
using NBomber.CSharp;
using WIS.Application.Common.Features.CreateInventoryItem;
using WIS.Application.Common.Features.ReceiveInventoryItem;
using WIS.PerformanceTests.Extensions;

namespace WIS.PerformanceTests.Scenarios;

public static class CreateInventoryIncomingScenario
{
    public static async Task<ScenarioProps> CreateAsync()
    {
        var httpClient = HttpClientBuilder.Create(new Uri("http://localhost:5177"));

        var inventoryIds = await CreateInventoryContext.CreateAsync().ToListAsync();

        var scenario = Scenario.Create("register-incoming-stock", async context =>
            {
                var id = inventoryIds[Random.Shared.Next(inventoryIds.Count())];

                var command = new RegisterIncomingStockRequest
                {
                    Code = id,
                    Quantity = 10
                };

                var response = await httpClient.PostAsJsonAsync(
                    "/api/warehouse/register-incoming-stock",
                    command);

                if (response.IsSuccessStatusCode)
                {
                    return Response.Ok();
                }
                
                var body = await response.Content.ReadAsStringAsync();
                return Response.Fail(statusCode: response.StatusCode.ToString(), message: body);
            })
            .WithLoadSimulations(LoadSimulationContext.Rate100);
        return scenario;
    }
}