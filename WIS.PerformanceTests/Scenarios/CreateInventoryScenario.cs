using System.Net.Http.Json;
using AutoFixture;
using NBomber.Contracts;
using NBomber.CSharp;
using WIS.Application.Common.Features.CreateInventoryItem;

namespace WIS.PerformanceTests.Scenarios;

public static class CreateInventoryScenario
{
    public static ScenarioProps Create()
    {
        var httpClient = new HttpClient();

        var fixture = new Fixture();
        
        var scenario = Scenario.Create("create", async context =>
            {
                var createInventoryItemCommand = fixture.Create<CreateInventoryItemCommand>();
                
                var response = await httpClient.PostAsJsonAsync(
                    "http://localhost:5000/api/inventory/create",
                    createInventoryItemCommand);

                return response.IsSuccessStatusCode
                    ? Response.Ok()
                    : Response.Fail(response, response.StatusCode.ToString());
            })
            .WithLoadSimulations(LoadSimulationContext.Rate100);

        return scenario;
    }
}