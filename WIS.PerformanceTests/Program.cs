using NBomber.CSharp;
using WIS.PerformanceTests.Scenarios;

namespace WIS.PerformanceTests;

public class Program
{
    public static async Task Main(string[] args)
    {
        NBomberRunner
            .RegisterScenarios(
                CreateInventoryScenario.Create(),
                await CreateInventoryIncomingScenario.CreateAsync(),
                await CreateInventoryOutgoingScenario.CreateAsync())
            .Run();
    }
}