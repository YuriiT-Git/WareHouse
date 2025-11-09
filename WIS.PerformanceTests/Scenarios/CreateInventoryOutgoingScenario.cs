using System.Net.Http.Json;
using AutoFixture;
using DotPulsar.Abstractions;
using NBomber.Contracts;
using NBomber.CSharp;
using WarehouseInventorySystem.Models;
using WIS.Application.Common.Features.CreateInventoryItem;
using WIS.Application.Common.Features.ReceiveInventoryItem;

namespace WIS.PerformanceTests.Scenarios;

public static class CreateInventoryOutgoingScenario
{
    public static async Task<ScenarioProps> CreateAsync()
    {
        var httpClient = HttpClientBuilder.Create(new Uri("http://localhost:5177"));

        List<string> inventoryIds = new();
        
        await foreach (var item in CreateInventoryContext.CreateAsync())
        {
            var command = new RegisterIncomingStockRequest
            {
                Code = item,
                Quantity = 100
            };

            var response = await httpClient.PostAsJsonAsync("/api/warehouse/register-incoming-stock", command);

            if (response.IsSuccessStatusCode)
            {
                inventoryIds.Add(item);
            }
        }

        var scenario = Scenario.Create("register-outgoing-stock", async context =>
            {
                var id = inventoryIds[Random.Shared.Next(inventoryIds.Count())];
                
                var command = new RegisterOutgoingStockModel
                {
                    Code = id,
                    Quantity = 2
                };
                
                var response = await httpClient.PostAsJsonAsync(
                    "/api/warehouse/register-outgoing-stock",
                    command);
                
                if (response.IsSuccessStatusCode)
                {
                    return Response.Ok();
                }
                
                var body = await response.Content.ReadAsStringAsync();
                return Response.Fail(statusCode: response.StatusCode.ToString(), message: body);
            }
            ).WithLoadSimulations(LoadSimulationContext.Rate100);

            return scenario;
        }
    }