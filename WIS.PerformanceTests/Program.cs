using NBomber.CSharp;
using WIS.PerformanceTests.Scenarios;

namespace WIS.PerformanceTests;

public class Program
{
    public static void Main(string[] args)
    {
        NBomberRunner
            .RegisterScenarios(
                CreateInventoryScenario.Create(),
                CreateInventoryIncomingScenario.Create(),
                CreateInventoryOutgoingScenario.Create())
            .Run();
    }
}