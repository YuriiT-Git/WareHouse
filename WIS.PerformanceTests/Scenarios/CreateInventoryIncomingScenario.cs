using System.Net.Http.Json;
using AutoFixture;
using NBomber.Contracts;
using NBomber.CSharp;
using WIS.Application.Common.Features.CreateInventoryItem;
using WIS.Application.Common.Features.ReceiveInventoryItem;

namespace WIS.PerformanceTests.Scenarios;

public static class CreateInventoryIncomingScenario
{
    public static ScenarioProps Create()
    {
        var httpClient = HttpClientBuilder.Create(new Uri("http://localhost:5177"));

        var preparationTask = CreateInventoryContext.Create();
        Task.WaitAll(preparationTask);

        List<string> inventoryIds = preparationTask.Result.ToList();

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
            .WithLoadSimulations(Simulation.Inject(
                rate: 10,
                interval: TimeSpan.FromSeconds(1),
                during: TimeSpan.FromSeconds(30)));
        return scenario;
    }
}