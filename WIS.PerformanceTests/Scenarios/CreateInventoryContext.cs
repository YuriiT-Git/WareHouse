using System.Net.Http.Json;
using AutoFixture;
using NBomber.Contracts;
using WIS.Application.Common.Features.CreateInventoryItem;

namespace WIS.PerformanceTests.Scenarios;

public static class CreateInventoryContext
{
    public static async Task<IEnumerable<string>> Create()
    {
        Console.WriteLine("Preparing test data...");
        var httpClient = HttpClientBuilder.Create(new Uri("http://localhost:5177"));
        var fixture = new Fixture();
        var ids = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            var createInventoryItemCommand = fixture.Build<CreateInventoryItemCommand>()
                .With(x => x.Brand, fixture.Create<string>().Substring(0, 10))
                .With(x => x.Model, fixture.Create<string>().Substring(0, 10))
                .With(x => x.Color, fixture.Create<string>().Substring(0, 10))
                .Create<CreateInventoryItemCommand>();
            
            var response = await httpClient.PostAsJsonAsync(
                "/api/inventory/create", createInventoryItemCommand);

            if (response.IsSuccessStatusCode)
            {
                var id = await response.Content.ReadAsStringAsync();
                ids.Add(id);
            }
        }
        
        Console.WriteLine($"Created {ids.Count} inventory items for load test.");
        return ids;
    }
}